using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCheese : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player got the cheese!");
            AudioManager.Play("Swallow");
            TutorialLevelManager.instance.GetCheese(1);
            Destroy(gameObject);
        }
    }
}
