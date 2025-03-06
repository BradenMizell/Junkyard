using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Scenes/SampleScene");
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene("Scenes/HowToPlay");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Scenes/Credits");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit!");
    }
}