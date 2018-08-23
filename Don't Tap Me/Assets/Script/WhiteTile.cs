using UnityEngine;
using System.Collections;

public class WhiteTile : MonoBehaviour
{
    // Use this for initialization
    public GameObject _tile;
    private float _leftSize = -2.39f;
    private float _rightSize = 2.39f;

    void Start()
    {
        InvokeRepeating("SpawnLeft", 0.53f, 0.51f);
        InvokeRepeating("SpawnRight", 0.71f, 0.53f);
		CollectMeUtil.GravityScale = 2;
    }

    void SpawnLeft()
    {
        Instantiate(_tile, new Vector3(_leftSize, transform.position.y, 0), Quaternion.identity);
    }
    void SpawnRight()
    {
        Instantiate(_tile, new Vector3(_rightSize, transform.position.y, 0), Quaternion.identity);
    }
}
