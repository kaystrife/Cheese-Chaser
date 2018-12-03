using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameRecordManager : MonoBehaviour {

    public static GameRecordManager instance = null;

    private const string PLAYER_LIFE = "PlayerLife";
    private const string CHEESE_AMOUNT = "CheeseAmount";
    private const string COIN_AMOUNT = "CoinAmount";
    private const string HIGHEST_LEVEL = "HighestLevel";
    private const string CURRENT_LEVEL = "CurrentLevel";
    private const string COMPLETE_TUTORIAL = "CompleteTutorial";
    private const string NO_ADS = "NoAds";
    private const string LINE_COLOUR = "LineColour";

    [SerializeField]
    private int INITIAL_LIFE = 10;
    [SerializeField]
    private int INITIAL_CHEESE = 0;
    [SerializeField]
    private int INITIAL_COIN = 0;

    private int _playerLife;
    private static int _cheeseAmount;
    private static int _coinAmount;
    private int _highestLevel;
    private int _currentLevel;

    public int playerLife;
    public int cheeseAmount;
    public int coinAmount;
    public int highestLevel;
    public int currentLevel;

    public static event Action<bool> OnWalletChange = delegate { };
    public static event Action<bool> OnHighestLevelChanged = delegate { };
    public static event Action<bool> OnGameOver = delegate { };

    // Use this for initialization
    void Awake () {

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        Initialise();

    }

    void Initialise()
    {
        GetLifeRecord();
        GetCheeseRecord();
        GetCoinRecord();
        GetHighestLevel();
        GetPreviousLevel();

        playerLife = _playerLife;
        cheeseAmount = _cheeseAmount;
        coinAmount = _coinAmount;
        highestLevel = _highestLevel;
        currentLevel = _currentLevel;
    }

    public bool EarnOrSpendCheese(int amount)
    {
        int temp = cheeseAmount + amount;
        bool spent = false;

        //not enough cheese to spend
        if(temp < 0)
        {
            spent = false;
        }
        else //enough cheese to spend
        {
            spent = true;
            _cheeseAmount = temp;
            cheeseAmount = _cheeseAmount;
            PlayerPrefs.SetInt(CHEESE_AMOUNT, cheeseAmount);
        }

        OnWalletChange(spent);
        return spent;
    }

    public bool EarnOrSpendCoin(int amount)
    {
        int temp = coinAmount + amount;
        bool spent = false;

        //not enough cheese to spend
        if (temp < 0)
        {
            spent = false;
        }
        else //enough cheese to spend
        {
            spent = true;
            _coinAmount = temp;
            coinAmount = _coinAmount;
            PlayerPrefs.SetInt(COIN_AMOUNT, coinAmount);
        }

        OnWalletChange(spent);
        return spent;
    }

    public bool EarnOrSpendLife(int amount)
    {
        //return false if no more lives left

        _playerLife += amount;

        if(_playerLife <= 0)
        {
            OnGameOver(true);
            _playerLife = 0;
            playerLife = _playerLife;
            return false;
        }

        playerLife = _playerLife;
        PlayerPrefs.SetInt(PLAYER_LIFE, _playerLife);
        return true;
    }

    public void SubmitLevel(int level)
    {
        bool newRecord = false;

        if (PlayerPrefs.HasKey(HIGHEST_LEVEL))
        {
            int highestLevelRecord = PlayerPrefs.GetInt(HIGHEST_LEVEL);

            if(level > highestLevelRecord)
            {
                PlayerPrefs.SetInt(HIGHEST_LEVEL, level);
                _highestLevel = level;
                highestLevel = _highestLevel;
                newRecord = true;
            }
        }else
        {
            PlayerPrefs.SetInt(HIGHEST_LEVEL, level);
            _highestLevel = level;
            highestLevel = _highestLevel;
            newRecord = true;
        }

        OnHighestLevelChanged(newRecord);
    }

    public void ProceedLevel()
    {
        _currentLevel++;
        currentLevel = _currentLevel;
        PlayerPrefs.SetInt(CURRENT_LEVEL, _currentLevel);
        SubmitLevel(_currentLevel);
    }

    public void CompleteTutorial()
    {
        PlayerPrefs.SetInt(COMPLETE_TUTORIAL, 1);
    }

    public void NoAds()
    {
        PlayerPrefs.SetInt(NO_ADS, 1);
    }

    public void SetLineColour(string colour)
    {
        PlayerPrefs.SetString(LINE_COLOUR, colour);
    }

    //=========================================================================================================
    // LOAD DATA
    //=========================================================================================================
    void GetLifeRecord()
    {
        string key = PLAYER_LIFE;

        if (!PlayerPrefs.HasKey(key))
        {
            _playerLife = INITIAL_LIFE;
            PlayerPrefs.SetInt(key, _playerLife);
        }
        else
        {
            _playerLife = PlayerPrefs.GetInt(key);
        }
    }

    void GetCheeseRecord()
    {
        string key = CHEESE_AMOUNT;

        if (!PlayerPrefs.HasKey(key))
        {
            _cheeseAmount = INITIAL_CHEESE;
            PlayerPrefs.SetInt(key, _cheeseAmount);
        }
        else
        {
            _cheeseAmount = PlayerPrefs.GetInt(key);
        }
    }

    void GetCoinRecord()
    {
        string key = COIN_AMOUNT;

        if (!PlayerPrefs.HasKey(key))
        {
            _coinAmount = INITIAL_COIN;
            PlayerPrefs.SetInt(key, _coinAmount);
        }
        else
        {
            _coinAmount = PlayerPrefs.GetInt(key);
        }
    }

    void GetHighestLevel()
    {
        string key = HIGHEST_LEVEL;

        if (!PlayerPrefs.HasKey(key))
        {
            _highestLevel = 1;
        }
        else
        {
            _highestLevel = PlayerPrefs.GetInt(key);
        }
    }

    void GetPreviousLevel()
    {
        string key = CURRENT_LEVEL;

        if (!PlayerPrefs.HasKey(key))
        {
            _currentLevel = 1;
            PlayerPrefs.SetInt(key, _currentLevel);
        }else
        {
            _currentLevel = PlayerPrefs.GetInt(key);
        }
    }

    //=========================================================================================================
    // RESET DATA AFTER GAME OVER
    //=========================================================================================================

    public void ResetLifeAndLevel()
    {
        Debug.Log("Game Over");
        OnGameOver(true);

        _playerLife = INITIAL_LIFE;
        playerLife = _playerLife;
        PlayerPrefs.SetInt(PLAYER_LIFE, _playerLife);

        _currentLevel = 1;
        currentLevel = _currentLevel;
        PlayerPrefs.SetInt(CURRENT_LEVEL, 1);
    }

    public bool StartFromHighestLevel()
    {
        if(EarnOrSpendCheese(-200))
        {
            _currentLevel = _highestLevel;
            currentLevel = _currentLevel;
            PlayerPrefs.SetInt(CURRENT_LEVEL, _currentLevel);
            return true;
        }
       
        return false;
    }

}
