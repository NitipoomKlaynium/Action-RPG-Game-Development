using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Image _panel;

    [SerializeField] private GlobalAudioManager _audioManager;

    void Start()
    {

    }

    void Update()
    {
        
    }
    public void PlayButtonSound()
    {
        _audioManager.PlaySFX("Click");
    }

    public void PlayButtonEvent()
    {
        
    }

    public void SettngsButtonEvent()
    {
        _panel.color = new Color(0.8f, 0.8f, 0.8f);
        _audioManager.PlayMusic("Settings");
    }

    public void ExitButtonEvent()
    {
        Application.Quit();
    }

    public void ResetValue()
    {
        PlayerPrefs.SetInt("Health", 0);
        PlayerPrefs.SetInt("Stamina", 0);
        PlayerPrefs.SetInt("Attack", 0);
        PlayerPrefs.SetInt("Coin", 0);
        PlayerPrefs.SetInt("Level Clear", 1);
    }
        
    public void GainCoin()
    {
        PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 500);
    }

    public void UnlockLevel()
    {
        PlayerPrefs.SetInt("Level Clear", 5);
    }
}