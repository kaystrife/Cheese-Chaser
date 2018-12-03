using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    #region singleton

    public static LevelManager instance = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        else if (instance!=this)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    public DrawLine drawLine;
    public LevelCompletePanel panel;

  
    int totalCheese;
    int currentCheese;
    private bool levelComplete;

    BoardManager bm;
    GameRecordManager grm;
    AccessoryManager am;
    Animator anim;
    CharacterWalk player;

    private void Start()
    {
        bm = BoardManager.instance;
        grm = GameRecordManager.instance;
        am = AccessoryManager.instance;

        anim = GameObject.FindGameObjectWithTag("Exit").GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterWalk>();

        Cheese.OnCheeseEaten += SetCurrentCheese;
        GameRecordManager.OnGameOver += GameOver;
        drawLine.NoInkLeft += NoInk;

        levelComplete = false;
        currentCheese = 0;
        totalCheese = bm.cheeseNum;
        Debug.Log("Total Cheese" + totalCheese);
    }

    public void SetCurrentCheese(int cheese)
    {
        currentCheese += cheese;
        CheckCompleteLevel();
        Debug.Log("Current cheese" + currentCheese);
    }
	
	void CheckCompleteLevel()
    {
        if(!levelComplete)
        {
            if (currentCheese >= (int)Mathf.Ceil(totalCheese * 0.5f)) 
            {
                levelComplete = true;
                anim.SetBool("exitOn", true);
                Debug.Log("Exit open");
            }
        }
    }

    public void ToNextLevel()
    {
        if(levelComplete)
        {
            drawLine.enabled = false;
            player.enabled = false;
            DestroyEnemies();

            //check if there is any bonus for the cheese
            CheckCheeseBonus();

            //increase number of cheese in player's wallet
            grm.EarnOrSpendCheese(currentCheese);

            //show message panel
            panel.gameObject.SetActive(true);
            panel.ShowResults(currentCheese);

            //update current level in the game record
            grm.ProceedLevel();
        }
    }

    void DestroyEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies != null)
        {
            foreach(GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
        }
    }

    public void GameOver(bool b)
    {
        drawLine.enabled = false;
        DestroyEnemies();

        panel.gameObject.SetActive(true);
        panel.ShowGameOver();
    }

    public void NoInk(bool b)
    {
        bool lifeLeft = grm.EarnOrSpendLife(-1);

        if(lifeLeft==false)
        {
            //player has no lives left -> game over
            return;
        }

        drawLine.enabled = false;
        DestroyEnemies();

        panel.gameObject.SetActive(true);
        panel.ShowNoInk();
    }

    void CheckCheeseBonus()
    {
        if(currentCheese == totalCheese)
        {
            currentCheese *= 2;
            panel.gotAllCheese = true;
        }

        foreach(Accessory accessory in am.equippedAccessory)
        {
            if(accessory.cheeseBonus >0)
            {
                int temp = (int)(currentCheese * accessory.cheeseBonus);
                currentCheese = temp;
            }
        }
    }

    private void OnDestroy()
    {
        Cheese.OnCheeseEaten -= SetCurrentCheese;
        GameRecordManager.OnGameOver -= GameOver;
        drawLine.NoInkLeft -= NoInk;
    }

}
