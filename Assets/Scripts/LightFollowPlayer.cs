using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFollowPlayer : MonoBehaviour {
	public GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (player.transform.position.x + 10f, 0, player.transform.position.z + 10f);
		transform.RotateAround (player.transform.position, Vector3.up, 10f * Time.deltaTime);
	}
}
