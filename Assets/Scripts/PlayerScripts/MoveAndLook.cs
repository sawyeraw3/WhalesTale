using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleController : MonoBehaviour {

	private Transform cameraPos;
	public float moveSpeed = 10f;
	public float defaultSpeed = 10f;
	public float sprintSpeed = 20f;

	// Use this for initialization
	void Start () {
		cameraPos = GameObject.FindGameObjectWithTag ("MainCamera").transform;
	}

	void Update() {
		if (Input.GetMouseButtonDown(0))
			
		if (Input.GetMouseButton (0))
			moveSpeed = sprintSpeed;
		else
			moveSpeed = defaultSpeed;
	}

	void FixedUpdate () {
		transform.rotation = cameraPos.rotation;
		transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
	}

	void Echo () {

	}
		
}