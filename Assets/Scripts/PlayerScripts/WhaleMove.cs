using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleMove : MonoBehaviour {

	public Transform target;
	public float moveSpeed = 5.0f;
	public float rotationSpeed = 0.1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		MoveTowardsTarget ();

	}

	void MoveTowardsTarget() {
		transform.rotation = Quaternion.Slerp ( target.rotation, transform.rotation, Time.deltaTime * rotationSpeed);
		float step = moveSpeed * Time.deltaTime;
		this.transform.position = Vector3.MoveTowards (transform.position, target.position, step);
	}

}
