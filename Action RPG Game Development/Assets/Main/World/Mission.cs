using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission : MonoBehaviour
{
    public GameObject[] enemies;

    public int firstReward = 50;
    public int normalReward = 20;

    public Player player;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        bool isClear = true;

        foreach (GameObject enemy in enemies)
        {
            if (enemy != null) // Check if enemy game object is not destroyed
            {
                isClear = false;
                break;
            }
        }

        //Mission Clear
        if (isClear)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt("Menu Mode", 1);

            if (PlayerPrefs.GetInt("Level Clear") == currentSceneIndex)
            {
                
                PlayerPrefs.SetInt("Coin Recieve", firstReward);
                PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + firstReward);
                PlayerPrefs.SetInt("Level Clear", currentSceneIndex + 1);
            }
            else
            {
                PlayerPrefs.SetInt("Coin Recieve", normalReward);
                PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + normalReward);
            }

            SceneManager.LoadScene(0);
        }

        //Failed
        if (player.health <= 0)
        {
            PlayerPrefs.SetInt("Menu Mode", 2);
            SceneManager.LoadScene(0);
        }
    }
}
