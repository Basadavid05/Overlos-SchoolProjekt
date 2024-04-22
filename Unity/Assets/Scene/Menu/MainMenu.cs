using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int load;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        MapControl.MapIsEnabled = true;
    }

    public void Settings()
    {
        load = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(load);
        SceneManager.LoadScene(2);
    }

    public void Back()
    {
        SceneManager.LoadScene(load);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
