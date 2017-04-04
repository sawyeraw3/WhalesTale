using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

	public AudioClip[] music;
	private AudioSource sound;
	private int i;

	// Use this for initialization
	void Start () {
		sound = GetComponent<AudioSource> ();
		i = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (i == music.Length + 1)
			i = 0;
		
		if (!sound.isPlaying) {
			sound.clip = music [i];
			sound.Play ();
			i++;
		}
	}
}
