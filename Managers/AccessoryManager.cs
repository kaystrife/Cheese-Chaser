using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class AccessoryManager : MonoBehaviour
{
    public static AccessoryManager instance = null;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else if(instance !=this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        LoadData();
 
    }

    private const string MAX_ACCESSORY_NUM = "MaxAccessoryNum";     //player prefs key
    private const string CURR_ACCESSORY_NUM = "CurrentAccessoryNum";    //player prefs key
    private const string UNLOCKED_ACCESSORY = "UnlockedAccessory";  //player prefs key
    private const string EQUIPPED_ACCESSORY = "EquippedAccessory";  //player prefs key
    private const string INCREASE_MAX = "IncreaseMax";

    public Accessory[] allAccessory;   //all accessories in the game
    public Accessory[] equippedAccessory;  //currently equipped accessories
    public Accessory empty;

    public static event Action<int> OnChangedAccessory = delegate { };
    public static event Action<int, int> OnUpdatedAccessoryNum = delegate { };

    public int currentEquipNum; //number of accessories that are currently equipped
    public int maxAccessoryNum; //max number of accessories that can be equipped

    void LoadData()
    {
        LoadMaxAccessoryNum();
        LoadCurrAccessoryNum();
        LoadEquippedAccessory();
    }

    //equip an accessory
    public void EquipAccessory(int ID)
    {
        //check if there are any empty equipment slots
        for (int i = 0; i < maxAccessoryNum; i++)
        {
            //if the equipment slot is empty
            if(equippedAccessory[i].ID == 0)
            {
                equippedAccessory[i] = allAccessory[ID];
                SetEquipped(i, ID);
                ChangeCurrAccessoryNum(1);
                return;
            }
        }

        //if all equipment slots are full, replace the first equipment with the desired equipment
        int tempID = equippedAccessory[0].ID;
        OnChangedAccessory(tempID);

        equippedAccessory[0] = allAccessory[ID];
        SetEquipped(0, ID);

    }

    //unequip an accessory
    public bool UnEquipAccessory(int ID)
    {
        for (int i = 0; i < maxAccessoryNum; i++)
        {
            if (equippedAccessory[i].name == allAccessory[ID].name)
            {
                equippedAccessory[i] = empty;
                SetUnequipped(i);
                ChangeCurrAccessoryNum(-1);
                return true;
            }
        }

        return false;
    }

    //check if an accessory is equipped
    //(to show the equipped icon in the Accessory Scene)
    public bool CheckAccessoryEquipped(int ID)
    {
        if(ID >= allAccessory.Length)
        {
            return false;
        }

        for (int i = 0; i < maxAccessoryNum; i++)
        {
            if (equippedAccessory[i].name == allAccessory[ID].name)
            {
                return true;
            }
        }

        return false;
    }

    //check if an accessory has been unlocked by the player
    //(to show unlocked icon in the Accessory Scene)
    public bool CheckUnlockedAccessory(int ID)
    {
        string key = UNLOCKED_ACCESSORY + ID;

        if(PlayerPrefs.HasKey(key))
        {
            return true;
        }

        return false;
    }

    //=========================================================================================================
    // LOAD DATA
    //=========================================================================================================

    //Get the max number of accessories the player can equip
    void LoadMaxAccessoryNum()
    {
        if(PlayerPrefs.HasKey(MAX_ACCESSORY_NUM))
        {
            maxAccessoryNum = PlayerPrefs.GetInt(MAX_ACCESSORY_NUM);
        }else
        {
            maxAccessoryNum = 1;
            PlayerPrefs.SetInt(MAX_ACCESSORY_NUM, 1);
        }

    }

    void LoadCurrAccessoryNum()
    {
        if(PlayerPrefs.HasKey(CURR_ACCESSORY_NUM))
        {
            currentEquipNum = PlayerPrefs.GetInt(CURR_ACCESSORY_NUM);
        }else
        {
            currentEquipNum = 0;
            PlayerPrefs.SetInt(CURR_ACCESSORY_NUM, 0);
        }
    }

    //load the equipped accessories
    void LoadEquippedAccessory()
    {
        for (int i = 0; i < maxAccessoryNum; i++)
        {
            string key = EQUIPPED_ACCESSORY + i;

            if (PlayerPrefs.HasKey(key))
            {
                int ID = PlayerPrefs.GetInt(key);
                equippedAccessory[i] = allAccessory[ID];
            }
        }
    }

    //=========================================================================================================
    // SET DATA
    //=========================================================================================================

    //Increase the max number of accessories the play can equip
    public void IncreaseMaxAccessory()
    {
        maxAccessoryNum++;

        if(maxAccessoryNum > 2)
        {
            maxAccessoryNum = 2;
        }

        PlayerPrefs.SetInt(MAX_ACCESSORY_NUM, maxAccessoryNum);
        PlayerPrefs.SetInt(INCREASE_MAX, 1);
    }

    //change the current num of equipped accessory (equip or unequip)
    void ChangeCurrAccessoryNum(int change)
    {
        currentEquipNum += change;

        if (currentEquipNum < 0)
        {
            currentEquipNum = 0;
        }

        else if (currentEquipNum > maxAccessoryNum)
        {
            currentEquipNum = maxAccessoryNum;
        }

        PlayerPrefs.SetInt(CURR_ACCESSORY_NUM, currentEquipNum);
        OnUpdatedAccessoryNum(currentEquipNum, maxAccessoryNum);
    }


    //Unlock an accessory of a given ID
    public void UnlockAccessory(int ID)
    {
        string key = UNLOCKED_ACCESSORY + ID;
        PlayerPrefs.SetInt(key, 1);
    }


    //Save what accessory is equipped so it will shown as equipped next time the player load the game
    void SetEquipped(int keyID, int ID)
    {
        string key = EQUIPPED_ACCESSORY + keyID;

        PlayerPrefs.SetInt(key, ID);
    }

    //Save an equipment slot as empty in Player Prefs
    void SetUnequipped(int keyID)
    {
        string key = EQUIPPED_ACCESSORY + keyID;

        PlayerPrefs.SetInt(key, 0);
    }

    //=========================================================================================================
    // END SET DATA
    //=========================================================================================================

}
