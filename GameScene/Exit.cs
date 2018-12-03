using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour {

    LevelManager levelManager;

    private void Start()
    {
        levelManager = LevelManager.instance;
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("reached exit");
            levelManager.ToNextLevel();
            this.enabled = false;
        }
    }
}
