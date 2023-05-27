using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GlobalAudioManager : MonoBehaviour
{
    public static GlobalAudioManager Instance { get; private set; }

    [SerializeField]private AudioSource MusicSource, SFXSource;

    [SerializeField]private Audio[] Music, SFX;

    private float MusicVolume = 0f, SFXVolume = 0f;

    private void Awake()
    {
        // Singleton pattern to ensure there's only one SoundManager instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    void Start()
    {
        if (PlayerPrefs.HasKey("Music Volume"))
        {
            MusicVolume = PlayerPrefs.GetFloat("Music Volume");
        }
        else
        {
            MusicVolume = 0.8f;
            PlayerPrefs.SetFloat("Music Volume", 0.8f);
        }
        if (PlayerPrefs.HasKey("SFX Volume"))
        {
            SFXVolume = PlayerPrefs.GetFloat("SFX Volume");
        }
        else
        {
            SFXVolume = 0.8f;
            PlayerPrefs.SetFloat("SFX Volume", 0.8f);
        }

        PlayMusic("Main Menu");
    }

    void Update()
    {
        

    }

    public void PlayMusic(string name)
    {
        for (int i = 0; i < Music.Length; i++)
        {
            if (Music[i].name == name)
            {
                MusicSource.clip = Music[i].clip;
                MusicSource.volume = MusicVolume;
                MusicSource.Play();
            }
        }


    }

    public void PlaySFX(string name)
    {
        for (int i = 0; i < SFX.Length; i++)
        {
            if (SFX[i].name == name)
            {
                SFXSource.volume = SFXVolume;
                StartCoroutine(PlaySFXCoroutine(SFX[i].clip));
            }
        }
    }

    private IEnumerator PlaySFXCoroutine(AudioClip clip)
    {
        AudioSource audioSource = Instantiate(SFXSource);
        audioSource.clip = clip;
        audioSource.Play();

        yield return new WaitForSeconds(clip.length + 0.1f);

        Destroy(audioSource.gameObject);
    }

    public void UpdateVolume()
    {
        MusicVolume = PlayerPrefs.GetFloat("Music Volume");
        MusicSource.volume = MusicVolume;

        SFXVolume = PlayerPrefs.GetFloat("SFX Volume");
        SFXSource.volume = SFXVolume;
    }
}
;