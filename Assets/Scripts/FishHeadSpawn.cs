using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHeadSpawn : MonoBehaviour {

	public Vector3 dir;
	private float rNum;
	private Vector3 spread;
	public GameObject fishPrefab;
	// Use this for initialization
	void Start () {
		//dir = Vector3.forward;
		//this.gameObject.transform.position = dir;

		rNum = Random.Range (7f, 15f);
		for (int i = 0; i < rNum; i++) {
			spread = Random.insideUnitSphere * 15;
			GameObject fish = Instantiate(fishPrefab) as GameObject;
			fish.transform.parent = this.transform;
			fish.transform.position = fish.transform.parent.position + spread;
		}
	}

	// Update is called once per frame
	void Update () {
		//dir = Vector3.forward;
		//transform.LookAt (dir);


	}
}
