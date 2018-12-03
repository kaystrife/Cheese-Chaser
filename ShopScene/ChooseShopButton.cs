using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseShopButton : MonoBehaviour {

    public string shopName;
    [TextArea(0,4)]
    public string shopDescription;

    public GameObject activateContent;
    public GameObject deactivateContent;

    public Text shopNameText;
    public Text shopDescriptionText;

    Button button;

	// Use this for initialization
	void Start () {

        button = GetComponent<Button>();
        button.onClick.AddListener(ShowShop);
	}
	
	void ShowShop()
    {
        AudioManager.Play("Click");
        deactivateContent.SetActive(false);
        activateContent.SetActive(true);
        shopNameText.text = shopName;
        shopDescriptionText.text = shopDescription;
    }

}
