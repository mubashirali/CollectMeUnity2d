using UnityEngine;
using System.Collections;

public class ScoreCounter : MonoBehaviour {

	private static int _score;
	private static int _bestScore;
	private static int _life;
    public TextMesh _scoreTxt;
	public TextMesh _lifeTxt;
	public TextMesh _lifeLbl;

    // Use this for initialization
	void Start () {
        _bestScore = PlayerPrefs.GetInt("BESTSCORE", 0);
		_life = 3;
        _score = 0;
	}

    public static int Score
    {
        get { return ScoreCounter._score; }
        set { ScoreCounter._score = value; }
    }

	public static int BestScore
	{
		get { return ScoreCounter._bestScore; }
		set { ScoreCounter._score = value; }
	}

	public static int Life
	{
		get { return ScoreCounter._life; }
		set { ScoreCounter._life = value; }
	}

    void FixedUpdate()
    {
		_scoreTxt.text = _score.ToString ();
		_lifeTxt.text = _life.ToString ();

		if(_lifeLbl.text == "")
			_lifeTxt.text = "";

        if (_bestScore < _score)
            _bestScore = _score;
    }

    void OnDestroy()
    {
        PlayerPrefs.SetInt("BESTSCORE", _bestScore);
    }
}
