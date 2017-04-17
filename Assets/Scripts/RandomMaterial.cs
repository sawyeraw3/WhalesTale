using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMaterial : MonoBehaviour {

	public Material[] materials;

	// Use this for initialization
	void Start () {
		Random.seed = System.DateTime.Now.Millisecond;

		this.GetComponent<Renderer>().material = materials[Random.Range(0, materials.Length)];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
