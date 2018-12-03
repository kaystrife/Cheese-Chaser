using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPauseButton : MonoBehaviour {

    Button button;
    bool isPaused;

    public Image image;
    public Sprite pause, play;
    public GameObject pauseText;

	// Use this for initialization
	void Start () {

        isPaused = false;

        button = GetComponent<Button>();
        button.onClick.AddListener(PauseOrPlay);
	}
	
    void PauseOrPlay()
    {
        AudioManager.Play("Click");

        if(isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            image.sprite = pause;
            pauseText.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            image.sprite = play;
            pauseText.SetActive(true);
        }
    }
}
