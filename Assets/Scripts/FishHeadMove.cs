using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHeadMove : MonoBehaviour {

	public Vector3 dir;
	private float rNum;
	private Vector3 spread;
	public GameObject fishPrefab;
	private Vector3 buffer;
	// Use this for initialization
	void Start () {
		//dir = Vector3.forward;
		//this.gameObject.transform.position = dir;
		buffer = Random.insideUnitSphere * 10f;
		transform.LookAt (buffer);
		rNum = Random.Range (5f, 10f);
		for (int i = 0; i < rNum; i++) {
			spread = Random.insideUnitSphere * 15;
			GameObject fish = Instantiate(fishPrefab) as GameObject;
			fish.transform.parent = gameObject.transform;
			fish.transform.position = fish.transform.parent.position + spread;
		}




	}

	// Update is called once per frame
	void Update () {
		//transform.Translate (gameObject.transform.forward * Time.deltaTime * 20);
		transform.Translate(Vector3.forward * Time.deltaTime * 20);

	}
}
