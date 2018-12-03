using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialLevelManager : MonoBehaviour {

    public static TutorialLevelManager instance = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }


    public int tutorialLevel;

    [SerializeField]
    int[] requiredCheeseNum;

    int currentCheese;
    bool levelCompleted = false;


    public bool[] levelMessageShown;

    private void Start()
    {
        CheckLevelComplete();
    }

    public bool ToNextLevel()
    {
        //check if level is complete
        //if yes, increase tutorial level -> reload scene
        if(levelCompleted)
        {
            tutorialLevel++;

            if(tutorialLevel>3)
            {
                GameRecordManager.instance.CompleteTutorial();
                TutorialBoardManager.instance.ShowTutorialCompleteMessage();
                //Destroy(gameObject);
                return true;
            }

            Debug.Log("To next level");
            SceneManager.LoadScene("TutorialScene");

            return true;
        }

        
        return false;
    }

    public void GetCheese(int amount)
    {
        currentCheese += amount;
        CheckLevelComplete();
    }

    void CheckLevelComplete()
    {
        if(currentCheese >= requiredCheeseNum[tutorialLevel])
        {
            levelCompleted = true;

            Animator anim = GameObject.FindGameObjectWithTag("TutorialExit").GetComponent<Animator>();
            anim.SetBool("exitOn", true);
        }

        Debug.Log("Level completed");
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
        TutorialBoardManager.instance.SetupScene(tutorialLevel);
        currentCheese = 0;
        levelCompleted = false;
        CheckLevelComplete();

        if(!levelMessageShown[tutorialLevel])
        {
            TutorialBoardManager.instance.ShowLevelMessage(tutorialLevel);
            levelMessageShown[tutorialLevel] = true;
        }
    }

}
