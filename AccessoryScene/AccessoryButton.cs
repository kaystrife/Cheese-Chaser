using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccessoryButton : MonoBehaviour {

    public int ID;
    public EquipButton equipButton;
    public UnequipButton unequipButton;

    public GameObject shopButton;
    public GameObject equipped;

    public Text accessoryDescription, accessoryName;

    bool isEquipped;
    bool isUnlocked;

    Button button;
    Image image;
    AccessoryManager am;

    // Use this for initialization
    void Start () {

        button = GetComponent<Button>();
        image = GetComponent<Image>();
        am = AccessoryManager.instance;

        AccessoryManager.OnChangedAccessory += CheckIfBeingChanged;

        Initialise();

        if(button!=null)
        {
            button.onClick.AddListener(ShowDescription);
        }
	}
	
    void Initialise()
    {
        if (am != null)
        {
            isUnlocked = am.CheckUnlockedAccessory(ID);

            if (isUnlocked)
            {
                image.sprite = am.allAccessory[ID].accessoryImage;
            }

            isEquipped = am.CheckAccessoryEquipped(ID);

            if(isEquipped)
            {
                equipped.SetActive(true);
            }
        }
    }

    void ShowDescription()
    {
        AudioManager.Play("Click");

        if (isUnlocked)
        {
            string aName = am.allAccessory[ID].name;
            string aDescription = am.allAccessory[ID].description;

            accessoryName.text = aName;
            accessoryDescription.text = aDescription;

            shopButton.SetActive(false);

            if (isEquipped)
            {
                unequipButton.gameObject.SetActive(true);
                equipButton.gameObject.SetActive(false);
                unequipButton.Initialise(this);
            }
            else
            {
                unequipButton.gameObject.SetActive(false);
                equipButton.gameObject.SetActive(true);
                equipButton.Initialise(this);
                Debug.Log("Initialise to " + ID);
            }

        }else
        {
            accessoryName.text = "???";
            accessoryDescription.text = "You haven't unlock this item";
            shopButton.SetActive(true);
            unequipButton.gameObject.SetActive(false);
            equipButton.gameObject.SetActive(false);
        }
    }


    public void EquipAccessory()
    {
        am.EquipAccessory(ID);
        isEquipped = true;
        equipButton.gameObject.SetActive(false);
        unequipButton.gameObject.SetActive(true);
        unequipButton.Initialise(this);
        equipped.SetActive(true);

    }

    public void UnequipAccessory()
    {
        am.UnEquipAccessory(ID);
        isEquipped = false;
        equipButton.gameObject.SetActive(true);
        unequipButton.gameObject.SetActive(false);
        equipButton.Initialise(this);
        equipped.SetActive(false);
    }

    void CheckIfBeingChanged(int removedID)
    {
        if(this.ID == removedID)
        {
            isEquipped = false;
            equipped.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        AccessoryManager.OnChangedAccessory -= CheckIfBeingChanged;
    }
}
