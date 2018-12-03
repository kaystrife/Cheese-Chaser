using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class TutorialBoardManager : MonoBehaviour {

    #region singleton
    public static TutorialBoardManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion


    //can change according to the size of the game board
    readonly int columns = 8;
    readonly int rows = 10;

    public GameObject exit;
    public GameObject[] floorTiles;
    public GameObject[] outerWallTiles;

    public GameObject pointer;
    public GameObject[] levels;
    public GameObject[] levelMessage;

    int currentLevel;

    //instantiates will be children of this boardHolder
    private Transform boardHolder;

    //call by tutorial level manager to set up the board of that level

    //for the setup of outer wall and floor
    void BoardSetUp()
    {
        boardHolder = new GameObject("Board").transform;
        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

                //positions where outer wall should be placed instead of floor
                if (x == -1 || x == columns || y == -1 || y == rows)
                {
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                }

                var clone = (GameObject)Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity);
                clone.transform.SetParent(boardHolder);
            }
        }
    }

    //will be called by GameManager when it is time to set up the board
    public void SetupScene(int level)
    {
        currentLevel = level;
        BoardSetUp();
        Instantiate(exit, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);
        levels[level].SetActive(true);

        DrawLine.TutorialNoInkLeft += ShowNoInkMessage;
    }

    public void ShowLevelMessage(int level)
    {
        levelMessage[level].SetActive(true);
    }

    public void ShowNoInkMessage(bool b)
    {
        if(currentLevel>0)
        {
            levelMessage[currentLevel].SetActive(true);
            levelMessage[currentLevel].GetComponent<LevelMessage>().ShowNoInk();
            Debug.Log("Show panel in level " + currentLevel);
        }
    }

    public void ShowNoLifeMessage()
    {
        levelMessage[currentLevel].SetActive(true);
        levelMessage[currentLevel].GetComponent<LevelMessage>().ShowNoLife();
        Debug.Log("Show panel in level " + currentLevel);
    }

    private void OnDestroy()
    {
        DrawLine.TutorialNoInkLeft -= ShowNoInkMessage;
    }

    public void ShowTutorialCompleteMessage()
    {
        GameRecordManager.instance.EarnOrSpendCheese(50);
        GameRecordManager.instance.EarnOrSpendCoin(20);
        GameRecordManager.instance.CompleteTutorial();

        levelMessage[currentLevel].SetActive(true);
        levelMessage[currentLevel].GetComponent<LevelMessage>().ShowTutorialComplete();
    }
}
