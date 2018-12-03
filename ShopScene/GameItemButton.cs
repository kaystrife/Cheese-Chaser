using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameItemButton : MonoBehaviour {

    public BuyItemMessagePanel messagePanel;

    public GameObject soldOutText;
    public GameObject lockPanel;
    public GameObject itemImage;

    public Text lockPanelText;
    public Text buttonName;
    public Text priceText;
    public Text accessoryName;
    public Text accessoryDescription;

    public Image currencyImage;
    public Image accesoryImage;

    public Sprite cheese, coin;

    public Accessory accessory;


    [SerializeField]
    private int ID;

    Button button;
    AccessoryManager am;
    GameRecordManager grm;
    BuyGameItemButton buyButton;

    // Use this for initialization
    void Start () {

        am = AccessoryManager.instance;
        grm = GameRecordManager.instance;
        button = GetComponent<Button>();
        buyButton = GetComponentInChildren<BuyGameItemButton>();

        Initialise();

	}

    void Initialise()
    {

        if (am != null)
        {
            accessory = am.allAccessory[ID];

            //check if the player is high level enough to buy this accessory
            if (grm.highestLevel >= accessory.unlockLevel)
            {
                buttonName.text = accessory.name;
                itemImage.SetActive(true);
                accesoryImage.sprite = accessory.accessoryImage;
                button.onClick.AddListener(ShowDescription);

                //check if player has already bought this item
                if (am.CheckUnlockedAccessory(ID))
                {
                    SoldOut();
                }
                else
                {
                    SetPrice();
                }

            }
            else //player is not high level enough to unlock this accessory
            {
                Locked();
            }
        }

        else
        {
            buttonName.text = "";
            accessory = am.allAccessory[0];
        }
    }

    void Locked()
    {
        buyButton.gameObject.SetActive(false);
        lockPanel.SetActive(true);
        lockPanelText.text = "Unlock at Level " + accessory.unlockLevel;
        button.onClick.AddListener(ShowNeedUnlock);
    }
	
    void ShowDescription()
    {
        accessoryName.text = accessory.name;
        accessoryDescription.text = accessory.description;
    }

    void ShowNeedUnlock()
    {
        accessoryName.text = "";
        accessoryDescription.text = "Reach level " + accessory.unlockLevel + " to unlock this accessory.";
    }

    void SetPrice()
    {
        if(buyButton!=null)
        {
            buyButton.Initialise(this);
            buyButton.gameObject.SetActive(true);
            priceText.text = accessory.price + "";

            if (accessory.isCheese)
            {
                currencyImage.sprite = cheese;
            }
            else if (accessory.isCoin)
            {
                currencyImage.sprite = coin;
            }
        }
        else
        {
            Debug.LogError("Can't find BuyGameItemButton script in child");
        }
       
    }

    void SoldOut()
    {
        soldOutText.SetActive(true);
        buyButton.gameObject.SetActive(false);
    }

    public void BuyItem()
    {
        bool bought = false;
        string currency;

        if(accessory.isCoin)
        {
            bought = grm.EarnOrSpendCoin(-accessory.price);
            currency = "coins";
        }else
        {
            bought = grm.EarnOrSpendCheese(-accessory.price);
            currency = "cheese";
        }

        if(bought == true)
        {
            BuyItemSuccessful();
        }
        else
        {
            messagePanel.NotEnoughMoney(currency);
        }
    }

    void BuyItemSuccessful()
    {
        am.UnlockAccessory(ID);
        SoldOut();
        messagePanel.AddedAccessory();
    }
}
