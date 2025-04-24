using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControlle : MonoBehaviour
{
    public static List<Scene> scenes;
    public static int sceneNum = 0;

    private void Start()
    {
        Debug.Log("hi!");
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            scenes.Add(SceneManager.GetSceneAt(i));
        }
    }

    public static void UpdateScene()
    {
        if (sceneNum < scenes.Count)
        {
            SceneManager.LoadScene(scenes[sceneNum].ToString());
            sceneNum++;
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
            sceneNum = 0;
        }
    }
}
