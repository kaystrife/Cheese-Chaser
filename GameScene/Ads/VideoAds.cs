using UnityEngine.Monetization;
using UnityEngine;
using System.Collections;

public class VideoAds : MonoBehaviour
{
    public string placementId;
    string gameId = "2865888";
    bool testMode = false;

    void Start()
    {
        Monetization.Initialize(gameId, testMode);
    }

    public void ShowAd()
    {
        StartCoroutine(ShowAdWhenReady());
    }

    private IEnumerator ShowAdWhenReady()
    {
        while (!Monetization.IsReady(placementId))
        {
            yield return new WaitForSeconds(0.25f);
        }

        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent(placementId) as ShowAdPlacementContent;

        if (ad != null)
        {
            ad.Show();
        }
    }
}
