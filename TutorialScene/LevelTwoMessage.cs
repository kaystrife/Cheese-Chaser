using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelTwoMessage : LevelMessage {


    public DrawLine drawLine;
    public DrawChanceText drawChanceText;
    public GameObject bracket;
    public GameObject nextButton;
    public GameObject restartButton;


    public override void CheckEvent()
    {
        switch(messageIndex)
        {
            case 1:
                bracket.SetActive(true);
                break;
            case 6:
                bracket.SetActive(false);
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
        drawChanceText.enabled = false;
        targetPanel.SetActive(false);

        if(drawLine!=null)
        {
            drawLine.enabled = false;
        }

        if(messageIndex==0)
        {
            messageText.text = messages[0];
            CheckEvent();
        }
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
