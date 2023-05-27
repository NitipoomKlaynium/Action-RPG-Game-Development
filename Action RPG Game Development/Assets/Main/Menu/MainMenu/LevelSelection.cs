using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] Button[] level;
    int levelClear = 1;

    void Start()
    {
        if (PlayerPrefs.HasKey("Level Clear"))
        {
            levelClear = PlayerPrefs.GetInt("Level Clear");
        }
        else
        {
            levelClear = 1;
            PlayerPrefs.SetInt("Level Clear", 1);
        }

        for (int i = 1 ; i <= 5; i++)
        {
            if (i > levelClear)
            {
                Image[] images = level[i-1].GetComponentsInChildren<Image>();
                foreach (Image image in images)
                {
                    image.color = Color.white;
                }
                level[i - 1].interactable = false;
            }
        }


    }

    public void Starting()
    {
        if (PlayerPrefs.HasKey("Level Clear"))
        {
            levelClear = PlayerPrefs.GetInt("Level Clear");
        }
        else
        {
            levelClear = 1;
            PlayerPrefs.SetInt("Level Clear", 1);
        }
        
        for (int i = 1; i <= 5; i++)
        {
            if (i > levelClear)
            {
                Image[] images = level[i - 1].GetComponentsInChildren<Image>();
                foreach (Image image in images)
                {
                    image.color = Color.white;
                }
                level[i - 1].interactable = false;
            }
        }
        Debug.Log(levelClear);
    }

    void Update()
    {
        
    }

    public void LoadScene(int i) {
        SceneManager.LoadScene(i);
    }
}
