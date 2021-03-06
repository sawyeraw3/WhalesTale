﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorExpand : MonoBehaviour {

	public int howFar;
	public int radius;
	public float expandSpeed;
	public Material[] colors;
	private Transform player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	// Update is called once per frame
	void FixedUpdate () {
		switch (howFar) {
		case 1:
			this.GetComponent<Renderer> ().material = colors [0];
			radius = 20;
			expandSpeed = 0.125f;
			break;
		case 2:
			this.GetComponent<Renderer> ().material = colors [1];
			radius = 30;
			expandSpeed = 0.15f;
			break;
		case 3:
			this.GetComponent<Renderer> ().material = colors [2];
			radius = 40;
			expandSpeed = 0.175f;
			break;
		case 4: 
			this.GetComponent<Renderer> ().material = colors [3];
			radius = 50;
			expandSpeed = 0.2f;
			break;
		default:
			this.GetComponent<Renderer> ().enabled = false;
			break;
		}
		float dist = Vector3.Distance (this.transform.position, player.position);
		this.transform.LookAt (player.position);
		this.transform.Rotate (0, 180, 0);
		this.transform.localScale += new Vector3 (expandSpeed, expandSpeed, expandSpeed); 
		if (this.transform.lossyScale.magnitude >= radius || dist < 30)
			DestroyImmediate (this.gameObject);
		else if(dist < 110)
			howFar = 4;
		else if(dist < 150)
			howFar = 3;
		else if(dist < 200)
			howFar = 2;
		else if(dist > 250)
			howFar = 1;
	}
}
