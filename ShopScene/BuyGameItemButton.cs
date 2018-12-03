using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyGameItemButton : MonoBehaviour {

    Button button;
    GameItemButton item;

    public BuyItemMessagePanel messagePanel;

	// Use this for initialization
	void Start () {
    
        button = GetComponent<Button>();
        button.onClick.AddListener(BuyOrNot);
	}
	
    public void Initialise(GameItemButton item)
    {
        this.item = item;
    }

    void BuyOrNot()
    {
        AudioManager.Play("Click");
        messagePanel.gameObject.SetActive(true);
        messagePanel.Initialise(item);
        messagePanel.ToBuyOrNot();
    }
}
