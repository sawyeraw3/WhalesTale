using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRockToBottom : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponentInChildren<Renderer> ().enabled = false;
		StartCoroutine (cast());

	}
	
	// Update is called once per frame
	void Update () {
		
	}


	IEnumerator cast(){
		yield return new WaitForSeconds (.01f);
		Random.seed = System.DateTime.Now.Millisecond;
		transform.rotation = Random.rotation;
		RaycastHit hit;
		Ray ray = new Ray (transform.position, Vector3.down);
		if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
			if (hit.collider.tag == "floor") {
				this.transform.position = hit.point;

			}
		}
		this.GetComponentInChildren<Renderer> ().enabled = true;
	}
}
