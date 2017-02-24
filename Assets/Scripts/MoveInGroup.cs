using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInGroup : MonoBehaviour {

	//target that the object follows
	public GameObject targetPlayer;

	public float speed = .1f;

	//min and max times for picking a new random point around the player to mov toward
	float changePosTimerMin, changePosTimerMax, rotTimerMin, rotTimerMax, rotByVal;

	//random point around the player in a shpere
	Vector3 buffer;

	//timer for changing positions around the player
	float moveTimer;
	float rotateTimer;

	Vector3 toTarget;
	float rotateBy;

	public int MinDisFromWhale = 3;
	public int MaxDisFromWhale = 10;
	public float rotationSpeed = .8f;


	// Use this for initialization
	void Start () {


		//vars for timer until finding next position
		changePosTimerMax = 20f;
		changePosTimerMin = 10f;
		//vars for timer until cange rotation direction
		rotTimerMax = 20f;
		rotTimerMin = 12f;
		//var for how much to rotate by
		rotByVal = Random.Range(0, 15f);

		MinDisFromWhale = 5;
		MaxDisFromWhale = 20;

		moveTimer = Random.Range (changePosTimerMin, changePosTimerMax);
		rotateTimer = Random.Range (rotTimerMin, rotTimerMax);
		buffer = Random.insideUnitSphere * MaxDisFromWhale;
		rotateBy = Random.Range (-rotByVal, rotByVal);

		InvokeRepeating("Tick", 0, 1.0f);
	}

	//timer tick function
	void Tick(){
		moveTimer -= 1.0f;
		if (moveTimer <= 0) {
			NewPosition();
			//reset timer
			moveTimer = Random.Range (changePosTimerMin, changePosTimerMax);
		}

		rotateTimer -= 1.0f;
		if (rotateTimer <= 0) {
			rotateTimer = Random.Range (rotTimerMin, rotTimerMax);
			rotateBy = Random.Range (-rotByVal, rotByVal);
		}
	}
		
	//rotation and movement
	void LateUpdate (){


		//if you reach position, look forward
		if (Vector3.Distance(transform.position, (targetPlayer.transform.position + buffer)) < 5) {
			transform.rotation = Quaternion.Slerp (transform.rotation, 
				Quaternion.LookRotation ((targetPlayer.transform.forward)), 
				rotationSpeed * Time.fixedDeltaTime);
			transform.Rotate(new Vector3(0,0,rotateBy) * Time.fixedDeltaTime);

		}
		// else look at position you are moving to
		else {
			transform.rotation = Quaternion.Slerp (transform.rotation, 
				Quaternion.LookRotation (targetPlayer.transform.position + buffer - transform.position), 
				(rotationSpeed/2)* Time.fixedDeltaTime);
			transform.Rotate(new Vector3(0,0,rotateBy) * Time.fixedDeltaTime);

		}

		//update position
		toTarget = (targetPlayer.transform.position + buffer) - transform.position;
		transform.position += toTarget * speed * Time.fixedDeltaTime;
	

	}

	//move to new position
	void NewPosition(){
		buffer = Random.insideUnitSphere * MaxDisFromWhale;
		speed = Random.Range (.05f, .3f);
		//dont pick a point inside the whale
		while (Vector3.Distance((buffer + targetPlayer.transform.position), (targetPlayer.transform.position)) < 
			MinDisFromWhale){
			buffer = Random.insideUnitSphere * MaxDisFromWhale;
		}


	}

	void OnTriggerEnter(Collider other){
		if (this.tag == "Whale" && other.tag == "Whale") {
			NewPosition ();
		}
		if (this.tag == "Fish1" && other.tag == "Fish1") {
			NewPosition ();
		}


	}

}
