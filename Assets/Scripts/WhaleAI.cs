using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleAI : MonoBehaviour {

	public Transform player;
	public float rotSpeed;
	public float moveSpeed;
	public float randWait;
	public float minDisFromWhale = 10f;
	Vector3 buffer;

	// Use this for initialization
	void Start () {
		GameObject p = GameObject.FindGameObjectWithTag("Player");
		player = p.transform;
		GameObject podNum = GameObject.Find ("GameManager");
		WhalesInPod script = podNum.GetComponent<WhalesInPod> ();

		//newRandWait();
		minDisFromWhale = (script.podCount * 2f);//replace with number of whales in pod
		Vector3 toScale = new Vector3(minDisFromWhale,minDisFromWhale,0);
		buffer = Random.insideUnitSphere; 
		buffer.Scale (toScale);
		Debug.Log (buffer);
		//StartCoroutine (newPos());
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (Vector3.Distance (this.transform.position, player.position + buffer) > 6) {
			Debug.Log ("lost");
			moveSpeed = 20f;
			transform.rotation = Quaternion.Slerp (transform.rotation, 
				Quaternion.LookRotation (player.position + buffer - transform.position), 
				rotSpeed * Time.fixedDeltaTime);
		}
		else {
			transform.rotation = Quaternion.Slerp (transform.rotation, player.rotation, rotSpeed * Time.fixedDeltaTime);
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
			Vector3 toScale = new Vector3(minDisFromWhale,minDisFromWhale,0);	
			buffer = Random.insideUnitSphere; 
			buffer += new Vector3 (1, 1, 1);
			buffer.Scale (toScale);

		}

	}

	void newRandWait(){
		Random.seed = System.DateTime.Now.Millisecond;
		//min and max wait time for new position
		randWait = Random.Range (5f, 10f);
	}
		
}
