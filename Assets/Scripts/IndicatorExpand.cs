using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorExpand : MonoBehaviour {

	public int howFar;
	public int radius;
	public int expandSpeed;
	public Material[] colors;
	// Use this for initialization
	void Start () {
		Debug.Log ("HELLO WORLD");
		switch (howFar) {
		case 1:
			this.GetComponent<Renderer> ().material = colors [0];
			break;
		case 2:
			this.GetComponent<Renderer> ().material = colors [1];
			break;
		case 3:
			this.GetComponent<Renderer> ().material = colors [2];
			break;
		case 4: 
			this.GetComponent<Renderer> ().material = colors [3];
			break;
		}
	}

	// Update is called once per frame
	void Update () {
		this.transform.LookAt (GameObject.FindGameObjectWithTag ("Player").transform.position);
		this.transform.localScale += new Vector3 (expandSpeed, expandSpeed, expandSpeed); 
		if (this.transform.lossyScale.magnitude >= radius)
			DestroyImmediate (this.gameObject);
	}
}
