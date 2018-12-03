﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDrawChanceText : MonoBehaviour {

    public DrawLine drawLine;
    Text text;

	// Use this for initialization
	void Start () {

        text = GetComponent<Text>();
        text.text = "";
	}
	
	// Update is called once per frame
	void Update () {

        text.text = "Draw chance: " + drawLine.drawChance;
	}
}
