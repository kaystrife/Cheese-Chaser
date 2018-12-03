using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialLifeManager : MonoBehaviour {

    public static TutorialLifeManager instance = null;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }else if(instance!=this)
        {
            Destroy(gameObject);
        }
    }

    int life;
    Text lifeText;
    float hurtTime;
    float hurtTimeCnt;

    public Animator anim;
    public bool isHurt;

	// Use this for initialization
	void Start () {

        lifeText = GetComponent<Text>();

        life = 3;
        lifeText.text = "x" + life;

        isHurt = false;
        hurtTime = 1.0f;
        hurtTimeCnt = hurtTime;
	}

    private void Update()
    {
        if(isHurt)
        {
            hurtTimeCnt -= Time.deltaTime;
        }

        if(hurtTimeCnt<=0)
        {
            hurtTimeCnt = hurtTime;
            isHurt = false;
            anim.SetBool("isHurt", false);
        }
    }

    public void GetHurt(int damage)
    {
        AudioManager.Play("Hurt");
        life -= damage;
        isHurt = true;
        anim.SetBool("isHurt", true);

        if (life <= 0)
        {
            life = 0;
            TutorialBoardManager.instance.ShowNoLifeMessage();
        }

        lifeText.text = "x" + life;
    }
}
