using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetText : MonoBehaviour {

    Text text;
    TutorialLevelManager tlm;

    [SerializeField]
    [TextArea(0, 4)]
    string[] targets;

	// Use this for initialization
	void Start () {

        text = GetComponent<Text>();
        tlm = TutorialLevelManager.instance;
        SetTarget();
    }

    void SetTarget()
    {
        int level = tlm.tutorialLevel;

        switch(level)
        {
            case 0:
                text.text = targets[0];
                break;
            case 1:
                text.text = targets[1];
                break;
            case 2:
                text.text = targets[2];
                break;
            case 3:
                text.text = targets[3];
                break;
        }
    }
}
