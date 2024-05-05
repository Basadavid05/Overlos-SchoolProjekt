using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [Header("Pause")]
    public static bool GameIsPause = false;

    public UnityEngine.UI.Button[] buttons;
    public List <string> scripts;

    private GameObject PauseMenuUI;
    private int spawn=0;

    private Main main;


    private void Start()
    {
        GameIsPause = false;
        PauseMenuUI = transform.GetChild(0).gameObject;
        PauseMenuUI.SetActive(false);

        if (!main)
        {
            main = GameObject.Find("Main").GetComponent<Main>();
        }

        if (spawn == 0)
        {            
            foreach (UnityEngine.UI.Button button in buttons)
            {
                scripts.Add(button.onClick.GetPersistentMethodName(0));
            }
            spawn++;
        }

        ButtonCheck();

        if (main.BackToGame)
        {
            Pause();
        }
    }

    

    private void ButtonCheck()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            string script =scripts[i];
            buttons[i].onClick.AddListener(() =>
            {
                main.Invoke(script, 0f);
            });
        }        
    }

    // Update is called once per frame
    private void Update()
    {

        if (!InventoryController.InventoryOpen && !MapControl.MapIsEnabled &&  !SoulShop.SoulShopActive && !MapControl.MapIsEnabled && !PlayerDatas.Death)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPause)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
        
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Main.main.Lock(false);
        GameIsPause = false;

    }

    public void Pause()
    {
        Time.timeScale = 0f;
        PauseMenuUI.SetActive(true);
        Main.main.Lock(true);
        GameIsPause = true;
    }



}