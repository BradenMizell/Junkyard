using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class exitLevel : MonoBehaviour
{

    [SerializeField] string nextLevelName;
    void OnTriggerEnter(Collider collision)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene(nextLevelName);
        //SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(nextLevelIndex).ToString());
        //LevelController.UpdateScene(nextLevelIndex);
    }
}
