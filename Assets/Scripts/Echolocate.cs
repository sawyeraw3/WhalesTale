﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echolocate : MonoBehaviour {

	public float radius;
	public float expandSpeed;
	public GameObject indicator;
	public Transform playerLoc;

	// Use this for initialization
	void Start () {
		radius *= 10000;
		playerLoc = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = playerLoc.position;
		this.transform.localScale += new Vector3 (expandSpeed, expandSpeed, expandSpeed); 
		if (this.transform.lossyScale.magnitude >= radius)
			DestroyImmediate (this.gameObject);


	}

	void OnTriggerEnter(Collider c) {
		float dist = Vector3.Distance (this.transform.position, c.transform.position);
		Debug.Log (dist);
		if (dist > 55) {
			GameObject temp = Instantiate (indicator);
			temp.transform.position = c.transform.position;
			if(c.transform.parent)
				temp.transform.position = c.transform.parent.position;
			if(dist < 110)
				temp.GetComponent<IndicatorExpand> ().howFar = 4;
			else if(dist < 150)
				temp.GetComponent<IndicatorExpand> ().howFar = 3;
			else if(dist < 200)
				temp.GetComponent<IndicatorExpand> ().howFar = 2;
			else if(dist > 250)
				temp.GetComponent<IndicatorExpand> ().howFar = 1;
			
		}
	}
}
