using UnityEngine;
using System.Collections;

public class DistroyTiles : MonoBehaviour
{
	private bool _callTriggerGameOver = true;
	public bool _destroyAll = false;

	void Start ()
	{
		_callTriggerGameOver = true;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (_destroyAll)
			Destroy (other.gameObject);
		else {
			if (other.gameObject.tag.ToLower () == "taptile") {
				TileItem objTileItem = GameObject.Find ("BlackTile").GetComponent<TileItem> ();
				StartCoroutine (objTileItem.TriggerGameOver ());
			}
		}
		Destroy (other.gameObject);
	}
}
