using UnityEngine;
using UnityEngine.SceneManagement;

public class HTPMenu : MonoBehaviour{
    public void Back(){
        SceneManager.LoadScene("Scenes/MainMenu");
    }
}