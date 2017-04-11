using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderWhenNear : MonoBehaviour {

	private bool isEnabled = false;
	private Transform playerLoc;
	public float distance = 110f;

	// Use this for initialization
	void Start () {
		playerLoc = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	void FixedUpdate() {
		if (Vector3.Distance (this.transform.position, playerLoc.position) < distance)
			isEnabled = true;
		else
			isEnabled = false;
	}

	void Update () {
		if (isEnabled) {
			toggleRenderer (true);
		} else
			toggleRenderer (false);
	}

	void toggleRenderer(bool enable) {
		Renderer render = this.GetComponent<Renderer> ();
		if (render) {
			render.enabled = enable;
		}

		foreach (Renderer r in this.GetComponentsInChildren<Renderer>()) {
			r.enabled = enable;
		}
	}
}
