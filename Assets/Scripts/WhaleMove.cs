using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleMove : MonoBehaviour {

	public Transform target;
	public float moveSpeed = 5.0f;
	public float rotationSpeed = 2.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		MoveTowardsTarget ();
	}

	void MoveTowardsTarget() {
		Vector3 direction = (target.position - transform.position);
		Quaternion lookRotation = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.Slerp (transform.rotation, target.rotation, Time.deltaTime * rotationSpeed);
		float step = moveSpeed * Time.deltaTime;
		this.transform.position = Vector3.MoveTowards (transform.position, target.position, step);
	}

}
