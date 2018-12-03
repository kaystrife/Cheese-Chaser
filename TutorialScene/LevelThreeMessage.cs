using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelThreeMessage : LevelMessage {

    public DrawLine drawLine;
    public DrawChanceText drawChanceText;
    public GameObject nextButton;
    public GameObject restartButton;

  
    public override void CheckEvent()
    {
        switch (messageIndex)
        {
            case 5:
                this.gameObject.SetActive(false);
                break;
        }
    }

    public override void ShowNoInk()
    {
        base.ShowNoInk();
        drawLine.enabled = false;
        nextButton.SetActive(false);
        restartButton.SetActive(true);
    }

    private void OnEnable()
    {
        if (messageIndex == 0)
        {
            drawLine.enabled = false;
            messageText.text = messages[0];
            CheckEvent();
        }

        drawChanceText.enabled = false;
        targetPanel.SetActive(false);
    }

    private void OnDisable()
    {
        if (drawLine != null)
        {
            drawLine.enabled = true;
            drawChanceText.enabled = true;
        }
        targetPanel.SetActive(true);
    }

    public void ClosePanel()
    {
        AudioManager.Play("Click");
        SceneManager.LoadScene("TutorialScene");
    }
}
