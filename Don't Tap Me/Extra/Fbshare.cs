using UnityEngine;
using System.Collections;

public class Fbshare : MonoBehaviour
{
	private string AppID = "Place your Facebook AppID here";
	private string Link = "";
	// The picture's URL and the picture must be at least 200px by 200px.
	private string Picture = "http://s12.postimg.org/fdnh2gfvx/Collect_Me.png";

	// The name of your app or game.
	private string Name = "Collect Me!";

	// The caption of your game or app.
	private string Caption = "My new Score in Collect Me! Game is {0}. Can u beat it?";

	// The description of your game or app.
	private string Description = "Enjoy this free game! Challenge yourself and your friends.";

	public void FacebookShare ()
	{
		Caption = string.Format (Caption, ScoreCounter.Score);
		Application.OpenURL ("https://www.facebook.com/dialog/feed?" +
			"app_id=" + AppID +
			"&link=" + Link +
			"&picture=" + Picture +
			"&name=" + SpaceHere (Name) +
			"&caption=" + SpaceHere (AddScore(Caption) + " https://play.google.com/store/apps/details?id=com.mobiapps.collectme") +
			"&description=" + SpaceHere (Description) +
			"&redirect_uri=https://facebook.com/");
	}

	string SpaceHere (string val)
	{
		return val.Replace (" ", "%20"); // %20 is only used for space
	}

	string AddScore(string Caption)
	{
		return string.Format (Caption, ScoreCounter.Score);
	}

	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint ((Input.mousePosition)), Vector2.zero);
			if (hit.collider != null)
			if (hit.collider.gameObject.tag.ToLower () == "fbbutton")
				FacebookShare ();
            
		}
	}
}