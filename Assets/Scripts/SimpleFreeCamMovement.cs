using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFreeCamMovement : MonoBehaviour {

	public float sensitivity;
	public float speed;

	float xLook = 0f;
	float yLook = 0f;

	float xMove = 0f;
	float yMove = 0f;

	Quaternion originalRot;

	// Use this for initialization
	void Start () {
		originalRot = transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
		xLook += Input.GetAxis ("Mouse X") * sensitivity;
		yLook += Input.GetAxis ("Mouse Y") * sensitivity;


		Quaternion xQuat = Quaternion.AngleAxis (xLook, Vector3.up);
		Quaternion yQuat = Quaternion.AngleAxis (yLook, -Vector3.right);

		transform.localRotation = originalRot * xQuat * yQuat;

		xMove = Input.GetAxis ("Horizontal");
		yMove = Input.GetAxis ("Vertical");

		Vector3 moveDir = new Vector3 (xMove, 0f, yMove);

		transform.Translate (moveDir * speed);
	}
}
