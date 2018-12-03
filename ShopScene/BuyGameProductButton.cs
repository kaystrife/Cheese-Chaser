using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyGameProductButton : MonoBehaviour {

    public BuyItemMessagePanel messagePanel;
    GameProductButton product;
    Button button;

	// Use this for initialization
	void Start () {
        button = GetComponent<Button>();

        button.onClick.AddListener(ShowMessage);
	}
	
	public void Initialise(GameProductButton product)
    {
        this.product = product;
    }

    void ShowMessage()
    {
        AudioManager.Play("Click");
        messagePanel.gameObject.SetActive(true);
        messagePanel.Initialise(product);
        messagePanel.ToBuyOrNot();
    }
}
