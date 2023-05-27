using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionClear : MonoBehaviour
{
    public TMP_Text coin;
    void Start()
    {
        PlayerPrefs.SetInt("Menu Mode", 0);
        coin.SetText("+ " + PlayerPrefs.GetInt("Coin Recieve").ToString());
        PlayerPrefs.SetInt("Coin Recieve", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
