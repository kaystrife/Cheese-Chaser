using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {


    public static GameManager instance = null;
    public BoardManager boardScript;

    int level;

	// Use this for initialization
	void Awake () {

        if(instance ==null)
        {
            instance = this;
        }

        else if(instance!=this)
        {
            Destroy(gameObject);

        }


        boardScript = GetComponent<BoardManager>();
	}

    void LoadLevel()
    {
        level = GameRecordManager.instance.currentLevel;
    }

    void InitGame()
    {

        boardScript.SetupScene(level);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        LoadLevel();
        InitGame();
    }
}
