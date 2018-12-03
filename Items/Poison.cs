using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Poison : MonoBehaviour {

    public static event Action<int> OnPoisonEaten = delegate { };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player got the poison!");
            OnPoisonEaten(-1);
            Destroy(gameObject);
        }
    }
}
