using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class exitLevel : MonoBehaviour
{

    [SerializeField] int nextLevelIndex;
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("running");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(nextLevelIndex).ToString());
        //LevelController.UpdateScene(nextLevelIndex);
    }
}
