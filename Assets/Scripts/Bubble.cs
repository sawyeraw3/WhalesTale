using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {

	public float bubbleSpeed;
	public float waterHeight;
	public float maxBubbleTime;
	private float bubbleTime;

	//bubble rotation
	private float rotateSpeed = 20f;
	private float radius = 0.05f;
	private float angle;

	//bubble variation
	private float sMin = .2f, sMax = .7f;

	// Use this for initialization
	void Start () {
		maxBubbleTime = 10;

		/* -- BUBBLE VARIATION -- */
		//random scaling of bubble
		float rScale = Random.Range(sMin,sMax);
		transform.localScale = new Vector3(rScale,rScale,rScale);
		//update bubble speed, rotation radius, and rotation speed based on size
		//ie: big bubbles = slower, little bubbles = faster
		bubbleSpeed = (sMax - (rScale - sMin)) * 25f;
		radius = rScale * .25f;
		if (radius > .1f) radius = .1f;
		rotateSpeed = ((sMax*10f - sMin*10f) - (rScale*10f - sMin*10f)) * 10f; //speed between 1 and 20 - based on size
		if (rotateSpeed < 2f) rotateSpeed = 2f;
	}
	
	// Update is called once per frame
	void Update () {

		angle += rotateSpeed * Time.deltaTime;
		Vector3 offset = new Vector3(Mathf.Sin(angle),0, Mathf.Cos(angle)) * radius;

		bubbleTime += Time.deltaTime;
		gameObject.transform.position += Vector3.up * bubbleSpeed * Time.deltaTime + offset;
		if (gameObject.transform.position.y >= waterHeight || bubbleTime >= maxBubbleTime) {
			Destroy (this.gameObject);
		}
	}
}
