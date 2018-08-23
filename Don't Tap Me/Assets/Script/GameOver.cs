using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour
{
	public GameObject _fbButton;

	public TextMesh _bestLbl;
	public TextMesh _bestTxt;
	public TextMesh _tapToPlayLbl;
	public TextMesh _lifeLbl;

	public static bool showGameOverAnimation = false;

	private Renderer _rend;

	private float _gameOverPositionY = -6f;
	private float _bestTxtPositionY = -7f;
	private float _bestLblPositionY = -8f;

	// Use this for initialization
	void Start ()
	{
		_rend = GetComponent<Renderer> ();
		_rend.enabled = false;
	}

	void Update ()
	{
		if (!Fbshare.stopOperation) {
			if (Input.GetMouseButtonDown (0)) {
				if (_gameOverPositionY >= 0 && _bestLblPositionY >= 3.8f && _bestTxtPositionY >= 2.8f) {
					RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint ((Input.mousePosition)), Vector2.zero);
					if (hit.collider == null) {
						Application.LoadLevel ("Scene01");

					}
				}
			}
		}
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		if (_rend.enabled)
			_bestTxt.text = ScoreCounter.BestScore.ToString ();

		if (showGameOverAnimation) {
			_rend.enabled = true;

			gameObject.transform.position = new Vector3 (0, _gameOverPositionY, 0);

			_lifeLbl.text = "";

			_bestLbl.transform.position = new Vector3 (0, _bestLblPositionY, 0);
			_bestTxt.transform.position = new Vector3 (0, _bestTxtPositionY, 0);

			_gameOverPositionY += 0.1f;
			_bestLblPositionY += 0.1f;
			_bestTxtPositionY += 0.1f;

			if (_bestLblPositionY >= 3.8f)
				_bestLblPositionY = 3.8f;
			if (_bestTxtPositionY >= 2.8f)
				_bestTxtPositionY = 2.8f;
			if (_gameOverPositionY >= 0)
				_gameOverPositionY = 0;

			if (_gameOverPositionY >= 0 && _bestLblPositionY >= 3.8f && _bestTxtPositionY >= 2.8f) {
				_tapToPlayLbl.transform.position = new Vector3 (0, -1.5f, 0);
				showGameOverAnimation = false;

				_fbButton.transform.position = new Vector3 (0, -2.99f, 0);
			}
		}


	}
}
