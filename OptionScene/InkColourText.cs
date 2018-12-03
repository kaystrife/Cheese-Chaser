using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InkColourText : MonoBehaviour {

    Text thisText;
    const string LINE_COLOUR = "LineColour";

    public Color red, blue, yellow, green, purple, pink;

    // Use this for initialization
    void Start () {

        thisText = GetComponent<Text>();
        CheckColour();
	}
	
	void CheckColour()
    {
        if(PlayerPrefs.HasKey(LINE_COLOUR))
        {
            string colour = PlayerPrefs.GetString(LINE_COLOUR);

            switch(colour)
            {
                case "red":
                    thisText.color = red;
                    break;
                case "blue":
                    thisText.color = blue;
                    break;
                case "yellow":
                    thisText.color = yellow;
                    break;
                case "green":
                    thisText.color = green;
                    break;
                case "purple":
                    thisText.color = purple;
                    break;
                case "pink":
                    thisText.color = pink;
                    break;
                case "black":
                    thisText.color = Color.black;
                    break;
                case "white":
                    thisText.color = Color.white;
                    break;
                default:
                    thisText.color = Color.white;
                    break;
            }
        }
        else
        {
            thisText.color = Color.white;
        }
    }
}
