using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour{
    public GameObject pauseMenu;
    private bool isPaused = false;

    void Start()
{
    pauseMenu.SetActive(false);
}


    void Update(){
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        
    }

    void Pause(){
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        
    }

    public void QuitGame(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    
    public void PlayGame(){
        pauseMenu.SetActive(false);
    }

    public void HowToPlay(){
        SceneManager.LoadScene("Scenes/HowToPlay");
    }

    public void Credits(){
        SceneManager.LoadScene("Scenes/Credits");
    }

    public void Back(){
        SceneManager.LoadScene("Scenes/MainMenu");
    }

   
}