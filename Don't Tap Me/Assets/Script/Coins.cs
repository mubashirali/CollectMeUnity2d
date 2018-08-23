using UnityEngine;
using System.Collections;

public class Coins : MonoBehaviour {

	void Start() {
			//gameObject.GetComponent<Rigidbody2D> ().gravityScale += (float) CollectMeUtil.GravityScale;
	}

	void Update()
	{
		GetComponent<Rigidbody2D> ().AddForce (Vector2.down * (int) CollectMeUtil.GravityScale);
	}
}
