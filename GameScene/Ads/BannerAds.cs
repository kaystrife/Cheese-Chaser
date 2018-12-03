using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAds : MonoBehaviour
{
    private const string NO_ADS = "NoAds";

    public string bannerPlacement;
    public bool testMode = false;

    const string gameID = "2865888";

    /*
#if UNITY_IOS
    public const string gameID = "2865887";
#elif UNITY_ANDROID
    public const string gameID = "2865888";
#elif UNITY_EDITOR
    public const string gameID = "1111111";
#endif*/

    void Start()
    {
        Advertisement.Initialize(gameID, testMode);

        StartCoroutine(ShowBannerWhenReady());
    }

    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(bannerPlacement))
        {
            yield return new WaitForSeconds(0.5f);
        }

        Advertisement.Banner.Show(bannerPlacement);
        Debug.Log("Show ad banner!");
    }
}
