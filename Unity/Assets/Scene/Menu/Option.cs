using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Option : MonoBehaviour
{
    [Header("Publics")]
    public GameObject pages;
    public GameObject Switches;

    [Header("Menu-Leaving")]
    public GameObject GoBacks;
    public GameObject ApplyButton;
    private GameObject GoToMenu;
    private GameObject GoToGame;

    [Header("Page1")]
    private GameObject page1;
    private GameObject Hotkeys;

    [Header("Page2")]
    private GameObject page2;
    private GameObject General;

    [Header("Resolution")]
    private Resolution[] Resolutions;

    [Header("Dropdown")]
    public TMPro.TMP_Dropdown Resolutionsdropdown;
    public TMPro.TMP_Dropdown FullScreenModes;
    public TMPro.TMP_Dropdown RefreshDropdown;
    public TMPro.TMP_Dropdown QualityDropdown;

    private List<string> ResolutionsOptions = new List<string>();
    private List<string> RefreshRates = new List<string>();
    private List<string> QualitySettingss = new List<string>();
    private List<string> Fullscreenss = new List<string>();

    private Main main;
    private bool changehappend=false;

    [Header("Saved")]
    public GameObject Saved;
    private UnityEngine.UI.Image img;
    private TextMeshProUGUI text;
    private float imgDefaultAlpha;
    private float textDefaultAlpha;


    private void Start()
    {
        if (!main)
        {
            main = GameObject.Find("Main").GetComponent<Main>();
        }

        Saved.SetActive(false);
        img = Saved.GetComponent<UnityEngine.UI.Image>();
        text = img.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        imgDefaultAlpha = img.color.a;
        textDefaultAlpha = text.color.a;

        GoToMenu = GoBacks.transform.GetChild(0).gameObject;
        GoToGame = GoBacks.transform.GetChild(1).gameObject;
        if (Main.main.load == 0)
        {
            GoToGame.SetActive(false);
        }
        else
        {
            GoToMenu.SetActive(false);
        }


        Resolutions = Screen.resolutions;

        for (int i = Resolutions.Length - 1; i >= 0; i--)
        {
            string options = Resolutions[i].width + "x" + Resolutions[i].height;
            ResolutionsOptions.Add(options);
            if (main.settingsload == 0 && Resolutions[i].width == Screen.width && Resolutions[i].height == Screen.height)
            {
                main.CurrentResolutionIndex = i;
                SetResolution();
            }
        }

        if (main.settingsload == 0 && main.CurrentResolutionIndex == -1)
        {
            main.CurrentResolutionIndex = Resolutions.Length - 1;
            SetResolution();
        }

        Resolutionsdropdown.ClearOptions();
        Resolutionsdropdown.AddOptions(ResolutionsOptions);
        Resolutionsdropdown.value = ResolutionsOptions.Count - 1 - main.CurrentResolutionIndex;
        Resolutionsdropdown.RefreshShownValue();


        List<int> commonRefreshRates = new List<int>(){
            60, 75, 100, 120, 144, 165, 180, 200, 240, 300};
        for (int i = commonRefreshRates.Count - 1; i >= 0; i--)
        {
            string options = commonRefreshRates[i] + " hz";
            RefreshRates.Add(options);
            if (main.settingsload == 0 && commonRefreshRates[i] == (int)Screen.currentResolution.refreshRateRatio.value)
            {
                    main.CurrentRefreshIndex = i;
                    SetFrame();
            }
        }
        if (main.settingsload == 0 && main.CurrentRefreshIndex == -1)
        {
            main.CurrentRefreshIndex = 0;
            SetFrame();
        }

        RefreshDropdown.ClearOptions();
        RefreshDropdown.AddOptions(RefreshRates);
        RefreshDropdown.value = RefreshRates.Count - 1 - main.CurrentRefreshIndex;
        RefreshDropdown.RefreshShownValue();


        for (int i = 0; i < QualitySettings.count; i++)
        {
            QualitySettingss.Add(QualitySettings.names[i]);
        }

        QualityDropdown.ClearOptions();
        QualityDropdown.AddOptions(QualitySettingss);

        if (main.settingsload == 0)
        {
            QualityDropdown.value = 0;
        }

        QualityDropdown.RefreshShownValue();

        FullScreenModes.ClearOptions();
        Fullscreenss.Add("Enable");
        Fullscreenss.Add("Disable");
        FullScreenModes.AddOptions(Fullscreenss);

        if (main.settingsload == 0)
        {
            FullScreenModes.value = 0;
            main.fullscreen = 0;
        }

        page1 = pages.transform.GetChild(0).gameObject;
        page2 = pages.transform.GetChild(1).gameObject;

        Hotkeys = Switches.transform.GetChild(0).gameObject;
        General = Switches.transform.GetChild(1).gameObject;

        Resolutionsdropdown.value = ResolutionsOptions.Count - 1 - main.CurrentResolutionIndex;
        RefreshDropdown.value = RefreshRates.Count - 1 - main.CurrentRefreshIndex;
        QualityDropdown.value = main.QualityIndex;

        SwitchOptionsType(true);

        if (main.settingsload == 0)
        {
            main.settingsload++;
        }
    }

    public void GoBackToTheGame()
    {
        SceneManager.LoadScene(Main.main.load);
        Main.main.BackToGame=true;
    }

    public void GoMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SwitchOptionsType(bool status)
    {
        page1.SetActive(status);
        page2.SetActive(!status);
        Saved.SetActive(false);
        ApplyButton.SetActive(status);
        General.SetActive(status);
        Hotkeys.SetActive(!status);
    }

    public void Apply()
    {
        int selectedResolutionIndex = ResolutionsOptions.Count - 1 - Resolutionsdropdown.value;
        if (selectedResolutionIndex != Resolutionsdropdown.value)
        {
            changehappend=true;
            main.CurrentResolutionIndex = selectedResolutionIndex;
            SetResolution();
        }

        int selectedRefleshIndex = RefreshRates.Count - 1 - RefreshDropdown.value;
        if (selectedRefleshIndex != RefreshDropdown.value)
        {
            changehappend = true;
            main.CurrentRefreshIndex = selectedRefleshIndex;
            SetFrame();
        }

        if(main.QualityIndex != QualityDropdown.value)
        {
            changehappend = true;
            main.QualityIndex =QualityDropdown.value;
            SetQuality();
        }
        
        if (FullScreenModes.value!=main.fullscreen)
        {
            changehappend = true;
            main.fullscreen=FullScreenModes.value;
            Screen.fullScreen= (main.fullscreen!=1);
        }

        if (changehappend)
            {
                Saved.SetActive(true);
                Invoke("FadeOut", 5f);
                changehappend=false;
            }
    }

    private void FadeOut()
    {
        float duration = 2f;

        if (img != null)
        {
            StartCoroutine(FadeComponent(img, img.color.a, 0, duration));
        }
        if (text != null)
        {
            StartCoroutine(FadeComponent(text, text.color.a, 0, duration));
        }

    }

    IEnumerator FadeComponent(Graphic component, float start, float end, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            Color newColor = component.color;
            newColor.a = Mathf.Lerp(start, end, elapsedTime / duration);
            component.color = newColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        component.color = new Color(component.color.r, component.color.g, component.color.b, end);

        if (end == 0)
        {
            Saved.SetActive(false);

            if (component == img)
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, imgDefaultAlpha);
            }
            else if (component == text)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, textDefaultAlpha);
            }
        }
    }
    
    private void SetFrame()
    {
        UnityEngine.Application.targetFrameRate = main.CurrentRefreshIndex;
    }

    private void SetQuality()
    {
        QualitySettings.SetQualityLevel(main.QualityIndex);
    }

    private void SetResolution()
    {
        Resolution resolution = Resolutions[main.CurrentResolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }
}