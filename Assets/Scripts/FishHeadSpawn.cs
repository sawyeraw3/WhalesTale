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
		rNum = Random.Range (7f, 15f);
		for (int i = 0; i < rNum; i++) {
			spread = Random.insideUnitSphere * 15;
			GameObject fish = Instantiate(fishPrefab) as GameObject;
			fish.transform.parent = this.transform;
			fish.transform.localPosition = Vector3.zero;

			GameObject fishObj = fish.GetComponentInChildren<MeshRenderer>().gameObject;
			fishObj.transform.localPosition = spread;
		}
	}

	// Update is called once per frame
	void Update () {
		foreach (GameObject fish in GameObject.FindGameObjectsWithTag("fishParent")) {
			fish.transform.RotateAround (fish.transform.position, Vector3.up, Time.deltaTime * fishRotSpeed);
		}

		this.transform.position = Vector3.MoveTowards (this.transform.position, this.transform.position + Vector3.forward, headMoveSpeed);

	}
}
