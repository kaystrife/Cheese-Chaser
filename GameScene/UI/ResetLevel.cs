using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour {
   
    Button button;

    // Use this for initialization
    void Start () {

        button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(ResetAndGoToMenu);
        }

    }
	
	void ResetAndGoToMenu()
    {
        AudioManager.Play("Click");
        GameRecordManager.instance.ResetLifeAndLevel();
        SceneManager.LoadScene("MenuScene");
    }
}
