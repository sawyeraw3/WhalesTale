using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPing : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collision col){

		if (col.gameObject.tag == "ping") {
			Debug.Log ("anus");
		}
	}
}
