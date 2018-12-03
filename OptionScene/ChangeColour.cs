using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColour : MonoBehaviour {

    public string colour;
    public Text inkColourText;

    Button thisButton;
    Image thisImage;

    private void Start()
    {
        thisButton = GetComponent<Button>();
        thisImage = GetComponent<Image>();

        thisButton.onClick.AddListener(ChangeInkColour);
    }

    void ChangeInkColour()
    {
        AudioManager.Play("Click");
        inkColourText.color = thisImage.color;
        GameRecordManager.instance.SetLineColour(colour);
    }
}
