using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleInPodAI : MonoBehaviour {

	//target that the object follows
	public GameObject targetPlayer;

	public float speed = .1f;

	//min and max times for picking a new random point around the player to mov toward
	float changePosTimerMin, changePosTimerMax;

	//random point around the player in a shpere
	Vector3 buffer;

	//timer for changing positions around the player
	float timer;

	Vector3 toTarget;

	public int DisFromWhaleMin = 3;
	public int DisFromWhaleMax = 10;
	public float rotationSpeed = .8f;


	// Use this for initialization
	void Start () {
		changePosTimerMax = 20f;
		changePosTimerMin = 10f;
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
	void LateUpdate (){


		//if you reach position
		if (Vector3.Distance(transform.position, (targetPlayer.transform.position + buffer)) < 5) {
			//NewPosition ();
			//Debug.Log("HI");
			transform.rotation = Quaternion.Slerp (transform.rotation, 
				Quaternion.LookRotation ((targetPlayer.transform.up)), 
				rotationSpeed * Time.fixedDeltaTime);
			if (timer > 5f)
				timer = 5f;
			
		} else {

			transform.rotation = Quaternion.Slerp (transform.rotation, 
				Quaternion.LookRotation (targetPlayer.transform.position + buffer - transform.position), 
				(rotationSpeed/2)* Time.fixedDeltaTime);
		}

		toTarget = (targetPlayer.transform.position + buffer) - transform.position;
		transform.position += toTarget * speed * Time.fixedDeltaTime;
	

	}

	//move to new position
	void NewPosition(){
		buffer = Random.insideUnitSphere * DisFromWhaleMax;
		speed = Random.Range (.05f, .5f);
		//dont pick a point inside the whale
		while (Vector3.Distance((buffer + targetPlayer.transform.position), (targetPlayer.transform.position)) < 
			DisFromWhaleMin){
			buffer = Random.insideUnitSphere * DisFromWhaleMax;
		}


	}
}
