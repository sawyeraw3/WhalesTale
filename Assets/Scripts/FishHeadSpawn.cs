using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHeadSpawn : MonoBehaviour {

	public Vector3 dir;
	private float rNum;
	private Vector3 spread;
	public GameObject fishPrefab;
	public float fishRotSpeed;
	public float headMoveSpeed;

	// Use this for initialization
	void Start () {
		this.transform.rotation = Random.rotation;
		rNum = Random.Range (7f, 15f);
		for (int i = 0; i < rNum; i++) {
			spread = Random.insideUnitSphere * 15;
			GameObject fish = Instantiate(fishPrefab) as GameObject;
			fish.transform.parent = this.transform;
			fish.transform.localPosition = Vector3.zero;

			GameObject fishObj = fish.GetComponentInChildren<MeshRenderer>().gameObject;
			fishObj.transform.localPosition = spread;
		}
		Random.seed = System.DateTime.Now.Millisecond;
		dir = new Vector3 (Random.Range (-1, 1), Random.Range (-1, 1), Random.Range (-1, 1));
		while (dir.x == 0) {
			dir.Set (Random.Range (-1, 1), dir.y, dir.z);
		}
		while (dir.y == 0) {
			dir.Set (dir.x, Random.Range (-1, 1), dir.z);
		}
		while (dir.z == 0) {
			dir.Set (dir.x, dir.y, Random.Range (-1, 1));
		}
	}

	// Update is called once per frame
	void Update () {
		int fishCount = 0;
		foreach (GameObject fish in GameObject.FindGameObjectsWithTag("fishParent")) {
			fish.transform.RotateAround (fish.transform.position, Vector3.up, Time.deltaTime * fishRotSpeed);
			GameObject fishObj = fish.GetComponentInChildren<Transform> ().gameObject;
			fishObj.transform.rotation = Quaternion.LookRotation (dir);

			if (fish.transform.position.y > 0 || fish.transform.position.y < -100) {
				Destroy (fish);
			} else {
				fishCount++;
			}
		}

		this.transform.position = Vector3.MoveTowards (this.transform.position, this.transform.position + dir, headMoveSpeed);

		if (transform.position.y > 0 || transform.position.y < -100) {
			if (fishCount == 0)
				Destroy (this.gameObject);
		}

	}
}
