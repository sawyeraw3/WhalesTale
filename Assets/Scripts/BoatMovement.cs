using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour {

	public GameObject whale;
	public GameObject boat;
	public float boatSpeed = .1f;
	public float maxDistance = 110f;
	private int forward = 1;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (whale.transform.position, boat.transform.position) > maxDistance) {
			boat.transform.Rotate (Vector3.up * 180);
			forward *= -1;
		}
		this.transform.Translate (Vector3.back * forward * boatSpeed);
	
	}
}
