using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelOneMessage : LevelMessage {

    public GameObject pointer;
    public DrawLine drawLine;
    public GameObject DrawChanceText;

    private void Start()
    {
        messageText.text = messages[0];
    }

    public override void CheckEvent()
    {
        switch(messageIndex)
        {
            case 1:
                pointer.SetActive(true);
                break;
            case 4:
                pointer.SetActive(false);
                this.gameObject.SetActive(false);
                break;
        }
    }

    private void OnEnable()
    {
        drawLine.enabled = false;
        DrawChanceText.SetActive(false);
        targetPanel.SetActive(false);
    }

    private void OnDisable()
    {
        drawLine.enabled = true;
        targetPanel.SetActive(true);
    }
}
