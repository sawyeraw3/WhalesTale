using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMove : MonoBehaviour {

	public Vector3 direction;

	// Use this for initialization
	void Start () {
		direction = Vector3.forward;
		transform.LookAt (direction);
	}

	// Update is called once per frame
	void Update () {
		direction = Vector3.forward;
		transform.LookAt (direction);
	}
}
