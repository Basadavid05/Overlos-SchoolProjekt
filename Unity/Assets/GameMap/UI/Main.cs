using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public static Main main;

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

    public void loadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
