using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static int sceneNum = 1;

    public static void UpdateScene()
    {
        if (sceneNum < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log(sceneNum);
            SceneManager.LoadScene(SceneManager.GetSceneAt(sceneNum).ToString());
            sceneNum++;
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
            sceneNum = 1;
        }
    }
}
