using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleProximity : MonoBehaviour {

	public GameObject player;
	public float followRange, killRange;
	public float moveSpeed;

	//for playing found sound
	public AudioClip call;
	AudioSource audSource;
	public bool justFound; 
	public bool roam;

	// Use this for initialization
	void Start () {

		GameObject p = GameObject.Find("whale");
		player = p;
		roam = true;
		followRange = 20f;
		killRange = 180f;
		audSource = GetComponent<AudioSource>();
		justFound = false;
		moveSpeed = Random.Range (10, 15);


		transform.LookAt (player.transform);
		Vector3 v = transform.rotation.eulerAngles;
		transform.rotation = Quaternion.Euler (0, v.y, v.z);


		WhaleAI aScript = this.gameObject.GetComponent<WhaleAI> ();
		aScript.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (roam) {

			transform.Translate (Vector3.forward * moveSpeed * Time.fixedDeltaTime);



			if (Vector3.Distance (transform.position, player.transform.position) < followRange) {
				roam = false;
			}

		}
		//start following
		else {
			if (Vector3.Distance (transform.position, player.transform.position) < followRange && justFound == false) {
				GameObject podNum = GameObject.Find ("GameManager");
				WhalesInPod script = podNum.GetComponent<WhalesInPod> ();

				WhaleAI aScript = this.gameObject.GetComponent<WhaleAI> ();
				aScript.enabled = true;
				if (justFound == false) {
					audSource.PlayOneShot (call);
					justFound = true;
					script.podCount += 1;

				}
				if (gameObject.layer == 8) {
					gameObject.layer = 0;
				}

			}

			//stop following
			if (Vector3.Distance (transform.position, player.transform.position) > killRange) {
				GameObject podNum = GameObject.Find ("GameManager");
				WhalesInPod script = podNum.GetComponent<WhalesInPod> ();

				WhaleAI aScript = this.gameObject.GetComponent<WhaleAI> ();
				aScript.enabled = false;
				if (justFound == true) {
					justFound = false;
					script.podCount -= 1;
					Destroy (gameObject);
				}
			}
		}

	}
}
