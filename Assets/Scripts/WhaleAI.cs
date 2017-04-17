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


		Random.seed = System.DateTime.Now.Millisecond;
		//newRandWait();
		minDisFromWhale = 5 + (script.podCount * 5f);//replace with number of whales in pod
		setBuffer();
		StartCoroutine (newPos());
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (Vector3.Distance (this.transform.position, player.position + buffer) > 10) {
			moveSpeed = 15f;
			transform.rotation = Quaternion.Slerp (transform.rotation, 
				Quaternion.LookRotation (player.position + buffer - transform.position), 
				rotSpeed * Time.fixedDeltaTime);
		}
		else {
			transform.rotation = Quaternion.Slerp (transform.rotation, player.rotation, rotSpeed * Time.fixedDeltaTime);
			moveSpeed = 7f;
		}

		transform.Translate (Vector3.forward * moveSpeed * Time.fixedDeltaTime);

		if (transform.position.y > -7) {
			transform.position = new Vector3 (transform.position.x, -7, transform.position.z);
		}
	}

	void FixedUpdate() {
		/*if (Vector3.Distance (player.position + buffer, player.position) < 10f) {
			setBuffer ();
		}*/
		GameObject[] whales = GameObject.FindGameObjectsWithTag ("Whale");
		foreach (GameObject w in whales) {
			if (Vector3.Distance (player.position + buffer, w.transform.position) < 5f) {
				setBuffer ();
			}
		}
	}

	IEnumerator newPos(){
		while (true) {
			newRandWait ();
			yield return new WaitForSeconds (randWait);
			setBuffer ();
		}

	}

	void setBuffer() {
		Random.seed = System.DateTime.Now.Millisecond;
		Vector3 toScale = new Vector3(minDisFromWhale,minDisFromWhale, -minDisFromWhale);	
		buffer = Random.insideUnitSphere; 
		buffer.z = Mathf.Abs (buffer.z);
		Vector3.Scale (buffer, toScale);
		Debug.Log (buffer);
	}

	void newRandWait(){
		Random.seed = System.DateTime.Now.Millisecond;
		//min and max wait time for new position
		randWait = Random.Range (8f, 14f);
	}
		
}
