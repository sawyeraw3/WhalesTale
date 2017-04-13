using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhalesInPod : MonoBehaviour {

	public int podCount;
	private GameObject followPoint;
	// Use this for initialization
	void Start () {
		podCount = 0;
		followPoint = GameObject.Find ("Focus");
	}
	
	// Update is called once per frame
	void Update () {
		switch (podCount) {
		case 0:
			followPoint.transform.localPosition = new Vector3 (0, 0, 0);
			break;
		case 1: 
			followPoint.transform.localPosition = new Vector3 (0, 0, -0.1f);
			break;
		case 2: 
			followPoint.transform.localPosition = new Vector3 (0, 0, -0.2f);
			break;
		case 3: 
			followPoint.transform.localPosition = new Vector3 (0, 0, -0.3f);
			break;
		case 4: 
			followPoint.transform.localPosition = new Vector3 (0, 0, -0.4f);
			break;
		case 5: 
			followPoint.transform.localPosition = new Vector3 (0, 0, -0.5f);
			break;
		default: 
			followPoint.transform.localPosition = new Vector3 (0, 0, -1f);
			break;
		}
	}
}
