using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPoison : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Tutorial: player got the poison");
            TutorialLifeManager.instance.GetHurt(1);
            Destroy(gameObject);
        }
    }
}
