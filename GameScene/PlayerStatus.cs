using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {

    public bool isHurt;
    private Animator anim;

    [SerializeField]
    private float hurtTime;
    private float hurtTimeCnt;
    private int immuneTimes;

    GameRecordManager grm;
    AccessoryManager am;

    public static Action<bool> OnHurt = delegate { };

	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();
        grm = GameRecordManager.instance;
        am = AccessoryManager.instance;

        if(am!=null)
        {
            Debug.Log("Here");
            foreach(Accessory accessory in am.equippedAccessory)
            {
                this.immuneTimes += accessory.immuneTimes;
            }
        }

        hurtTimeCnt = hurtTime;

        Poison.OnPoisonEaten += GetHurt;
	}
	
	// Update is called once per frame
	void Update () {
		
        if(isHurt)
        {
            hurtTimeCnt -= Time.deltaTime;
            if(hurtTimeCnt <= 0)
            {
                isHurt = false;
                anim.SetBool("isHurt", false);
                anim.SetBool("isProtected", false);
                hurtTimeCnt = hurtTime;
            }
        }

	}

    public void GetHurt(int damage)
    {
        if (immuneTimes > 0)
        {
            AudioManager.Play("Protect");
            isHurt = true;
            immuneTimes--;
            anim.SetBool("isProtected", true);
            Debug.Log("Protected!");
        }
        else
        {
            AudioManager.Play("Hurt");
            OnHurt(true);
            isHurt = true;
            anim.SetBool("isHurt", true);
            grm.EarnOrSpendLife(damage);
        }
    }

    private void OnDisable()
    {
        Poison.OnPoisonEaten -= GetHurt;
    }
}
