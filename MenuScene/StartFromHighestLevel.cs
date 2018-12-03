using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class StartFromHighestLevel : MonoBehaviour {

    Button button;

    public static event Action<bool> OnNotEnoughCheese = delegate { };

	// Use this for initialization
	void Start () {

        button = GetComponent<Button>();

        if(button!=null)
        {
            button.onClick.AddListener(StartHighestLevel);
        }
	}
	
	void StartHighestLevel()
    {
        if(GameRecordManager.instance.StartFromHighestLevel())
        {
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            //show you don't have enough cheese
            OnNotEnoughCheese(true);
        }
    }
}
