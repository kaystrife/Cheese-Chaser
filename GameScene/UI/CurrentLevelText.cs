using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentLevelText : MonoBehaviour {

    Text text;
    GameRecordManager grm;

    // Use this for initialization
    void Start()
    {

        text = GetComponent<Text>();
        grm = GameRecordManager.instance;
    }

    // Update is called once per frame
    void Update()
    {

        text.text = "Level " + grm.currentLevel;
    }
}
