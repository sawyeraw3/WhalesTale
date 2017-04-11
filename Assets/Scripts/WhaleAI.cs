using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleAI : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.Slerp (transform.rotation, 
			Quaternion.LookRotation (player.transform.position - transform.position), 
			Time.fixedDeltaTime);
	}
}
