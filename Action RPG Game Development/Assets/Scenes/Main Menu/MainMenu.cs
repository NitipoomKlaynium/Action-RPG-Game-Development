using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public void PlayButtonHandleEvent() {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);    
        Debug.Log("Play Game");
        SceneManager.LoadScene(1);
    }
}
