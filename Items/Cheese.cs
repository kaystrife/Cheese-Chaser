using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cheese : MonoBehaviour {

    public static event Action<int> OnCheeseEaten = delegate { };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            AudioManager.Play("Swallow");
            Debug.Log("Player got the cheese!");
            OnCheeseEaten(1);
            Destroy(gameObject);
        }
    }
}
