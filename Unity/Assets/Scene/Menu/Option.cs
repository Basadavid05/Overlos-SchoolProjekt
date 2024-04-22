using UnityEngine;
using UnityEngine.SceneManagement;


public class Option : MonoBehaviour
{
    public void GoBackToTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void GoMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
