using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePrefab : MonoBehaviour {

	public bool isRock = false;
	public bool normalize = false;
	public bool moveToBottom = false;
	private bool moved = false;

	// Use this for initialization
	void Start () {
		this.GetComponentInChildren<Renderer> ().enabled = false;
		rotate ();

	}

	// Update is called once per frame
	void FixedUpdate () {
		if (moveToBottom && !moved) {
			this.GetComponentInChildren<Renderer> ().enabled = false;
			Vector3 startPos = transform.position;
			startPos.y += 50;
			LayerMask myLayerMask = 1 << 9;
			RaycastHit hit;
			Ray ray = new Ray (startPos, Vector3.down);
			if (Physics.Raycast (ray, out hit, 150, myLayerMask)) {
				if (hit.collider.tag == "floor") {
					this.transform.position = hit.point;
					moved = true;
					transform.RotateAround(transform.position, transform.up, Random.Range (0, 360));
					this.GetComponentInChildren<Renderer> ().enabled = true;
				}
			}
		}
	}


	void rotate(){
		Random.seed = System.DateTime.Now.Millisecond;
		if (isRock) {
			transform.localRotation = Random.rotation;
			this.GetComponentInChildren<Renderer> ().enabled = true;
		}
		if (normalize) {
			Vector3 startPos = transform.position;
			startPos.y += 50;
			LayerMask myLayerMask = 1 << 9;
			RaycastHit hit;
			Ray ray = new Ray (startPos, Vector3.down);
			if (Physics.Raycast (ray, out hit, 150, myLayerMask)) {
				if (hit.collider.tag == "floor") {
					this.transform.rotation = Quaternion.FromToRotation (transform.up, hit.normal) * transform.rotation;
				}
			}
			transform.RotateAround(transform.position, transform.up, Random.Range (0, 360));
			this.GetComponentInChildren<Renderer> ().enabled = true;
		}
	}
}
