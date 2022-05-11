using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using Gamina.Maze.Managers;
using Gamina.Maze.Controllers.UI;

namespace Gamina.Maze
{
    public class GoogleAdMobController : MonoBehaviour
    {
        private void OnEnable()
        {
            GameManager.OnLevelCompleted += ShowInterstitialAd;
        }

        private void OnDisable()
        {
            GameManager.OnLevelCompleted -= ShowInterstitialAd;
        }

        private void Start()
        {
            MobileAds.Initialize(initStatus => { });
            RequestInterstitial();
        }

        private InterstitialAd _interstitial;

        private void RequestInterstitial()
        {
#if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

            _interstitial = new InterstitialAd(adUnitId);

            _interstitial.OnAdClosed += GameManager.Instance.OnAdClose;

            AdRequest request = new AdRequest.Builder().Build();
            _interstitial.LoadAd(request);
        }

        private void ShowInterstitialAd()
        {
            if (_interstitial.IsLoaded())
            {
                _interstitial.Show();
            }
            else
            {
                RequestInterstitial();
            }
        }
    }
}
