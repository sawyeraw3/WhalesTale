using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAboveWater : MonoBehaviour {


	public Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (this.transform.position.y > 0) {
			if (rb.useGravity != true) {
				rb.useGravity = true;
				rb.AddForce (transform.up * 500f);
				rb.AddForce (transform.forward * 500f);

				
			}
			
		}
		if (this.transform.position.y < 0) {
			if (rb.useGravity != false)
				rb.useGravity = false;
		}
	}
}
