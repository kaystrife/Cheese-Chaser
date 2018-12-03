using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawChanceText : MonoBehaviour {

    Text drawChanceText;

    public DrawLine drawLine;

    public Image[] penImage;
    public Sprite pen, empty;

	// Use this for initialization
	void Start () {

        drawChanceText = GetComponent<Text>();
	}

    void Update()
    {
        int drawChance = drawLine.drawChance;

        for (int i = 0; i < penImage.Length; i++)
        {
            if(i<drawChance)
            {
                penImage[i].sprite = pen;
            }
            else
            {
                penImage[i].sprite = empty;
            }
        }
    }
}
