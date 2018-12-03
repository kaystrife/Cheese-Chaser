using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccessoryEquippedText : MonoBehaviour {

    public Text text;

    private const string MAX_ACCESSORY_NUM = "MaxAccessoryNum";     //player prefs key
    private const string CURR_ACCESSORY_NUM = "CurrentAccessoryNum";    //player prefs key

    private void Awake()
    {
        AccessoryManager.OnUpdatedAccessoryNum += UpdateText;
    }

    // Use this for initialization
    void Start () {
    
        int currEquipNum = PlayerPrefs.GetInt(CURR_ACCESSORY_NUM);
        int maxEquipNum = PlayerPrefs.GetInt(MAX_ACCESSORY_NUM);

        text.text = "Accessory Equipped: " + currEquipNum + " / " + maxEquipNum;
	}
	
	void UpdateText(int curr, int max)
    {
        text.text = "Accessory Equipped: " + curr + " / " + max;
    }

    private void OnDestroy()
    {
        AccessoryManager.OnUpdatedAccessoryNum -= UpdateText;
    }
}
