using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {

	public float bubbleSpeed;
	public float waterHeight;
	public float maxBubbleTime;
	private float bubbleTime;

	//bubble rotation
	private float RotateSpeed = 20f;
	private float Radius = 0.05f;
	private float angle;

	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3(.5f,.5f,.5f);
	
	}
	
	// Update is called once per frame
	void Update () {

		angle += RotateSpeed * Time.deltaTime;
		Vector3 offset = new Vector3(Mathf.Sin(angle),0, Mathf.Cos(angle)) * Radius;

		bubbleTime += Time.deltaTime;
		gameObject.transform.position += Vector3.up * bubbleSpeed * Time.deltaTime + offset;
		if (gameObject.transform.position.y >= waterHeight || bubbleTime >= maxBubbleTime) {
			Destroy (this.gameObject);
		}
	}
}
