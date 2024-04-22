using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public static Main main;
    public int load;

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
        load = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(load);
        SceneManager.LoadScene(2);
    }

    public void Back()
    {
        SceneManager.LoadScene(load);
    }

    public void GoMenu()
    {
        load = SceneManager.GetActiveScene().buildIndex;
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
