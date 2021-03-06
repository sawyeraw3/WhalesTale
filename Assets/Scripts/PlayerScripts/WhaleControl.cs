using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleControl: MonoBehaviour {

	private Transform cameraPos;
	private float secs;
	private bool waitForPing = false;
	private bool waitForUp = false;
	private AudioSource whale;
	private GameObject followPoint;

	public AudioClip[] sounds;
	public float moveSpeed = 10f;
	public float defaultSpeed = 10f;
	public float sprintSpeed = 20f;
	public GameObject pingSphere;
	public float pingDelay = 5f;
	public bool endGame = false;

	// Use this for initialization
	void Start () {
		followPoint = GameObject.Find ("Focus");
		whale = GetComponent<AudioSource> ();
		cameraPos = GameObject.FindGameObjectWithTag ("MainCamera").transform;
		secs = pingDelay;
	}

	void Update() {
		if (!endGame) {
			if (Input.GetMouseButtonDown (0) || GvrController.ClickButtonDown) {
				waitForUp = true;
				secs = 0.5f;
			}
			if (Input.GetMouseButtonUp (0) || GvrController.ClickButtonUp) {
				if (waitForUp) {
					Echo ();
					waitForUp = false;
				}
			}
			if (Input.GetMouseButton (0) || GvrController.ClickButton) {
				moveSpeed = sprintSpeed;
			} else if (Input.GetMouseButton(1) || GvrController.AppButton){
				moveSpeed = 0;
			} else
				moveSpeed = defaultSpeed;
			if (waitForPing || waitForUp)
				secs -= Time.deltaTime;
			if (secs <= 0) {
				waitForPing = false;
				waitForUp = false;
			}
		}
	}

	void FixedUpdate () {
		if (!endGame)
			transform.rotation = cameraPos.rotation;
		else {
			Vector3 rot = transform.forward;
			Quaternion rotate = Quaternion.LookRotation (transform.forward);
			rotate.x = 0;
			rotate.z = 0;
			transform.rotation = rotate;
		}
		transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

	}

	void Echo () {
		if (!waitForPing) {
			secs = pingDelay;
			waitForPing = true;
			Instantiate (pingSphere);
			int index = Random.Range (0, sounds.Length);
			whale.clip = sounds [index];
			whale.Play ();
		}
	}

}