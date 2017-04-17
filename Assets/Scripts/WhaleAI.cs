using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleAI : MonoBehaviour {

	public Transform player;
	public float rotSpeed;
	public float moveSpeed;
	public float randWait;
	public float MaxDisFromWhale;
	Vector3 buffer;

	// Use this for initialization
	void Start () {
		GameObject p = GameObject.Find("whale");
		player = p.transform;
		GameObject podNum = GameObject.Find ("GameManager");
		WhalesInPod script = podNum.GetComponent<WhalesInPod> ();

		//newRandWait();
		MaxDisFromWhale = 12 + (script.podCount * 5f);//replace with number of whales in pod
		buffer = Random.insideUnitSphere * MaxDisFromWhale;
		Debug.Log (buffer);
		//StartCoroutine (newPos());
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (Vector3.Distance (this.transform.localPosition, buffer) > 10) {
			moveSpeed = 20f;
			transform.rotation = Quaternion.Slerp (transform.rotation, 
				Quaternion.LookRotation (player.position + buffer - transform.position), 
				rotSpeed * Time.fixedDeltaTime);
		}
		else {
			Debug.Log ("arrive");
			transform.rotation = player.rotation;
			moveSpeed = 10f;
		}

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
			moveSpeed = 20f;
			buffer = Random.insideUnitSphere * MaxDisFromWhale;

		}

	}

	void newRandWait(){
		Random.seed = System.DateTime.Now.Millisecond;
		//min and max wait time for new position
		randWait = Random.Range (5f, 10f);
	}
		
}
