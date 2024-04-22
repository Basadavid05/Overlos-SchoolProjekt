using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [Header("Pause")]
    public static bool GameIsPause = false;
    private GameObject PauseMenuUI;


    private void Start()
    {
        GameIsPause = false;
        PauseMenuUI = transform.GetChild(0).gameObject;
        PauseMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
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
