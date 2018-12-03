using UnityEngine.Monetization;
using UnityEngine;

public class UnityAdsScript : MonoBehaviour
{

    string gameId = "2865888";
    bool testMode = false;

    void Start()
    {
        Monetization.Initialize(gameId, testMode);
    }
}
