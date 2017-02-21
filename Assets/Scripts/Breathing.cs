using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Breathing : MonoBehaviour {

	public float breathLength; // 60 is recommended
	private float currentBreath;
	private float bubbleFrequency;
	private float minBubbleFrequency = 1;
	public float waterHeight;
	public GameObject bubblePrefab;
	[Header("A float on the interval (0, 1]")]
	// Closer to 0 is more bubbles
	// Closer to 1 is less bubbles
	public float bubbleFactor; // 0.3 is recommended

	// Use this for initialization
	void Start () {
		currentBreath = breathLength;
	}
	
	// Update is called once per frame
	void Update () {
		// For testing
		if (Input.GetKey("up")) {
			gameObject.transform.position += (Vector3.up / 10);
		}
		else if (Input.GetKey("down")) {
			gameObject.transform.position += (Vector3.down / 10);
		}

		// underwater
		if(this.gameObject.transform.position.y < waterHeight) {
			if (currentBreath <= 0) {
				// out of breath
			    Debug.Log ("Dead");
				currentBreath = breathLength;
				bubbleFrequency = bubbleFactor * currentBreath;
			} 
			else {
				currentBreath -= Time.deltaTime;
				if (bubbleFrequency <= 0) {
					//spawn a bubble and reset bubbleFrequency
					GameObject bubble = Instantiate(bubblePrefab) as GameObject;
					bubble.transform.position = this.gameObject.transform.position + new Vector3 (-1, 1, 0);
					bubbleFrequency = bubbleFactor * currentBreath;
					if (bubbleFrequency < minBubbleFrequency) {
						bubbleFrequency = minBubbleFrequency;
					}
				} else {
					bubbleFrequency -= Time.deltaTime;
				}
			}
		}
		// above water - reset breath
		else {
			currentBreath = breathLength;
			bubbleFrequency = bubbleFactor * currentBreath;
		}
	}
}
