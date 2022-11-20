using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    // Start is called before the first frame update
    public void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }
    
    public void Play() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        UnityEngine.Debug.Log("Log : Play Game!");
    }

    public void Quit() {
        Application.Quit();
        UnityEngine.Debug.Log("Log : Quit Game!");
    }
}
