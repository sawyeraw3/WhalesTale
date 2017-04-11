using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderWhenNear : MonoBehaviour {

	private bool isEnabled = false;
	private Transform playerLoc;
	// Use this for initialization
	void Start () {
		playerLoc = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	void FixedUpdate() {
		if (Vector3.Distance (this.transform.position, playerLoc.position) < 90)
			isEnabled = true;
		else
			isEnabled = false;
	}

	void Update () {
		if (isEnabled) {
			this.GetComponentInChildren<Renderer> ().enabled = true;
		} else
			this.GetComponentInChildren<Renderer> ().enabled = false;
	}
}
