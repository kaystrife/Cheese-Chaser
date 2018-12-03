using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialExit : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("reached exit");

            if(TutorialLevelManager.instance.ToNextLevel())
            {
                Debug.Log("Exit disable");
                this.enabled = false;
            }

        }
    }
}
