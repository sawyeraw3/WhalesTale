﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpencerTestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (transform.forward * Time.deltaTime * 2f);
	}
}