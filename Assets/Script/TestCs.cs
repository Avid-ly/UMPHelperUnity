using GoogleMobileAds.Ump.Api;
using System.Collections;
using System.Collections.Generic;
using UMPHelper;
using UnityEngine;
public class TestCs : MonoBehaviour
{
#if UNITY_IOS
string adUnitId = "YOUR_IOS_AD_UNIT_ID";
#else // UNITY_ANDROID
    string adUnitId = "60f3ff7bdf62d4a8";
#endif
    int retryAttempt;
    // Start is called before the first frame update
    void Start()
    {

        var debugSettings = new ConsentDebugSettings
        {
            // Geography appears as in EEA for debug devices.
            DebugGeography = DebugGeography.EEA,
            TestDeviceHashedIds = new List<string>
             {
                 "6B268B4102A1961812FC511632EEAB9B" //this is my android device,please change to your device
             }
        };
        // Set tag for under age of consent.
        // Here false means users are not under age of consent.
        ConsentRequestParameters request = new ConsentRequestParameters
        {
            TagForUnderAgeOfConsent = false,
            ConsentDebugSettings = debugSettings,
        };

        // Check the current consent information status.
        ConsentInformation.Update(request, OnConsentInfoUpdated);


    }
    void OnConsentInfoUpdated(FormError consentError)
    {
        if (consentError != null)
        {
            // Handle the error.
            UnityEngine.Debug.LogError(consentError);
            return;
        }
        // If the error is null, the consent information state was updated.
        // You are now ready to check if a form is available.
        ConsentInformation.Reset();
        ConsentForm.LoadAndShowConsentFormIfRequired((FormError formError) =>
        {
            if (formError != null)
            {
                // Consent gathering failed.
                UnityEngine.Debug.LogError(consentError);
                return;
            }

            // Consent has been gathered.
            if (ConsentInformation.CanRequestAds())
            {
                Debug.Log("CanRequestAds");

                UMPHelperApi umpHelperApi = new UMPHelperApi();
                if (!umpHelperApi.isGDPR())
                {
                    // 初始化pssdk等sdk
                }
                else
                {
                    if (umpHelperApi.isUmpAllowed())
                    {
                        // 正常初始化其他sdk
                    }
                    else
                    {
                        // 在初始化tasdk之前，调用方法disableAccessPrivacyInformation
                        // 正常初始化其他sdk
                    }
                }
              
            }
        });
    }

    public void onInitClick()
    {
        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
        {
            // AppLovin SDK is initialized, start loading ads
            Debug.Log("sdk inited : " + sdkConfiguration.ToString());

             UMPHelperApi umpHelperApi = new UMPHelperApi();
                if (!umpHelperApi.isGDPR())
                {
                    // 初始化pssdk等sdk
                }
                else
                {
                    if (umpHelperApi.isUmpAllowed())
                    {
                        // 正常初始化其他sdk
                    }
                    else
                    {
                        // 在初始化tasdk之前，调用方法disableAccessPrivacyInformation
                        // 正常初始化其他sdk
                    }
                }
        };

        MaxSdk.SetSdkKey("BLZ3nWD4mwe_7TFhC7kqaUqZMz32l9nxVL-GtCKc6-cEWsxizeXT8L7UJAX2KJ-qey4W9P7FNkUvaPcT295AUD");
        MaxSdk.SetUserId("USER_ID_001");
        MaxSdk.InitializeSdk();

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void onLoadClick()
    {
        Debug.Log("InitializeInterstitialAds ");
        // Attach callback
        MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
        MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialLoadFailedEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += OnInterstitialDisplayedEvent;
        MaxSdkCallbacks.Interstitial.OnAdClickedEvent += OnInterstitialClickedEvent;
        MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialHiddenEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += OnInterstitialAdFailedToDisplayEvent;

        // Load the first interstitial
        LoadInterstitial();
    }

    private void LoadInterstitial()
    {
        Debug.Log("LoadInterstitial ");
        MaxSdk.LoadInterstitial(adUnitId);
    }

    private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad is ready for you to show. MaxSdk.IsInterstitialReady(adUnitId) now returns 'true'

        // Reset retry attempt
        Debug.Log("OnInterstitialLoadedEvent: " + adUnitId);
        retryAttempt = 0;
    }

    private void OnInterstitialLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Interstitial ad failed to load 
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds)

        retryAttempt++;
        double retryDelay = 3.0;
        Debug.Log("OnInterstitialLoadFailedEvent: " + adUnitId + " " + errorInfo.Code + " " + errorInfo.Message);

        Invoke("LoadInterstitial", (float)retryDelay);
    }

    private void OnInterstitialDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    private void OnInterstitialAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad failed to display. AppLovin recommends that you load the next ad.
        Debug.Log("OnInterstitialAdFailedToDisplayEvent: " + adUnitId + " " + errorInfo.Code + " " + errorInfo.Message);
        LoadInterstitial();
    }

    private void OnInterstitialClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    private void OnInterstitialHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad is hidden. Pre-load the next ad.
        Debug.Log("OnInterstitialHiddenEvent: " + adUnitId);
        LoadInterstitial();
    }


    public void showInter()
    {
        Debug.Log("showInter: ");
        if (MaxSdk.IsInterstitialReady(adUnitId))
        {
            MaxSdk.ShowInterstitial(adUnitId);
        }
        else
        {
            Debug.Log("showInter: not ready");
        }
    }
}
