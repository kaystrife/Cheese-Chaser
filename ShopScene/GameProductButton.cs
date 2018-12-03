using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProductButton : MonoBehaviour {

    private const string INCREASE_MAX = "IncreaseMax";

    Button button;
    BuyGameProductButton buyButton;
    GameRecordManager grm;

    public Product product;
    public BuyItemMessagePanel messagePanel;

    public Text productName;
    public Text productDescription;
    public Text priceText;
    public Text buttonName;
   
    public GameObject soldOut;

    public Sprite cheeseCurrency;
    public Sprite coinCurrecny;
    public Sprite usdCurrency;

    public Image productCurrency;
    public Image productImage;


    private void Start()
    {
        button = GetComponent<Button>();
        buyButton = GetComponentInChildren<BuyGameProductButton>();
        grm = GameRecordManager.instance;

        button.onClick.AddListener(ShowDescription);

        if(buyButton!=null)
        {
            buyButton.Initialise(this);
            Initialise();
        }
    }

    void Initialise()
    {
        buttonName.text = product.name;
        productImage.sprite = product.productImage;

        if (product.increaseMax)
        {
            if(PlayerPrefs.HasKey(INCREASE_MAX))
            {
                SoldOut();
                return;
            }
        }

        ShowPrice();
       
    }

    void ShowDescription()
    {
        productName.text = product.productName;
        productDescription.text = product.productDescription;

    }

    void ShowPrice()
    {
        buyButton.gameObject.SetActive(true);
        priceText.text = "" + product.payAmount;

        if (product.realMoney)
        {
            productCurrency.sprite = usdCurrency;
        }
        else if(product.pCheese)
        {
            Debug.Log("here");
            productCurrency.sprite = cheeseCurrency;
        }
        else
        {
            productCurrency.sprite = coinCurrecny;
        }

    }
   
    void SoldOut()
    {
        soldOut.SetActive(true);
        buyButton.gameObject.SetActive(false);
    }

    public void BuyProduct()
    {
        bool bought = false;
        string currency = "";

        if(product.realMoney)
        {
            //pass the product ID to the in app purchase manager
            return;
        }
        else if(product.pCheese)
        {
            bought = grm.EarnOrSpendCheese(-(int)product.payAmount);
            currency = "cheese";
        }
        else
        {
            bought = grm.EarnOrSpendCoin(-(int)product.payAmount);
            currency = "coins";
        }

        if(bought)
        {
            ReceiveProduct();
        }else
        {
            messagePanel.NotEnoughMoney(currency);
        }
    }

    void ReceiveProduct()
    {
        if(product.rCoin)
        {
            grm.EarnOrSpendCoin(product.receiveAmount);
        }
        else if(product.rCheese)
        {
            grm.EarnOrSpendCheese(product.receiveAmount);
        }
        else if(product.rLife)
        {
            grm.EarnOrSpendLife(product.receiveAmount);
        }
        else //increase equipment slot
        {
            IncreaseEquipmentSlot();
        }

        messagePanel.ThankYou();
    }

    void IncreaseEquipmentSlot()
    {
        AccessoryManager.instance.IncreaseMaxAccessory();
        SoldOut();
    }
}
