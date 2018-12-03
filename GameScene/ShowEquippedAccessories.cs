using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowEquippedAccessories : MonoBehaviour {

    public Image accessoryOne, accessoryTwo;
    AccessoryManager am;

	// Use this for initialization
	void Start () {

        am = AccessoryManager.instance;

        if(am!=null)
        {
            accessoryOne.sprite = am.equippedAccessory[0].accessoryImage;
            accessoryTwo.sprite = am.equippedAccessory[1].accessoryImage;
        }
	}
	
}
