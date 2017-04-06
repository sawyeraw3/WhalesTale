using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Cameras;
using UnityEngine;

public class ActivateWhale : MonoBehaviour {

	private Animator camAnim;
	private GameObject gameCamera;
	private GameObject player;
	// Use this for initialization
	void Start () {
		gameCamera = GameObject.Find ("FreeLookCameraRig");
		camAnim = gameCamera.GetComponent<Animator> ();
		player = GameObject.Find ("whale");
	}
	
	// Update is called once per frame
	void Update () {
		if (camAnim.GetCurrentAnimatorStateInfo (0).normalizedTime > 1 && !camAnim.IsInTransition (0))
		{
			camAnim.enabled = false;
			gameCamera.GetComponent<FreeLookCam> ().enabled = true;
			gameCamera.GetComponent<ProtectCameraFromWallClip> ().enabled = true;
			player.GetComponent<WhaleControl> ().enabled = true;
		}
	}
}
