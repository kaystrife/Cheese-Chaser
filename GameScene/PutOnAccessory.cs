using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PutOnAccessory : MonoBehaviour {

    SpriteRenderer sprite;
    AccessoryManager am;
	// Use this for initialization
	void Start () {

        sprite = GetComponent<SpriteRenderer>();
        am = AccessoryManager.instance;

        if(am!=null)
        {
            sprite.sprite = am.equippedAccessory[0].accessoryImage;
        }
	}
}
