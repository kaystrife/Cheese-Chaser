using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wallet : MonoBehaviour {

    public Text cheeseAmount;
    public Text coinAmount;
    GameRecordManager grm;

	// Use this for initialization
	void Start () {

        grm = GameRecordManager.instance;

        if(grm!=null)
        {
            GetCoinAndCheese();
        }

        GameRecordManager.OnWalletChange += UpdateWallet;
	}
	
	void GetCoinAndCheese()
    {
        cheeseAmount.text = grm.cheeseAmount + "";
        coinAmount.text = grm.coinAmount + "";
    }

    void UpdateWallet(bool spent)
    {
        if(spent)
        {
            cheeseAmount.text = grm.cheeseAmount + "";
            coinAmount.text = grm.coinAmount + "";
        }
    }

    //unsubscribe
    private void OnDestroy()
    {
        GameRecordManager.OnWalletChange -= UpdateWallet;
    }
}
