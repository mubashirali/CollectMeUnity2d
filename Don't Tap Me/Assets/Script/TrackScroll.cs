using UnityEngine;
using System.Collections;

public class TrackScroll : MonoBehaviour
{

    public float _trackSpeed = 0.5f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = new Vector2(0, Time.deltaTime * _trackSpeed);
        GetComponent<Renderer>().material.mainTextureOffset = offset;
    }
}
