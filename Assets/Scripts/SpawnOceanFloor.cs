using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOceanFloor : MonoBehaviour {

	public GameObject[] Rocks;
	private GameObject[] spawnRocks;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 20; i++) {
			Random.seed = System.DateTime.Now.Millisecond;
			GameObject rock = Instantiate (Rocks [Random.Range (0, Rocks.Length - 1)]) as GameObject;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
