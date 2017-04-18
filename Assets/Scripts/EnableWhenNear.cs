using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableWhenNear : MonoBehaviour {

	private bool isEnabled = false;
	private Transform playerLoc;
	public float distance = 110f;
	public float destroyDistance = 350f;

	// Use this for initialization
	void Start () {
		playerLoc = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	void FixedUpdate() {
		if (Vector3.Distance (this.transform.position, playerLoc.position) < distance)
			isEnabled = true;
		else if (Vector3.Distance (this.transform.position, playerLoc.position) > destroyDistance)
			Destroy (this.gameObject);
		else {
			isEnabled = false;
		}
	}

	void Update () {
		toggleRenderer ();
	}

	void toggleRenderer() {
		Renderer render = this.GetComponent<Renderer> ();
		if (render) {
			render.enabled = isEnabled;
		}	

		foreach (Renderer r in this.GetComponentsInChildren<Renderer>()) {
			r.enabled = isEnabled;
		}
	}
}
