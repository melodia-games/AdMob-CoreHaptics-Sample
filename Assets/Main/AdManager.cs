using System.Collections;
using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    class AdUnits
    {
        internal string BannerId;
        internal string InterstitialId;
        internal string RewardId;
    }

    static readonly AdUnits TestiOSId = new AdUnits {
        BannerId = "ca-app-pub-3940256099942544/2934735716",
        InterstitialId = "ca-app-pub-3940256099942544/4411468910",
        RewardId = "ca-app-pub-3940256099942544/1712485313",
    };

    BannerView bannerView;
    InterstitialAd interstitial;
    RewardedAd rewardedAd;

    void Awake()
    {
        MobileAds.Initialize(initStatus => {
            RequestBanner();
            RequestInterstitial();
            RequestReward();
        });
    }

    AdRequest GetAdRequest()
    {
        var builder = new AdRequest.Builder();
        return builder.Build();
    }

    private void RequestBanner()
    {
        var adUnitId = TestiOSId.BannerId;

        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
        bannerView.LoadAd(GetAdRequest());
    }

    private void RequestInterstitial()
    {
        var adUnitId = TestiOSId.InterstitialId;

        if (interstitial != null) {
            interstitial.Destroy();
        }

        interstitial = new InterstitialAd(adUnitId);

        interstitial.LoadAd(GetAdRequest());

        interstitial.OnAdClosed += (o, e) => {
            RequestInterstitial();
        };
    }

    void RequestReward()
    {
        var adUnitId = TestiOSId.RewardId;

        rewardedAd = new RewardedAd(adUnitId);

        rewardedAd.LoadAd(GetAdRequest());

        rewardedAd.OnAdClosed += (o, e) => {
            RequestReward();
        };
    }

    public void ShowInterstitial()
    {
        if (interstitial == null || !interstitial.IsLoaded()) {
            Debug.Log("Now loading interstitial");
            return;
        }

        interstitial.Show();
    }

    public void ShowReward()
    {
        if (rewardedAd == null || !rewardedAd.IsLoaded()) {
            Debug.Log("Now loading reward");
            return;
        }

        rewardedAd.Show();
    }
}
