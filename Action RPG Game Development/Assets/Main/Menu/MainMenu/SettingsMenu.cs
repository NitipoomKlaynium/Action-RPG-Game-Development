using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Image _panel;

    [SerializeField] private PostProcessLayer _postProcessLayer;

    [SerializeField] private Slider _musicSlider;
    [SerializeField] private TMP_Text _musicText;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private TMP_Text _sfxText;
    [SerializeField] private Dropdown _antiAliasingDropdown;
    [SerializeField] private Dropdown _textureQualityDropdown;
    [SerializeField] private Dropdown _shadowResolutionDropdown;
    [SerializeField] private Dropdown _vSyncDropdown;

    [SerializeField] private GlobalAudioManager _audioManager;

    void Start()
    {
        GetPlayerPref();
        _audioManager.UpdateVolume();
    }

    void Update()
    {

    }

    private void GetPlayerPref()
    {
        //Get config settings

        _musicSlider.value = GetPlayerPrefFloat("Music Volume", 0.8f);
        _sfxSlider.value = GetPlayerPrefFloat("SFX Volume", 0.8f);

        _antiAliasingDropdown.value = GetPlayerPrefInt("Anti Aliasing", 4);
        _textureQualityDropdown.value = GetPlayerPrefInt("Texture Quality", 0);
        _shadowResolutionDropdown.value = GetPlayerPrefInt("Shadow Resolution", 0);
        _vSyncDropdown.value = GetPlayerPrefInt("VSync", 0);

        MusicVolumeChange();
        SFXVolumeChange();
        AntiAliasingChange();
        TextureQualityChange();
        ShadowResolutionChange();
        VsyncChange();

    }

    public static int GetPlayerPrefInt(string key, int defaultValue)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetInt(key);
        }
        else
        {
            return defaultValue;
        }
    }

    public static float GetPlayerPrefFloat(string key, float defaultValue)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetFloat(key);
        }
        else
        {
            return defaultValue;
        }
    }

    public void PlayButtonSound()
    {
        _audioManager.PlaySFX("Click");
    }

    public void BackButtonEvent()
    {
        _panel.color = new Color(1.0f, 1.0f, 1.0f);
        _audioManager.PlayMusic("Main Menu");
    }

    public void MusicVolumeChange()
    {
        _musicText.SetText((_musicSlider.value * 100f).ToString("F0") + "%");
        PlayerPrefs.SetFloat("Music Volume", _musicSlider.value);
        _audioManager.UpdateVolume();
    }

    public void SFXVolumeChange()
    {
        _sfxText.SetText((_sfxSlider.value * 100f).ToString("F0") + "%");
        PlayerPrefs.SetFloat("SFX Volume", _sfxSlider.value);
        _audioManager.UpdateVolume();
    }

    public void AntiAliasingChange()
    {
        switch (_antiAliasingDropdown.value)
        {
            case 0:
                QualitySettings.antiAliasing = 0;
                _postProcessLayer.antialiasingMode = PostProcessLayer.Antialiasing.None;
                break;
            case 1:
                QualitySettings.antiAliasing = 2;
                _postProcessLayer.antialiasingMode = PostProcessLayer.Antialiasing.None;
                break;
            case 2:
                QualitySettings.antiAliasing = 4;
                _postProcessLayer.antialiasingMode = PostProcessLayer.Antialiasing.None;
                break;
            case 3:
                QualitySettings.antiAliasing = 8;
                _postProcessLayer.antialiasingMode = PostProcessLayer.Antialiasing.None;
                break;
            case 4:
                QualitySettings.antiAliasing = 0;
                _postProcessLayer.antialiasingMode = PostProcessLayer.Antialiasing.FastApproximateAntialiasing;
                break;
            case 5:
                QualitySettings.antiAliasing = 0;
                _postProcessLayer.antialiasingMode = PostProcessLayer.Antialiasing.SubpixelMorphologicalAntialiasing;
                break;
            case 6:
                QualitySettings.antiAliasing = 0;
                _postProcessLayer.antialiasingMode = PostProcessLayer.Antialiasing.TemporalAntialiasing;
                break;
        }

        PlayerPrefs.SetInt("Anti Aliasing", _antiAliasingDropdown.value);
    }

    public void TextureQualityChange()
    {
        switch (_textureQualityDropdown.value)
        {
            case 0:
                QualitySettings.masterTextureLimit = 0;
                break;
            case 1:
                QualitySettings.masterTextureLimit = 1;
                break;
            case 2:
                QualitySettings.masterTextureLimit = 2;
                break;
            case 3:
                QualitySettings.masterTextureLimit = 3;
                break;
        }

        PlayerPrefs.SetInt("Texture Quality", _textureQualityDropdown.value);
    }

    public void ShadowResolutionChange()
    {
        switch (_shadowResolutionDropdown.value)
        {
            case 0:
                QualitySettings.shadowResolution = ShadowResolution.VeryHigh;
                break;
            case 1:
                QualitySettings.shadowResolution = ShadowResolution.High;
                break;
            case 2:
                QualitySettings.shadowResolution = ShadowResolution.Medium;
                break;
            case 3:
                QualitySettings.shadowResolution = ShadowResolution.Low;
                break;
        }

        PlayerPrefs.SetInt("Shadow Resolution", _shadowResolutionDropdown.value);
    }

    public void VsyncChange() { 
        switch (_vSyncDropdown.value)
        {
            case 0:
                QualitySettings.vSyncCount = 0;
                break;
            case 1:
                QualitySettings.vSyncCount = 1;
                break;
        }

        PlayerPrefs.SetInt("VSync", _vSyncDropdown.value);
    }

}
