using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class exitLevel : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("running");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        LevelController.UpdateScene();
    }
}
