using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteButton : MonoBehaviour {

    bool muted;
    public GameObject mute, unmute;
    public AudioSource bgm;

	// Use this for initialization
	void Start () {

        muted = AudioManager.muted;

        if(!muted)
        {
            unmute.SetActive(true);
            mute.SetActive(false);
        }else
        {
            unmute.SetActive(false);
            mute.SetActive(true);
        }
	}
	
    public void MuteOrUnmute()
    {
        if(!muted)
        {
            unmute.SetActive(false);
            mute.SetActive(true);
            MuteSound();
        }else
        {
            unmute.SetActive(true);
            mute.SetActive(false);
            UnmuteSound();
        }
    }

    void MuteSound()
    {
        AudioManager.changeSFXVolume(0);
        BGM.ChangeBGMVolume(0);
        muted = true;
    }

    void UnmuteSound()
    {
        AudioManager.changeSFXVolume(1);
        BGM.ChangeBGMVolume(1);
        muted = false;
    }
}
