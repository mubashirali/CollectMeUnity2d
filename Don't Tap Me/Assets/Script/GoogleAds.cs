using System;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class GoogleAds : MonoBehaviour
{

	public static string BannerID = "Place your banner ID here";
	public static string InterstitialID = "Place your Interstitial ID here";
	public static InterstitialAd interstitial;
	public static BannerView bannerView;
	public static GoogleAds Instance;

	// Use this for initialization
	void Start ()
	{
		RequestBanner ();
		RequestInterstitial ();
		Instance = this;
	}

	public static void RequestBanner ()
	{
		try {
			#if UNITY_ANDROID
			string adUnitId = BannerID;
			#elif UNITY_IPHONE
				string adUnitId = InterstitialID;
			#else
				string adUnitId = InterstitialID;
			#endif

			// Create a 320x50 banner at the top of the screen.
			bannerView = new BannerView (adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
			// Load the banner with the request.
			bannerView.LoadAd (createAdRequest ());
		} catch (Exception ex) {
		}
	}

	public static void RequestInterstitial ()
	{
		try {
			#if UNITY_EDITOR
			string adUnitId = InterstitialID;
			#elif UNITY_ANDROID
				string adUnitId = InterstitialID;
			#elif UNITY_IPHONE
				string adUnitId = InterstitialID;
			#else
				string adUnitId = InterstitialID;
			#endif

			// Create an interstitial.
			interstitial = new InterstitialAd (adUnitId);
			// Load an interstitial ad.
			interstitial.LoadAd (createAdRequest ());
		} catch (Exception ex) {
		}
	}

	public static AdRequest createAdRequest ()
	{
		return new AdRequest.Builder ()
			.AddKeyword ("game")
			.Build ();
	}

	public static void ShowInterstitial (float time)
	{
			if(Instance)
			Instance.Invoke ("Show", time);
	}

	public void Show ()
	{
		if (interstitial.IsLoaded ())
			interstitial.Show ();
		else
			RequestInterstitial ();
	}

}
