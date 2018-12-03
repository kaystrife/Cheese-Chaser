using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletePanel : MonoBehaviour {

    private const string NO_ADS = "NoAds";

    [SerializeField]
    string[] congrats;

    public bool gotAllCheese = false;

    public Text congratsText;
    public Text cheeseText;

    public GameObject nextLevelBtn, menuBtn, restartLevelBtn, resetLevelBtn;
    public VideoAds videoAd;
    public CharacterWalk player;

    public void ShowResults(int cheeseGet)
    {
        ShowAdOrNot();
        congratsText.text = RandomCongrats();
        cheeseText.text = "You've earned " + cheeseGet + " cheese!";

        if(gotAllCheese == true)
        {
            cheeseText.text += "\n [Bonus for getting all the cheese in this level!]";
        }

        nextLevelBtn.SetActive(true);
        menuBtn.SetActive(true);
        restartLevelBtn.SetActive(false);
        resetLevelBtn.SetActive(false);
    }

    void ShowAdOrNot()
    {
        int currentLv = GameRecordManager.instance.currentLevel;

        if (currentLv % 10 == 0)
        {
            videoAd.ShowAd();
        }
    }

    string RandomCongrats()
    {
        int rand = Random.Range(0, congrats.Length);
        return congrats[rand];
    }

    public void ShowGameOver()
    {
        AudioManager.Play("Lose");
        player.enabled = false;
        congratsText.text = "Game Over";
        cheeseText.text = "You have no more lives left...";
        nextLevelBtn.SetActive(false);
        restartLevelBtn.SetActive(false);
        menuBtn.SetActive(false);
        resetLevelBtn.SetActive(true);
    }

    public void ShowNoInk()
    {
        AudioManager.Play("Lose");
        player.enabled = false;
        congratsText.text = "Oops, no ink!";
        cheeseText.text = "You have reached the draw chance limit. \n One life will be deducted.";
        restartLevelBtn.SetActive(true);
        menuBtn.SetActive(true);
        nextLevelBtn.SetActive(false);
        resetLevelBtn.SetActive(false);
    }
}
