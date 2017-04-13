﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePrefab : MonoBehaviour {

	public bool isRock = false;
	public bool normalize = false;

	// Use this for initialization
	void Start () {
		this.GetComponentInChildren<Renderer> ().enabled = false;
		rotate ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void rotate(){
		Random.seed = System.DateTime.Now.Millisecond;
		if(isRock)
			transform.localRotation = Random.rotation;
		if (normalize) {
			Vector3 startPos = transform.position;
			startPos.y += 10;
			LayerMask myLayerMask = 1 << 9;
			RaycastHit hit;
			Ray ray = new Ray (startPos, Vector3.down);
			if (Physics.Raycast (ray, out hit, 15, myLayerMask)) {
				if (hit.collider.tag == "floor") {
					this.transform.rotation = Quaternion.FromToRotation (transform.up, hit.normal) * transform.rotation;
				}
			}
		}
		this.GetComponentInChildren<Renderer> ().enabled = true;
	}
}