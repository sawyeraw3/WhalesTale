using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleProximity : MonoBehaviour {

	public GameObject player;
	public float followRange, killRange;

	//for playing found sound
	public AudioClip call;
	AudioSource audSource;
	public bool justFound; 

	// Use this for initialization
	void Start () {
		followRange = 15f;
		killRange = 30f;
		audSource = GetComponent<AudioSource>();
		justFound = false;

	}
	
	// Update is called once per frame
	void Update () {
		//start following
		if (Vector3.Distance(transform.position, player.transform.position) < followRange){
			GameObject podNum = GameObject.Find("GameManager");
			WhalesInPod script = podNum.GetComponent<WhalesInPod>();
			script.podCount += 1;

			WhaleAI aScript = this.gameObject.GetComponent<WhaleAI>();
			aScript.enabled = true;
			if (justFound == false){
				audSource.PlayOneShot(call);
				justFound = true;
			}

		}

		//stop following
		if (Vector3.Distance(transform.position, player.transform.position) > killRange){
			GameObject podNum = GameObject.Find("GameManager");
			WhalesInPod script = podNum.GetComponent<WhalesInPod>();
			script.podCount -= 1;

			WhaleAI aScript = this.gameObject.GetComponent<WhaleAI>();
			aScript.enabled = false;
			justFound = false;
		}

	}
}
