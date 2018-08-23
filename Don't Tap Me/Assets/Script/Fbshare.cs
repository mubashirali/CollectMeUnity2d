using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using System;

public class Fbshare : MonoBehaviour
{
	private string Link;
	private byte[] dataToSave;
	public static bool stopOperation;

	// The picture's URL and the picture must be at least 200px by 200px.
//	private string Picture = "http://s12.postimg.org/fdnh2gfvx/Collect_Me.png";
//
//	// The name of your app or game.
//	private string Name = "Collect Me!";
//
//	// The caption of your game or app.
//	private string Caption = "My new Score in Collect Me! Game is {0}. Can u beat it?";
//
//	// The description of your game or app.
//	private string Description = "Enjoy this free game! Challenge yourself and your friends.";

	void Awake ()
	{
		if (!FB.IsInitialized) {
			// Initialize the Facebook SDK
			FB.Init (InitCallback, OnHideUnity);
		} else {
			// Already initialized, signal an app activation App Event
			FB.ActivateApp ();
		}
	}

	void Start()
	{
		stopOperation = false;
		Link = "https://play.google.com/store/apps/details?id=com.mobiapps.collectme";	
	}

	void InitCallback ()
	{
		Debug.Log ("Fb init callback");
		FB.ActivateApp ();

	}

	void Update ()
	{
		if (!stopOperation) {
			if (Input.GetMouseButtonDown (0)) {
				RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint ((Input.mousePosition)), Vector2.zero);
				if (hit.collider != null) {
					if (hit.collider.gameObject.tag.ToLower () == "fbbutton")
						InitalizeFBSDK ();
				}
			}
		}
	}

	private void InitalizeFBSDK ()
	{
		if (FB.IsLoggedIn) {
			FBShareDialog ();
		} else {
			var perms = new List<string> (){ "publish_actions" };
			FB.LogInWithPublishPermissions (perms, AuthCallback);
		}
	}

	private void AuthCallback (ILoginResult result)
	{
		if (FB.IsLoggedIn)
			FBShareDialog ();
		else
			Debug.Log ("User cancelled login");
	}

	private void OnHideUnity (bool isGameShown)
	{
		if (!isGameShown) {
			// Pause the game - we will need to hide
			Time.timeScale = 0;
		} else {
			// Resume the game - we're getting focus again
			Time.timeScale = 1;
		}
	}

	private void FBShareDialog ()
	{
		Debug.Log ("Event FBShareDialog");
		CaptureScreenShotAndPostToFB ();
//		FB.ShareLink (
//			new Uri (Link),
//			"",
//			"",
//			new Uri (Picture),
//			this.HandleResult);
	}

	protected void HandleResult (IShareResult result)
	{
		if (result.Cancelled || !String.IsNullOrEmpty (result.Error)) {
			Debug.Log ("ShareLink Error: " + result.Error);
		} else if (!String.IsNullOrEmpty (result.PostId)) {
			Debug.Log (result.PostId);
		} else {
			Debug.Log ("ShareLink success!");
		}
	}


	public void CaptureScreenShotAndPostToFB()
	{
		StartCoroutine(ShareImageShot());
	}

	IEnumerator ShareImageShot()
	{
		yield return new WaitForEndOfFrame();

		var width = Screen.width;
		var height = Screen.height;
		var tex = new Texture2D(width, height, TextureFormat.RGB24, false);
		tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
		tex.Apply();
		byte[] screenshot = tex.EncodeToPNG();

		var wwwForm = new WWWForm();
		wwwForm.AddBinaryData("image", screenshot, "Screenshot.png");
		//wwwForm.AddField ("name","Collect Me! Can you beat my score!, Download now for free from " + Link);

		FB.API("me/photos", HttpMethod.POST, APICallback, wwwForm);

		stopOperation = true;
		Handheld.SetActivityIndicatorStyle (AndroidActivityIndicatorStyle.Large);
		Handheld.StartActivityIndicator();
	}

	public void APICallback(IGraphResult r)
	{
		stopOperation = false;
		Handheld.StopActivityIndicator ();
		Debug.Log ("Posted!");
	}
}