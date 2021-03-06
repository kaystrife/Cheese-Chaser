﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnequipButton : MonoBehaviour {

    private AccessoryButton accessoryButton;
    Button button;

    // Use this for initialization
    void Start()
    {

        button = GetComponent<Button>();

        button.onClick.AddListener(Unequip);
    }

    public void Initialise(AccessoryButton accessoryButton)
    {
        this.accessoryButton = accessoryButton;
    }

    void Unequip()
    {
        AudioManager.Play("Click");
        accessoryButton.UnequipAccessory();
    }
}
