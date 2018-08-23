using UnityEngine;
using System.Collections;

public class TileItem : MonoBehaviour
{
	private bool _enableCollider = true;
	public AudioSource _audioClipTap;
	public AudioSource _audioClipDontTap;
	private bool isGameOverTriggered;

	void Start ()
	{
		isGameOverTriggered = false;
		_enableCollider = true;
	}

	void Update ()
	{
		if (_enableCollider) {

#if UNITY_EDITOR
			if (Input.GetMouseButtonDown (0)) {
				RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint ((Input.mousePosition)), Vector2.zero);
				if (hit.collider != null)
					TouchDetected (hit);
			}
#endif

			if (Input.touchCount > 0) {
				for (int i = 0; i < Input.touchCount; i++) {
					if (Input.touches [i].phase == TouchPhase.Began) {
						RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint ((Input.touches [i].position)), Vector2.zero);
						if (hit.collider != null)
							TouchDetected (hit);
					}
					if (Input.touches [i].phase == TouchPhase.Ended) {
						RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint ((Input.touches [i].position)), Vector2.zero);
						if (hit.collider != null)
							TouchDetected (hit);
					}
				}
			}
		}
	}

	private void TouchDetected (RaycastHit2D hit)
	{
		if (hit.collider.gameObject.tag.ToLower () == "tapnottile") {
			Destroy (hit.collider.gameObject);
			StartCoroutine(TriggerGameOver());
		} else if (hit.collider.gameObject.tag.ToLower () == "taptile") {
			Destroy (hit.collider.gameObject);
			_audioClipTap.PlayOneShot (_audioClipTap.clip);
			ScoreCounter.Score += 1;
		} else if (hit.collider.gameObject.tag.ToLower () == "bluetile") {
			Destroy (hit.collider.gameObject);
			_audioClipTap.PlayOneShot (_audioClipTap.clip);
			ScoreCounter.Life += 1;
		}
	}

	public IEnumerator TriggerGameOver ()
	{
		yield return new WaitForEndOfFrame ();

		if (ScoreCounter.Life == 0 && !isGameOverTriggered) {
			isGameOverTriggered = true;
			_audioClipTap.PlayOneShot (_audioClipDontTap.clip);
			GameOver.showGameOverAnimation = true;
			_enableCollider = false;
			SpawnTile._disableSpwan = true;
			GoogleAds.ShowInterstitial (2f);
		} else if (ScoreCounter.Life != 0){
			ScoreCounter.Life--;
			_audioClipTap.PlayOneShot (_audioClipDontTap.clip);
		}
	}
}
