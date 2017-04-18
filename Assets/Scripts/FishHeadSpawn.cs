using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHeadSpawn : MonoBehaviour {

	private Vector3 dir;
	private float rNum;
	private Vector3 spread;
	public GameObject[] fishPrefabs;
	public float fishRotSpeed;
	public float headMoveSpeed;
	private bool rotatingOrSchooling;
	public float minDistance;
	public GameObject player;
	private bool started = false;
	private bool scanning = false;
	private bool flee = false;
	private bool returning = false;
	private bool shark = false;

	// Use this for initialization
	void Start () {
		scanning = false;
		flee = false;
		returning = false;
		this.headMoveSpeed = 1 + Random.value * 2;
		this.transform.eulerAngles = Vector3.zero;
		/*this.transform.position = player.transform.position;
		if (started) {
			this.transform.position += Random.insideUnitSphere * minDistance * .75f;
			if (this.transform.position.y > -15) {
				this.transform.position = new Vector3 (this.transform.position.x, -15, this.transform.position.z);
			} else if (this.transform.position.y < -35) {
				this.transform.position = new Vector3 (this.transform.position.x, -35, this.transform.position.z);
			}
		} else {
			this.transform.position += Vector3.forward * minDistance / 4;
		}*/
		started = true;
		rNum = Random.Range (7f, 15f);
		int fishIndex = Random.Range (0, fishPrefabs.Length);

		int pattern = Random.Range (0, 2);
		if(pattern == 0) {
			rotatingOrSchooling = false; //rotating
		}
		else {
			rotatingOrSchooling = false; //schooling
		}

		bool reduced = false;
		for (int i = 0; i < rNum; i++) {
			spread = Random.insideUnitSphere * Random.Range(4, 8);
			GameObject fish = Instantiate(fishPrefabs[fishIndex]) as GameObject;
			if (fish.name.Contains ("Shark") && !reduced) {
				rNum /= 4;
				reduced = true;
				shark = true;
			}
			fish.transform.parent = this.transform;
			fish.transform.localPosition = Vector3.zero;
			fish.transform.localEulerAngles = Vector3.zero;

			GameObject fishObj = fish.transform.FindChild("fishModel").gameObject;
			Transform look = fish.transform.FindChild ("LookAt");
			fishObj.transform.localPosition = spread;
			
			look.transform.localPosition = spread + Vector3.forward;
		}
		Random.seed = System.DateTime.Now.Millisecond;
		dir = new Vector3 (Random.value + Random.Range (-1, 1), Random.value + Random.Range (-1, 1), Random.value + Random.Range (-1, 1));
		while (dir.x == 0) {
			dir.Set (Random.value, dir.y, dir.z);
		}
		while (dir.y == 0) {
			dir.Set (dir.x, Random.value, dir.z);
		}
		while (dir.z == 0) {
			dir.Set (dir.x, dir.y, Random.value);
		}
		StartCoroutine (waitForMovement ());
	}

	IEnumerator waitForMovement() {
		yield return new WaitForSeconds (2.0f);
		scanning = true;
	}

	IEnumerator waitAndRestart() {
		yield return new WaitForSeconds (6.0f);
		restart ();
		/*returning = true;
		foreach (GameObject fish in GameObject.FindGameObjectsWithTag("fishParent")) {
			GameObject fishObj = fish.transform.FindChild ("fishModel").gameObject;
			Vector3 target = Random.insideUnitSphere * Random.Range (4, 8);
			Transform look = fish.transform.FindChild ("LookAt");
			look.position = this.transform.position + target;
			fishObj.transform.LookAt (look);
			while(!Vector3.Equals (fishObj.transform.position, look.transform.position)) {
				fishObj.transform.position = Vector3.MoveTowards (fishObj.transform.position, look.position, headMoveSpeed / 6f);
			}
			returning = false;
			flee = false;
		}*/
	}

	// Update is called once per frame
	void FixedUpdate () {
		bool returned = true;
		foreach (Transform child in transform) {
			GameObject fishObj = child.FindChild ("fishModel").gameObject;
			if (rotatingOrSchooling) { //rotating
				child.RotateAround (child.position, Vector3.up, Time.deltaTime * fishRotSpeed);
				Transform look = child.FindChild ("LookAt");
				look.localPosition = fishObj.transform.localPosition + dir * headMoveSpeed;
				fishObj.transform.rotation = Quaternion.Slerp (fishObj.transform.rotation, Quaternion.LookRotation (look.position - fishObj.transform.position), fishRotSpeed * Time.deltaTime);
				//fishObj.transform.LookAt (look);
			} else if (returning) {
				Transform look = child.FindChild ("LookAt");
				//fishObj.transform.LookAt (look);
				fishObj.transform.rotation = Quaternion.Slerp (fishObj.transform.rotation, Quaternion.LookRotation (look.position - fishObj.transform.position), fishRotSpeed * Time.deltaTime);
				fishObj.transform.position = Vector3.MoveTowards (fishObj.transform.position, look.position, headMoveSpeed / (shark ? 4f : 5f));
				if (Vector3.Distance (fishObj.transform.position, this.transform.position) < 3)
					returning = false;
			}
			else { //schooling
				Transform look = child.FindChild ("LookAt");
				look.localPosition = fishObj.transform.localPosition + dir * headMoveSpeed;
				fishObj.transform.rotation = Quaternion.Slerp (fishObj.transform.rotation, Quaternion.LookRotation (look.position - fishObj.transform.position), fishRotSpeed * Time.deltaTime);
				//fishObj.transform.LookAt (look);
			}

			if (flee) {
				Transform look = child.FindChild ("LookAt");
				look.localPosition = new Vector3(fishObj.transform.localPosition.x * 2f, fishObj.transform.localPosition.y, fishObj.transform.localPosition.z * 2f);
				fishObj.transform.rotation = Quaternion.Slerp (fishObj.transform.rotation, Quaternion.LookRotation (look.position - fishObj.transform.position), fishRotSpeed * Time.deltaTime);
				//fishObj.transform.LookAt (look);
				fishObj.transform.position = Vector3.MoveTowards (fishObj.transform.position, look.position, headMoveSpeed / (shark ? 6f : 8f));
			}
		}

		if (!rotatingOrSchooling && !flee) { //schooling
			this.transform.position += dir * Time.deltaTime * headMoveSpeed * (shark ? 1.5f : 1f);
		}
			

		/*if (scanning && Mathf.Abs (Vector3.Distance (this.transform.position, player.transform.position)) > minDistance) {
			restart ();
		}*/

		if ((dir.y > 0 && this.transform.position.y > -15) || (dir.y < 0 && this.transform.position.y < -35)) {
			dir = new Vector3 (dir.x, -dir.y, dir.z);
		}

	}

	void restart() {
		int count = this.transform.childCount;
		for (int i = 0; i < count; i++) {
			GameObject fish = this.transform.GetChild (i).gameObject;
			Destroy (fish);
		}
		//Start ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.name == "whale") {
			flee = true;
			//StartCoroutine (waitAndRestart ());
		}

	}
	void OnTriggerExit(Collider other) {
		if (other.name == "whale") {
			flee = false;
			foreach (Transform child in transform) {
				GameObject fishObj = child.FindChild ("fishModel").gameObject;
				Transform look = child.FindChild ("LookAt");
				look.localPosition = Random.insideUnitCircle * Random.Range (4, 8);
				fishObj.transform.rotation = Quaternion.Slerp (fishObj.transform.rotation, Quaternion.LookRotation (look.position - fishObj.transform.position), fishRotSpeed * Time.deltaTime);
				//fishObj.transform.LookAt (look);
				returning = true;
			}
			//StartCoroutine (waitAndRestart ());
		}

	}
}
