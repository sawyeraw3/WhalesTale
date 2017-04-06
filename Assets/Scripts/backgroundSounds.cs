using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundSounds : MonoBehaviour {

	public AudioClip underwater;
	private AudioSource sound;
	public GameObject whale;

	// Use this for initialization
	void Start () {
		sound = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (whale.transform.position.y > 0)
			sound.volume = 0;
		else
			sound.volume = .35f;
		
		if (whale.transform.position.y < 0 && sound.isPlaying == false) {
			sound.loop = true;
			sound.clip = underwater;
			sound.Play ();
		}
	}
}
