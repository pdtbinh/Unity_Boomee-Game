using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class Ads : MonoBehaviour, IUnityAdsListener
{
    private string appStoreID = "3809256";

    //App only available for Android
    private string playStoreID = "3809257";

    private string interstitialAd = "video";

    //Currently only support Rewarded video
    private string rewardedVideoAd = "rewardedVideo";
    
    public bool isTargetPlayStore = true;
    public bool isTestAd = false;

    public GameObject reviveButton;
    public GameObject continueButton;

    private void Start()
    {
        Advertisement.AddListener(this);
        InitializeAdvertisement();
    }

    private void InitializeAdvertisement()
    {
        if (isTargetPlayStore) { Advertisement.Initialize(playStoreID, isTestAd); return; }

        Advertisement.Initialize(appStoreID, isTestAd);
    }

    public void PlayInterstitialAd()
    {
        if (!Advertisement.IsReady(interstitialAd)) { return; }
        Advertisement.Show(interstitialAd);
    }

    
    public void PlayRewardedVideoAd()
    {
        if (!Advertisement.IsReady(rewardedVideoAd)) { return; }
        Advertisement.Show(rewardedVideoAd);
    }

    public void OnUnityAdsReady(string placementId)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidError(string message)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        //throw new System.NotImplementedException();

        switch (showResult)
        {

            case ShowResult.Failed:
                if (placementId == rewardedVideoAd && continueButton != null && reviveButton != null)
                {
                    continueButton.SetActive(false);
                    reviveButton.SetActive(false);
                }
                break;

            case ShowResult.Skipped:
                if (placementId == rewardedVideoAd && continueButton != null && reviveButton != null)
                {
                    continueButton.SetActive(false);
                    reviveButton.SetActive(false);
                }
                break;

            case ShowResult.Finished:

                if (placementId == rewardedVideoAd && continueButton != null && reviveButton != null)
                {
                    continueButton.SetActive(true);
                    reviveButton.SetActive(false);
                }
                break;
        } 
    }
}

