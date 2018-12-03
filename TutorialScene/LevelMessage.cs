using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMessage : MonoBehaviour {

    [SerializeField]
    [TextArea(0, 4)]
    protected string[] messages;
    [SerializeField]
    protected int messageIndex;

    public Text messageText;
    public GameObject targetPanel;

    public void NextMessage()
    {
        AudioManager.Play("Click");
        messageIndex++;

        if (messageIndex < messages.Length)
        {
            messageText.text = messages[messageIndex];
        }

        CheckEvent();
    }

    public virtual void CheckEvent()
    {
        Debug.Log("Check Event");
    }

    public virtual void ShowNoInk()
    {
        AudioManager.Play("Lose");
        messageText.text = "Oops, no ink! \n Don't give up, try again!";
    }

    public virtual void ShowNoLife()
    {
        AudioManager.Play("Lose");
        messageText.text = "Game Over \n You have no lives left :(";
    }

    public virtual void ShowTutorialComplete()
    {
        messageText.text = "Tutorial complete! \n You've received  <color=yellow>50 cheese</color> and <color=yellow>20coins</color> as rewards!";
    }
}
