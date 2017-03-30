﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echolocate : MonoBehaviour {

	public float radius;
	public float expandSpeed;
	public GameObject indicator;

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

	void OnTriggerEnter(Collider c) {
		float dist = Vector3.Distance (this.transform.position, c.transform.position);
		Debug.Log (dist);
		if (dist > 25) {
			GameObject temp = Instantiate (indicator, c.transform);
			if(dist < 50)
				temp.GetComponent<IndicatorExpand> ().howFar = 4;
			else if(dist < 75)
				temp.GetComponent<IndicatorExpand> ().howFar = 3;
			else if(dist < 100)
				temp.GetComponent<IndicatorExpand> ().howFar = 2;
			else if(dist > 100)
				temp.GetComponent<IndicatorExpand> ().howFar = 1;
			
		}
		//temp.GetComponent<IndicatorExpand> ().howFar = dist;
	}
}
