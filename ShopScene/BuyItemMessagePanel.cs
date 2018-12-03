using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyItemMessagePanel : MonoBehaviour {

    GameItemButton item;
    GameProductButton product;

    public Button yesButton;
    public Button noButton;
    public Button okButton;
    public Text messageText;

    bool buyItem;
    bool buyProduct;


	void Awake() {

        yesButton.onClick.AddListener(Buy);
        noButton.onClick.AddListener(Close);
        okButton.onClick.AddListener(Close);

        buyItem = false;
        buyProduct = false;

	}
	
    public void Initialise(GameItemButton item)
    {
        this.item = item;
        buyItem = true;
        buyProduct = false;
    }

    public void Initialise(GameProductButton product)
    {
        this.product = product;
        buyProduct = true;
        buyItem = false;
    }

    void Buy()
    {
        AudioManager.Play("Click");

        if (buyItem)
        {
            item.BuyItem();
        }
        else if(buyProduct)
        {
            product.BuyProduct();
        }

    }

    void Close()
    {
        AudioManager.Play("Click");
        this.gameObject.SetActive(false);
    }

    public void ToBuyOrNot()
    {
        if(buyItem)
        {
            ShowItemMessage();
        }
        else if(buyProduct)
        {
            ShowProductMessage();
        }

        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);
        okButton.gameObject.SetActive(false);

    }

    void ShowItemMessage()
    {
        Accessory toBuyItem = item.accessory;
        string currency = "";

        if (toBuyItem.isCheese)
        {
            currency = "Cheese";
        }
        else
        {
            currency = "Coins";
        }

        messageText.text = "Do you wish to spend " + "<color=red>" + toBuyItem.price + " " + currency + "</color>" + " on this item?";
    }

    void ShowProductMessage()
    {
        Product toBuyProduct = product.product;
        string currency = "";

        if(toBuyProduct.realMoney)
        {
            currency = "USD";
        }
        else if(toBuyProduct.pCheese)
        {
            currency = "Cheese";
        }
        else if(toBuyProduct.pCoin)
        {
            currency = "Coins";
        }

        messageText.text = "Do you wish to spend " + "<color=red>" + toBuyProduct.payAmount + " " +currency + "</color>" + " on this item?";
    }

    public void NotEnoughMoney(string currency)
    {
        messageText.text = "You don't have enough " + currency;

        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
        okButton.gameObject.SetActive(true);
    }

    public void AddedAccessory()
    {
        AudioManager.Play("Thanks");
        messageText.text = "<size=50>" + "Thank you for your purchase! \n The item has been added to your <color=yellow>Accessory</color>!" + "</size>";
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
        okButton.gameObject.SetActive(true);
    }

    public void ThankYou()
    {
        AudioManager.Play("Thanks");
        messageText.text = "Thank you for your purchase! \n Have a good day!";
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
        okButton.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        buyProduct = false;
        buyItem = false;
    }
}
