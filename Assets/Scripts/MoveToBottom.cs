using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToBottom : MonoBehaviour {

	public bool isRock = false;
	public bool normalize = false;

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
		if(isRock)
			transform.rotation = Random.rotation;
		RaycastHit hit;
		Ray ray = new Ray (transform.position, Vector3.down);
		if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
			if (hit.collider.tag == "floor") {
				this.transform.position = hit.point;
				if(normalize)
					this.transform.rotation = Quaternion.FromToRotation (transform.up, hit.normal) * transform.rotation;
			}
		}
		this.GetComponentInChildren<Renderer> ().enabled = true;
	}
}
