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

	// Use this for initialization
	void Start () {
		this.transform.rotation = Random.rotation;
		this.transform.position = player.transform.position + Random.insideUnitSphere * minDistance / 2;
		rNum = Random.Range (7f, 15f);
		int fishIndex = Random.Range (0, fishPrefabs.Length);

		int pattern = Random.Range (0, 2);
		if(pattern == 0) {
			rotatingOrSchooling = false; //rotating
		}
		else {
			rotatingOrSchooling = false; //schooling
		}

		for (int i = 0; i < rNum; i++) {
			spread = Random.insideUnitSphere * 15;
			GameObject fish = Instantiate(fishPrefabs[fishIndex]) as GameObject;
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
	}

	// Update is called once per frame
	void Update () {
		foreach (GameObject fish in GameObject.FindGameObjectsWithTag("fishParent")) {
			GameObject fishObj = fish.transform.FindChild ("fishModel").gameObject;
			if (rotatingOrSchooling) { //rotating
				fish.transform.RotateAround (fish.transform.position, Vector3.up, Time.deltaTime * fishRotSpeed);
				Transform look = fish.transform.FindChild ("LookAt");
				fishObj.transform.LookAt (look);
			} else { //schooling
				fishObj.transform.rotation = Quaternion.LookRotation (dir);
			}
		}

		if (!rotatingOrSchooling) { //schooling
			this.transform.position = Vector3.MoveTowards (this.transform.position, this.transform.position + dir, headMoveSpeed);
		}

		if (Mathf.Abs (Vector3.Distance (this.transform.position, player.transform.position)) > minDistance) {
			restart ();
		}

	}

	void restart() {
		int count = this.transform.childCount;
		for (int i = 0; i < count; i++) {
			GameObject fish = this.transform.GetChild (i).gameObject;
			Destroy (fish);
		}
		Start ();
	}
}
