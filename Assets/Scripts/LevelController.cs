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
        if (sceneNum < SceneManager.sceneCount)
        {
            SceneManager.LoadScene(SceneManager.GetSceneAt(sceneNum).ToString());
            sceneNum++;
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
            sceneNum = 0;
        }
    }
}
