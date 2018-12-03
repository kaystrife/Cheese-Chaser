using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighestLevelText : MonoBehaviour
{

    private const string HIGHEST_LEVEL = "HighestLevel";
    Text text;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();

        GameRecordManager.OnHighestLevelChanged += UpdateHighestLevel;


        //Highest level will only be shown if player has started the game
        if (PlayerPrefs.HasKey(HIGHEST_LEVEL))
        {
            int highestLevel = PlayerPrefs.GetInt(HIGHEST_LEVEL);
            text.text = "Highest level: " + highestLevel;
        }
        else
        {
            text.text = "";
        }
    }

    void UpdateHighestLevel(bool newRecord)
    {
       int highestLevel = PlayerPrefs.GetInt(HIGHEST_LEVEL);
        text.text = "Highest level: " + highestLevel;
    }

    private void OnDestroy()
    {
        GameRecordManager.OnHighestLevelChanged -= UpdateHighestLevel;
    }
}
