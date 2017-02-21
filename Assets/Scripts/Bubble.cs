using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {

	public float bubbleSpeed;
	public float waterHeight;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position += Vector3.up * bubbleSpeed * Time.deltaTime;
		if (gameObject.transform.position.y >= waterHeight) {
			Destroy (this.gameObject);
		}
	}
}
