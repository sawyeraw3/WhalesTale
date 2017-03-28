using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echolocate : MonoBehaviour {

	public float radius;
	public float expandSpeed;

	// Use this for initialization
	void Start () {
		radius *= 10000;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.localScale += new Vector3 (expandSpeed, expandSpeed, expandSpeed); 
		if (this.transform.lossyScale.magnitude >= radius)
			DestroyImmediate (this.gameObject);


	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "fishhead") {
			Debug.Log ("anus");
		}

	}

	void OnTriggerEnter(Collider c) {
		Debug.Log (c.transform.position);
	}
}
