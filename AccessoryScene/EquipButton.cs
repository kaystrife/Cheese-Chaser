using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipButton : MonoBehaviour {

    private AccessoryButton accessoryButton;
    Button button;

	// Use this for initialization
	void Start () {

        button = GetComponent<Button>();

        button.onClick.AddListener(Equip);
	}
	
    public void Initialise(AccessoryButton accessoryButton)
    {
        this.accessoryButton = accessoryButton;
    }

    void Equip()
    {
        AudioManager.Play("Click");
        accessoryButton.EquipAccessory();
    }
}
