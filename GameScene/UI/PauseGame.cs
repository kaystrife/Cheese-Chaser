using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour {

    Button button;
    bool paused;

    public GameObject pausePanel;
    public DrawLine drawline;

	// Use this for initialization
	void Start () {

        paused = false;
        button = GetComponent<Button>();
        button.onClick.AddListener(Pause);
	}

    void Pause()
    {
        AudioManager.Play("Click");

        if (!paused)
        {
            Time.timeScale = 0;
            drawline.enabled = false;
            paused = true;
            pausePanel.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        AudioManager.Play("Click");
        Time.timeScale = 1;
        drawline.enabled = true;
        paused = false;
        pausePanel.SetActive(false);
    }

    public void BackToMenu()
    {
        AudioManager.Play("Click");
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuScene");
    }


}
