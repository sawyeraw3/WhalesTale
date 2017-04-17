using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOffAboveWater : MonoBehaviour {

	private GameObject player;
	private Light l;
	// Use this for initialization
	void Start () {
		player = GameObject.Find("FreeLookCameraRig");
		l = this.gameObject.GetComponent<Light>();

	}
	
	// Update is called once per frame
	void Update () {
		if (player.transform.position.y > 0){
			l.enabled = false;
		} 
		else {
			l.enabled = true;
		}
	}
}
