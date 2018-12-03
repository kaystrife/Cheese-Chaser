using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHighestLevelText : MonoBehaviour {

    Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();

        text.text = "Highest level: " + GameRecordManager.instance.highestLevel;
	}
}
