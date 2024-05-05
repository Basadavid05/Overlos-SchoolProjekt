using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public static Main main;
    public int load;
    public int settingsload=0;

    [Header("Buttons")]
    private int spawn;
    public Vector3 playerlocation;

    [HideInInspector] public bool BackToGame;
    [HideInInspector] public bool ChangedHotkeys;

    [Header("Settings")]
    public int CurrentResolutionIndex = -1;
    public int CurrentRefreshIndex = -1;
    public int QualityIndex = 0;
    public int fullscreen=0;

    public Dictionary<Hotkeys, KeyCode> keyMappings = new Dictionary<Hotkeys, KeyCode>();


    public enum Hotkeys {
        Sprint,
        Jump,
        Slide,
        Crouch,
        Alt,
        interract,
        Inventory,
        CameraSwitch,
        ViewFps,
        SoulTestKey,
        Drop
    }


    private void Awake()
    {
        if (main == null)
        {
            main = this;
        }
        else
        {
            // If an instance already exists, destroy this one
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {
        if (spawn == 0)
        {
            BackToGame = false;


            keyMappings[Hotkeys.Sprint] = KeyCode.LeftShift;
            keyMappings[Hotkeys.Jump] = KeyCode.Space;
            keyMappings[Hotkeys.Slide] = KeyCode.LeftControl;
            keyMappings[Hotkeys.Crouch] = KeyCode.C;
            keyMappings[Hotkeys.Alt] = KeyCode.LeftAlt;
            keyMappings[Hotkeys.interract] = KeyCode.E;
            keyMappings[Hotkeys.Inventory] = KeyCode.I;
            keyMappings[Hotkeys.ViewFps] = KeyCode.F1;
            keyMappings[Hotkeys.CameraSwitch] = KeyCode.V;
            keyMappings[Hotkeys.SoulTestKey] = KeyCode.P;
            keyMappings[Hotkeys.Drop] = KeyCode.O;



            Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;
            Screen.SetResolution(Screen.width, Screen.height, true);
            QualitySettings.vSyncCount = 0;
            QualitySettings.SetQualityLevel(0);
            spawn++;
        }
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        MapControl.MapIsEnabled = true;
    }

    public void GoBackToTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Settings()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            load = 0;
        }
        SceneManager.LoadScene(1);

    }

    public void Back()
    {
        Debug.Log(load);
        SceneManager.LoadScene(load);
    }

    public void GoMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Lock(bool status)
    {
        if (status)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = status;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = status;
        }
    }
}
