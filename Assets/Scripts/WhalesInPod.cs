using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Cameras;
using UnityEngine.SceneManagement;

public class WhalesInPod : MonoBehaviour {

	public int podCount;
	private GameObject followPoint;	
	private GameObject player;
	private Animator camAnim;
	private GameObject gameCamera;
	bool ending = false;
	bool cameraRising = false;


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
		if (cameraRising && gameCamera.transform.position.y < 8.5f) {
			gameCamera.transform.Translate (Vector3.up * Time.deltaTime * 5);
		}
		if (player.transform.position.y > 0 || GvrController.ClickButton || Input.GetMouseButton(0))
			followPoint.transform.localPosition = Vector3.zero;
		else if (GvrController.AppButton || Input.GetMouseButton(1)) {
			followPoint.transform.localPosition = new Vector3 (0, 0, -0.2f);
		} else {
			switch (podCount) {
			case 0:
				followPoint.transform.localPosition = Vector3.zero;
				break;
			case 1: 
				followPoint.transform.localPosition = new Vector3 (0, 0, -0.1f);
				break;
			case 2: 
				followPoint.transform.localPosition = new Vector3 (0, 0, -0.2f);
				break;
			case 3: 
				followPoint.transform.localPosition = new Vector3 (0, 0, -0.3f);
				break;
			case 4: 
				followPoint.transform.localPosition = new Vector3 (0, 0, -0.4f);
				break;
			default: 
				EndGame ();
				break;
			}
		}
	}

	void EndGame() {
		if (!ending) {
			GameObject.Find ("OceanFloor").GetComponent<GenerateInfinite> ().enabled = false;
			GameObject.Find ("Ocean").GetComponent<OceanFollowPlayer> ().enabled = false;
			foreach (GameObject g in GameObject.FindGameObjectsWithTag("Boat"))
				Destroy (g);
			player.GetComponent<WhaleControl> ().endGame = true;
			gameCamera.GetComponentInChildren<FreeLookCam> ().enabled = false;
			ending = true;
			StartCoroutine (MoveCamera ());
			StartCoroutine (LoadMenu ());
		}
	}

	IEnumerator MoveCamera() {
		yield return new WaitForSeconds (3);
		cameraRising = true;
	}

	IEnumerator LoadMenu() {

		yield return new WaitForSeconds(20);
		// Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
		AsyncOperation async = SceneManager.LoadSceneAsync(0); //while the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
		while (!async.isDone) {
			yield return null;
		}

	}
}
