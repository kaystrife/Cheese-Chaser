using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelFourMessage : LevelMessage {

    public DrawLine drawLine;
    public DrawChanceText drawChanceText;
    public GameObject nextButton;
    public GameObject restartButton;
    public GameObject goToShopButton;
    public GameObject startGameButton;
    public GameObject brackets;

    bool shown = false;

    GameObject[] enemies;

    public override void CheckEvent()
    {
        switch (messageIndex)
        {
            case 1:
                brackets.SetActive(true);
                break;
            case 2:
                brackets.SetActive(false);
                break;
            case 4:
                this.gameObject.SetActive(false);
                break;
        }
    }

    public override void ShowNoInk()
    {
        base.ShowNoInk();
        drawLine.enabled = false;
        nextButton.SetActive(false);
        restartButton.SetActive(true);
    }

    public override void ShowNoLife()
    {
        base.ShowNoLife();
        nextButton.SetActive(false);
        restartButton.SetActive(true);
    }

    public override void ShowTutorialComplete()
    {
        base.ShowTutorialComplete();
        nextButton.SetActive(false);
        restartButton.SetActive(false);
        goToShopButton.SetActive(true);
        startGameButton.SetActive(true);
    }

    private void OnEnable()
    {
        drawLine.enabled = false;
        messageText.text = messages[0];
        drawChanceText.enabled = false;
        targetPanel.SetActive(false);

        if(!shown)
        {
            brackets.SetActive(true);
            shown = true;
        }


        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if(enemies!=null){

            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<EnemyWander>().enabled = false;
            }
        }
    }

    private void OnDisable()
    {
        targetPanel.SetActive(true);
        brackets.SetActive(false);

        if (drawLine != null)
        {
            drawLine.enabled = true;
            drawChanceText.enabled = true;
        }

        if (enemies != null)
        {

            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<EnemyWander>().enabled = true;
            }
        }
    }

    public void ClosePanel()
    {
        AudioManager.Play("Click");
        this.gameObject.SetActive(false);
        SceneManager.LoadScene("TutorialScene");
    }

    public void GoToShop()
    {
        AudioManager.Play("Click");
        Destroy(TutorialLevelManager.instance.gameObject);
        SceneManager.LoadScene("ShopScene");
    }

    public void GoToGame()
    {
        AudioManager.Play("Click");
        Destroy(TutorialLevelManager.instance.gameObject);
        SceneManager.LoadScene("GameScene");
    }
}
