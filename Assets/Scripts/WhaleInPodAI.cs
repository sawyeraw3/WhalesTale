using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleInPodAI : MonoBehaviour {

	//target that the object follows
	public GameObject targetPlayer;

	float speed;

	//min and max times for picking a new random point around the player to mov toward
	float changePosTimerMin, changePosTimerMax;

	//random point around the player in a shpere
	Vector3 buffer;

	//timer for changing positions around the player
	float timer;

	Vector3 toTarget;

	public int DisFromWhaleMin = 3;
	public int DisFromWhaleMax = 10;
	public int rotationSpeed = 2;


	// Use this for initialization
	void Start () {
		speed = .5f;
		changePosTimerMax = 9f;
		changePosTimerMin = 3f;
		timer = Random.Range (changePosTimerMin, changePosTimerMax);
		buffer = Random.insideUnitSphere * DisFromWhaleMax;

		InvokeRepeating("Tick", 0, 1.0f);
	}

	void Tick(){
		timer -= 1.0f;
		if (timer <= 0) {
			NewPosition();
			//reset timer
			timer = Random.Range (changePosTimerMin, changePosTimerMax);
		}
	}


	// Update is called once per frame
	void Update () {

	}

	//rotqation and movement
	void FixedUpdate (){
		transform.rotation = Quaternion.Slerp (transform.rotation, 
			Quaternion.LookRotation (targetPlayer.transform.position + buffer - transform.position), rotationSpeed * Time.deltaTime);

		toTarget = (targetPlayer.transform.position + buffer) - transform.position;
		transform.position += toTarget * speed * Time.deltaTime;

	}

	//move to new position
	void NewPosition(){
		buffer = Random.insideUnitSphere * DisFromWhaleMax;

		//dont pick a point inside the whale
		while (Vector3.Distance((buffer + targetPlayer.transform.position), (targetPlayer.transform.position)) < 
			DisFromWhaleMin){
			buffer = Random.insideUnitSphere * DisFromWhaleMax;
		}


	}
}
