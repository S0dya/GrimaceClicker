using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsManager : SingletonMonobehaviour<AdsManager>, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
#if UNITY_IOS
    const string rewardedVideo = "Rewarded_iOS";
    string gameId = "5392398";
#else
    const string rewardedVideo = "Rewarded_Android";
    string gameId = "5392399";
#endif

    [HideInInspector] public bool x10;
    [HideInInspector] public bool x2;

    protected override void Awake()
    {
        base.Awake();

        Advertisement.Initialize(gameId, true, this);
    }

    public void LoadRewardedAd()
    {
        Advertisement.Load(rewardedVideo, this);
    }

    public void ShowRewardedAd()
    {
        Advertisement.Show(rewardedVideo, this);
    }

    #region Interface Implementations
    public void OnInitializationComplete()
    {
        LoadRewardedAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Init Failed: [{error}]: {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Load Failed: [{error}:{placementId}] {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"OnUnityAdsShowFailure: [{error}]: {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        //AudioManager.Instance.ToggleSound(false);
    }

    public void OnUnityAdsShowClick(string placementId)
    {
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        //AudioManager.Instance.ToggleSound(true);
        if (x10)
        {
            StatsPanel.I.RewardPlayerX10();
        }
        else if (x2)
        {
            StatsPanel.I.RewardPlayerX2();
        }
    }
    #endregion
}
