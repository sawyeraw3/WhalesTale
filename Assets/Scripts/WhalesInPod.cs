using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class WhalesInPod : MonoBehaviour {

	public int podCount;
	private GameObject followPoint;	
	private GameObject player;
	private Animator camAnim;
	private GameObject gameCamera;


	// Use this for initialization
	void Start () {
		podCount = 0;
		followPoint = GameObject.Find ("Focus");
		player = GameObject.FindGameObjectWithTag ("Player");
		gameCamera = GameObject.Find ("FreeLookCameraRig");
		camAnim = gameCamera.GetComponent<Animator> ();


	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (player.transform.position.y > 0)
			followPoint.transform.localPosition = Vector3.zero;
		else {
			switch (podCount) {
			case 0:
				followPoint.transform.localPosition = Vector3.zero;
				break;
			case 1: 
				followPoint.transform.localPosition = new Vector3 (0, 0, -0.05f);
				break;
			case 2: 
				followPoint.transform.localPosition = new Vector3 (0, 0, -0.1f);
				break;
			case 3: 
				followPoint.transform.localPosition = new Vector3 (0, 0, -0.15f);
				break;
			case 4: 
				followPoint.transform.localPosition = new Vector3 (0, 0, -0.2f);
				break;
			case 5: 
				followPoint.transform.localPosition = new Vector3 (0, 0, -0.25f);
				break;
			case 6: 
				followPoint.transform.localPosition = new Vector3 (0, 0, -0.3f);
				break;
			case 7: 
				followPoint.transform.localPosition = new Vector3 (0, 0, -0.35f);
				break;
			case 8: 
				followPoint.transform.localPosition = new Vector3 (0, 0, -0.4f);
				break;
			default: 
				followPoint.transform.localPosition = new Vector3 (0, 0, -0.5f);
				break;
			}
		}
	}

	void EndGame() {
		player.GetComponent<WhaleControl> ().endGame = true;

	}
}
