using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour
{
	public AudioSource _audioClip;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
			LaunchNewScene ();	
        }
    }

	void LaunchNewScene ()
	{
		_audioClip.PlayOneShot (_audioClip.clip);
		//yield return new WaitForSeconds (_audioClip.clip.length);
		Application.LoadLevel("Scene01");
	}

}
