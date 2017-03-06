using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanFollowPlayer : MonoBehaviour {
	public GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = new Vector3 (player.transform.position.x, 0, player.transform.position.z);
	}
}
