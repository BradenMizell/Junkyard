using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class exitLevel : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        LevelControlle.UpdateScene();
    }
}
