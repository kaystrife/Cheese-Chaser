using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    public GameObject startLevelPanel;
    public Text message;
    public Text highestLevelText;
    public GameObject levelOneBtn, highestLevelBtn, cancelBtn, okayBtn;

    private const string COMPLETE_TUTORIAL = "CompleteTutorial";

	
	public void ToGame () {

        AudioManager.Play("Click");

        if(PlayerPrefs.GetInt(COMPLETE_TUTORIAL) == 1)
        {
            if(GameRecordManager.instance.currentLevel > 1 || GameRecordManager.instance.highestLevel == 1)
            {
                SceneManager.LoadScene("GameScene");
            }
            else
            {
                //ask if the player wants to start from level 1 or the highest level (200 cheese)
                StartLevelOption();
            }
        }
        else
        {
            Debug.Log("Go to tutorial");
            SceneManager.LoadScene("TutorialScene");
        }

	}

    void StartLevelOption()
    {
        startLevelPanel.SetActive(true);
        message.text = "Do you wish to start from...";
        highestLevelText.text = "Level " + GameRecordManager.instance.highestLevel + "\n (200 cheese)";
        levelOneBtn.SetActive(true);
        highestLevelBtn.SetActive(true);
        cancelBtn.SetActive(true);
        okayBtn.SetActive(false);
    }

    void ShowNotEnoughCheese(bool b)
    {
        message.text = "You Don't have enough cheese!";
        levelOneBtn.SetActive(false);
        highestLevelBtn.SetActive(false);
        cancelBtn.SetActive(false);
        okayBtn.SetActive(true);
    }

    private void OnEnable()
    {
        StartFromHighestLevel.OnNotEnoughCheese += ShowNotEnoughCheese;
    }

    private void OnDisable()
    {
        StartFromHighestLevel.OnNotEnoughCheese -= ShowNotEnoughCheese;
    }

    public void ClosePanel()
    {
        startLevelPanel.SetActive(false);
    }
}
