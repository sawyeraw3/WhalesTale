using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleAI : MonoBehaviour {

	public Transform player;
	public float rotSpeed;
	public float moveSpeed;
	public float randWait;
	public float minDisFromWhale = 10f;
	public Transform buffer;
	private int currPos;
	GameObject[] positions;

	// Use this for initialization
	void Start () {
		positions = GameObject.FindGameObjectsWithTag ("PodPos");

		GameObject p = GameObject.FindGameObjectWithTag("Player");
		player = p.transform;
		GameObject podNum = GameObject.Find ("GameManager");
		WhalesInPod script = podNum.GetComponent<WhalesInPod> ();


		Random.InitState(System.DateTime.Now.Millisecond);
		//newRandWait();
		minDisFromWhale = 5 + (script.podCount * 5f);//replace with number of whales in pod
		setBuffer(script.podCount);
		currPos = script.podCount;
		StartCoroutine (newPos());
	}


	void FixedUpdate() {
		if (!buffer)
			nextBuffer ();
		if (Vector3.Distance (this.transform.position, buffer.position) > 5) {
			moveSpeed = 15f;
			transform.rotation = Quaternion.Slerp (transform.rotation, 
				Quaternion.LookRotation (buffer.position - transform.position), 
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
		GameObject[] whales = GameObject.FindGameObjectsWithTag ("Whale");
		foreach (GameObject w in whales) {
			if (buffer == w.GetComponent<WhaleAI>().buffer && !this.gameObject.Equals(w)) {
				//w.GetComponent<WhaleAI> ().nextBuffer ();
				nextBuffer();
			}
		}
	}

	IEnumerator newPos(){
		while (true) {
			newRandWait ();
			yield return new WaitForSeconds (randWait);
			//nextBuffer ();
			Vector3 newPos = buffer.position;
			newPos.z = Random.Range (-1, -15);
			//buffer.localPosition = newPos;
			nextBuffer ();
		}

	}

	void setBuffer(int buffNum) {
		//Random.seed = System.DateTime.Now.Millisecond;
		//int buffNum = Random.Range (0, positions.Length);
		buffer = positions[buffNum].transform;
		/*Vector3 toScale = new Vector3(minDisFromWhale,minDisFromWhale, -minDisFromWhale);	
		buffer = Random.insideUnitSphere; 
		buffer.z = Mathf.Abs (buffer.z);
		Vector3.Scale (buffer, toScale);*/ 
	}

	public void nextBuffer() {
		currPos += 1;
		if (currPos >= positions.Length)
			currPos = 0;
		buffer = positions [currPos].transform;
	}

	void newRandWait(){
		Random.InitState(System.DateTime.Now.Millisecond);
		//min and max wait time for new position
		randWait = Random.Range (20f, 30f);
	}
		
}
