using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static int sceneNum = 0;

    public static void UpdateScene()
    {
        sceneNum = SceneManager.GetActiveScene().buildIndex;
        if (sceneNum < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log(sceneNum + 1);
            SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(sceneNum).ToString());
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
            sceneNum = 0;
        }
    }
}
