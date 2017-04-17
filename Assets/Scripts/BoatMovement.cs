using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour {

	public GameObject whale;
	public GameObject boat;
	public float boatSpeed = .1f;
	public float maxDistance = 200f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (whale.transform.position, boat.transform.position) > maxDistance) {
			Destroy(this.gameObject);
		}
		this.transform.Translate (Vector3.forward  * boatSpeed);

	}
}
