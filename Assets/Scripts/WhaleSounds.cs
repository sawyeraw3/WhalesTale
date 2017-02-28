using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleSounds : MonoBehaviour {

	public AudioClip[] sounds;
	private AudioSource whale;

	// Use this for initialization
	void Start () {
		whale = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) && !whale.isPlaying) {
			int index = Random.Range (0, sounds.Length);
			whale.clip = sounds [index];
			whale.Play ();
		}
	}
}
