using UnityEngine;
using System.Collections;

public class SpawnTile : MonoBehaviour
{

	// Use this for initialization
	public GameObject _yellowCoin;
	public GameObject _redCoin;
	public GameObject _blueCoin;
	private float _leftSize;
	private float _rightSize;
	private bool _spawnLeft;
	private bool _spawnRight;
	private static float _spwanSpeedLeft;
	private static float _spwanSpeedRight;
	public static bool _disableSpwan;
	private int _tempOldScore;

	void Start ()
	{
		_disableSpwan = false;
		_leftSize = -2.39f;
		_rightSize = 2.39f;
		_spwanSpeedLeft = 1f;
		_spwanSpeedRight = 1f;
		_tempOldScore = 0;
		CollectMeUtil.GravityScale = 1f;

		_spawnLeft = true;
		_spawnRight = true;
		StartCoroutine (SpawnLeft (0.5f));
		StartCoroutine (SpawnRight (0.5f));
	}

	void Update ()
	{
		if (!_disableSpwan) {
			if (!_spawnLeft) {
				_spawnLeft = true;
				StartCoroutine (SpawnLeft (_spwanSpeedLeft));
			} else if (!_spawnRight) {
				_spawnRight = true;
				StartCoroutine (SpawnRight (_spwanSpeedRight));
			}
			if (ScoreCounter.Score != 0 && _tempOldScore != ScoreCounter.Score && ScoreCounter.Score % 10 == 0)
				ChangeUitlVeriables ();
		}
	}

	//Percentage (30% chance of Tap Me and 20% chance of Don't Tap Me)
	IEnumerator SpawnLeft (float time)
	{
		yield return new WaitForSeconds (time);
		int pct = Random.Range (0, 100);

		if (pct < 76)
			Instantiate (_yellowCoin, new Vector3 (_leftSize, transform.position.y, 0), Quaternion.identity);
		else if (pct > 75 && pct < 98)
			Instantiate (_redCoin, new Vector3 (_leftSize, transform.position.y, 0), transform.rotation);
		else
			Instantiate (_blueCoin, new Vector3 (_leftSize, transform.position.y, 0), Quaternion.identity);
		_spawnLeft = false;
	}

	IEnumerator SpawnRight (float time)
	{
		yield return new WaitForSeconds (time);
		int pct = Random.Range (0, 100);
		int index = Random.Range (0, 2);
		float[] position = { 1.2f, 0.8f, 0.6f };

		if (pct < 76)
			Instantiate (_yellowCoin, new Vector3 (_rightSize, transform.position.y + position [index], 0), Quaternion.identity);
		else if (pct > 75 && pct < 98)
			Instantiate (_redCoin, new Vector3 (_rightSize, transform.position.y + position [index], 0), transform.rotation);
		else
			Instantiate (_blueCoin, new Vector3 (_rightSize, transform.position.y + position [index], 0), Quaternion.identity);
		_spawnRight = false;
	}

	void ChangeUitlVeriables ()
	{
		CollectMeUtil.GravityScale += 0.5f;

		if (_spwanSpeedRight > 0.35) {
			_spwanSpeedRight -= 0.14f;
			_spwanSpeedLeft -= 0.141f;
		}
		Debug.Log (_spwanSpeedRight + " + left" + _spwanSpeedLeft + " gr " + CollectMeUtil.GravityScale);
		_tempOldScore = ScoreCounter.Score;
	}
}