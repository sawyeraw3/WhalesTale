using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Cameras;
using UnityEngine;

public class ActivateWhale : MonoBehaviour {

	private Animator camAnim;
	private GameObject gameCamera;
	private GameObject player;
	public bool debugging = false;
	// Use this for initialization
	void Start () {
		gameCamera = GameObject.Find ("FreeLookCameraRig");
		camAnim = gameCamera.GetComponent<Animator> ();
		camAnim.SetTrigger ("Startgame");
		player = GameObject.Find ("whale");
		if (debugging) {
			Activate ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!debugging && camAnim.GetCurrentAnimatorStateInfo (0).normalizedTime > 1 && !camAnim.IsInTransition (0))
		{
			Activate ();
		}
	}

	void Activate() {
		camAnim.enabled = false;
		gameCamera.GetComponentInChildren<FreeLookCam> ().enabled = true;
		gameCamera.GetComponentInChildren<ProtectCameraFromWallClip> ().enabled = true;
		player.GetComponent<WhaleControl> ().enabled = true;
		this.enabled = false;
	}
}
