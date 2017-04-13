using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhalesInPod : MonoBehaviour {

	public int podCount;
	private GameObject followPoint;
	private Transform playerPos;
	// Use this for initialization
	void Start () {
		podCount = 0;
		followPoint = GameObject.Find ("Focus");
		playerPos = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (playerPos.position.y > 0)
			followPoint.transform.localPosition = Vector3.zero;
		else {
			switch (podCount) {
			case 0:
				followPoint.transform.localPosition = Vector3.zero;
				break;
			case 1: 
				followPoint.transform.localPosition = new Vector3 (0, 0, -0.05f);
				break;
			case 2: 
				followPoint.transform.localPosition = new Vector3 (0, 0, -0.1f);
				break;
			case 3: 
				followPoint.transform.localPosition = new Vector3 (0, 0, -0.15f);
				break;
			case 4: 
				followPoint.transform.localPosition = new Vector3 (0, 0, -0.2f);
				break;
			case 5: 
				followPoint.transform.localPosition = new Vector3 (0, 0, -0.25f);
				break;
			case 6: 
				followPoint.transform.localPosition = new Vector3 (0, 0, -0.3f);
				break;
			case 7: 
				followPoint.transform.localPosition = new Vector3 (0, 0, -0.35f);
				break;
			case 8: 
				followPoint.transform.localPosition = new Vector3 (0, 0, -0.4f);
				break;
			default: 
				followPoint.transform.localPosition = new Vector3 (0, 0, -1f);
				break;
			}
		}
	}
}
