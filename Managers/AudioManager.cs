using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    #region Don't Destroy On Load
    private static bool created = false;

    private void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }

        else
            Destroy(gameObject);
    }
    #endregion

    public static AudioClip click, hurt, protect, swallow, thanks, lose;
    static AudioSource audioSource;
    public static float sfxVolume;
    public static bool muted;


    void Start()
    {

        audioSource = gameObject.GetComponent<AudioSource>();

        click = Resources.Load<AudioClip>("Click");
        hurt = Resources.Load<AudioClip>("Hurt");
        protect = Resources.Load<AudioClip>("Protect");
        swallow = Resources.Load<AudioClip>("Swallow");
        thanks = Resources.Load<AudioClip>("Thanks");
        lose = Resources.Load<AudioClip>("Lose");
        sfxVolume = 1.0f;
        muted = false;
    }

    public static void changeSFXVolume(float volume)
    {
        sfxVolume = volume;

        if(volume>0)
        {
            muted = false;
        }else
        {
            muted = true;
        }
    }

    public static void Play(string clip)
    {
        audioSource.volume = sfxVolume;

        switch (clip)
        {
            case "Click":
                audioSource.PlayOneShot(click);
                break;
            case "Hurt":
                audioSource.PlayOneShot(hurt);
                break;
            case "Protect":
                audioSource.PlayOneShot(protect);
                break;
            case "Swallow":
                audioSource.PlayOneShot(swallow);
                break;
            case "Thanks":
                audioSource.PlayOneShot(thanks);
                break;
            case "Lose":
                audioSource.PlayOneShot(lose);
                break;
        }
    }

}
