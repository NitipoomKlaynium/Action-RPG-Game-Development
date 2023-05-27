using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{
    public GlobalAudioManager _audioManager;

    public GameObject mainMenu;
    public GameObject missionClear;
    public GameObject gameOver;
    

    void Start()
    {
        Application.targetFrameRate = 60;
        _audioManager.UpdateVolume();
        _audioManager.PlayMusic("Main Menu");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //Stats
        if (!PlayerPrefs.HasKey("Health"))
        {
            PlayerPrefs.SetInt("Health", 0);
        }
        if (!PlayerPrefs.HasKey("Stamina"))
        {
            PlayerPrefs.SetInt("Stamina", 0);
        }
        if (!PlayerPrefs.HasKey("Attack"))
        {
            PlayerPrefs.SetInt("Attack", 0);
        }

        if (!PlayerPrefs.HasKey("Recovery Potion"))
        {
            PlayerPrefs.SetInt("Recovery Potion", 0);
        }


        if (!PlayerPrefs.HasKey("Coin"))
        {
            PlayerPrefs.SetInt("Coin", 50000);
        }

        if (!PlayerPrefs.HasKey("Menu Mode"))
        {
            PlayerPrefs.SetInt("Menu Mode", 0);
        }

        int mode = PlayerPrefs.GetInt("Menu Mode");

        switch (mode)
        {
            case 0:
                mainMenu.SetActive(true);
                missionClear.SetActive(false);
                gameOver.SetActive(false);
                break;
            case 1:
                mainMenu.SetActive(false);
                missionClear.SetActive(true);
                gameOver.SetActive(false);
                break;
            case 2:
                mainMenu.SetActive(false);
                missionClear.SetActive(false);
                gameOver.SetActive(true);
                break;
        }
        
    }

    void Update()
    {
        
    }
}
