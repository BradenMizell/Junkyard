using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject hurtOverlay;
    public Transform respawnPoint;
    public GameObject player;
    public PlayerMovement pm;

    static public bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                hurtOverlay.SetActive(true);
                ResumeGame();
            }
            else
            {
                hurtOverlay.SetActive(false);
                PauseGame();
            }
        }
    }
    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void gotoMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void respawn()
    {
        Vector3 respawnPos = new Vector3(respawnPoint.transform.position.x, respawnPoint.transform.position.y, respawnPoint.transform.position.z);
        player.transform.position = respawnPos;
        pm.freeze = true;
        ResumeGame();
    }
}

