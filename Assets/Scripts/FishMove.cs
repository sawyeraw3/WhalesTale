using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMove : MonoBehaviour {
	Vector3 toTarget;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		toTarget = (gameObject.transform.position + new Vector3(0,0,1)) - transform.position;
		transform.position += toTarget * 10f * Time.fixedDeltaTime;
	}
}
