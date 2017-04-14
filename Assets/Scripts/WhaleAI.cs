using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleAI : MonoBehaviour {

	public GameObject player;
	public float rotSpeed;
	public float moveSpeed;
	public float randWait;
	public float MaxDisFromWhale;
	Vector3 buffer;

	// Use this for initialization
	void Start () {
		GameObject p = GameObject.Find("whale");
		player = p;
		GameObject podNum = GameObject.Find ("GameManager");
		WhalesInPod script = podNum.GetComponent<WhalesInPod> ();

		newRandWait();
		MaxDisFromWhale = 12 + (script.podCount * 5f);//replace with number of whales in pod
		buffer = Random.insideUnitSphere * MaxDisFromWhale;
		StartCoroutine (newPos());
		rotSpeed = .5f;
		moveSpeed = 10f;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.rotation = Quaternion.Slerp (transform.rotation, 
			Quaternion.LookRotation (player.transform.position + buffer - transform.position), 
			rotSpeed * Time.fixedDeltaTime);
		transform.Translate (Vector3.forward * moveSpeed * Time.fixedDeltaTime);

		if (transform.position.y > -7) {
			transform.position = new Vector3 (transform.position.x, -7, transform.position.z);
		}
	}

	IEnumerator newPos(){
		while (true) {
			newRandWait ();
			yield return new WaitForSeconds (randWait);
			Random.seed = System.DateTime.Now.Millisecond;
			//min and max move speed
			moveSpeed = Random.Range (10, 15);
			buffer = Random.insideUnitSphere * MaxDisFromWhale;

		}

	}

	void newRandWait(){
		Random.seed = System.DateTime.Now.Millisecond;
		//min and max wait time for new position
		randWait = Random.Range (5f, 10f);
	}
		
}
