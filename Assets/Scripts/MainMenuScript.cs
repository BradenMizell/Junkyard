using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour{
    public void PlayGame(){
        Debug.Log("sampleScene");

        SceneManager.LoadScene("Scenes/SampleScene");
    }

    public void HowToPlay(){
        SceneManager.LoadScene("Scenes/HowToPlay");
    }

    public void Credits(){
        SceneManager.LoadScene("Scenes/Credits");
    }

    public void QuitGame(){
        Application.Quit();
        Debug.Log("Game Quit!");
    }
}