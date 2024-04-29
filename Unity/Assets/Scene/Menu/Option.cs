using UnityEngine;
using UnityEngine.SceneManagement;


public class Option : MonoBehaviour
{
    [Header("Publics")]
    public GameObject pages;
    public GameObject Switches;

    [Header("Page1")]
    private GameObject page1;
    private GameObject Hotkeys;

    [Header("Page2")]
    private GameObject page2;
    private GameObject General;

    private void Start()
    {
        page1=pages.transform.GetChild(0).gameObject;
        page2=pages.transform.GetChild(1).gameObject;

        Hotkeys = Switches.transform.GetChild(0).gameObject;
        General = Switches.transform.GetChild(1).gameObject;
        SwitchOptionsType(true);
    }

    public void GoBackToTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void GoMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }


    public void SwitchOptionsType(bool status)
    {
        page1.SetActive(status);
        page2.SetActive(!status);
        General.SetActive(status);
        Hotkeys.SetActive(!status);
    }
}
