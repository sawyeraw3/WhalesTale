using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndLook : MonoBehaviour {

	private Transform cameraPos;
	public float moveSpeed = 10f;

	// Use this for initialization
	void Start () {
		cameraPos = GameObject.FindGameObjectWithTag ("MainCamera").transform;
	}

	void FixedUpdate () {
		transform.rotation = cameraPos.rotation;
		transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
	}
}
