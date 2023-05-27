using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource SFXSource;

    [SerializeField] private Audio[] SFX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(string name)
    {
        for (int i = 0; i < SFX.Length; i++)
        {
            if (SFX[i].name == name)
            {
                SFXSource.volume = PlayerPrefs.GetFloat("SFX Volume");
                SFXSource.clip = SFX[i].clip;
                SFXSource.Play();
                //StartCoroutine(PlaySFXCoroutine(SFX[i].clip));
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

}
