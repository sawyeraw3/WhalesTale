using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFollowPlayer : MonoBehaviour {
	public GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (player.transform.position.y + 10 > 0)
			transform.position = new Vector3 (player.transform.position.x, -1, player.transform.position.z);
		else
			transform.position = new Vector3 (player.transform.position.x, player.transform.position.y + 10, player.transform.position.z);

	}
}
