using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;



public class MainMenu : MonoBehaviour
{
    [Header("Buttons")]
    public UnityEngine.UI.Button[] buttons;
    public List<string> scripts;


    [Header("Menus")]
    private GameObject Menu;
    private GameObject GameMenu;
    private Transform newGameMenu;
    private Button NewGame;
    private TMP_InputField GameTitle;
    private bool CretatorMenuIsOpen;


    [Header("Games")]
    public GameObject[] Maps;
    private Main main;
    private int spawn;

    private void Start()
    {
        if (!main)
        {
            main = GameObject.Find("Main").GetComponent<Main>();
        }

        Scene sceneToUnload = SceneManager.GetSceneByName("Game");
        if (sceneToUnload.IsValid())
        {
            SceneManager.UnloadSceneAsync("Game");
        }

        Scene Options = SceneManager.GetSceneByName("Options");
        if (sceneToUnload.IsValid())
        {
            SceneManager.UnloadSceneAsync("Options");
        }


        Menu =transform.GetChild(0).gameObject;
        GameMenu=transform.GetChild(1).gameObject;
        newGameMenu= GameMenu.transform.GetChild(1).GetChild(0);

        GameTitle = newGameMenu.Find("GameTitle").gameObject.GetComponent<TMP_InputField>();
        NewGame= newGameMenu.Find("CreateNewGame").gameObject.GetComponent<Button>();
        PlayMenu(false);

        if (spawn == 0)
        {
                foreach (UnityEngine.UI.Button button in buttons)
                {
                    scripts.Add(button.onClick.GetPersistentMethodName(0));
                }
                spawn++;
        }

        ButtonCheck();
    }

    private void ButtonCheck()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            string script = scripts[i];
            buttons[i].onClick.AddListener(() =>
            {
                main.Invoke(script, 0f);
            });
        }
    }

    public void PlayMenu(bool status)
    {
        Menu.SetActive(!status);
        GameMenu.SetActive(status);
        CretatorMenuIsOpen=status;
    }

    public void CreateNewMenu()
    {
        string GameName = GameTitle.text;
        string mapname="Island";
        SceneManager.LoadScene(mapname,LoadSceneMode.Additive);

    }

    public void QuitGame()
        {
            Application.Quit();
        }

    private void Update()
    {
        if (CretatorMenuIsOpen)
        {
            if (GameTitle.text.Length == 0)
            {
                NewGame.enabled = false;
            }
            else
            {
                NewGame.enabled = true;
            }

            if (Input.GetKey(KeyCode.Escape))
            {
                PlayMenu(false);
            }
        }
        
    }

}

