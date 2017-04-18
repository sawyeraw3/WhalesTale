using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAboveWater : MonoBehaviour {


	Rigidbody rb;
	private float speed;
	private float mult;
	public AudioClip[] splash;
	AudioSource audSource;
	private int index;
	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody>();
		speed = this.GetComponent<WhaleControl> ().moveSpeed;
		audSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (speed == 10f)
			mult = 15f;
		if (speed == 20f)
			mult = 10f;
		
		speed = this.GetComponent<WhaleControl> ().moveSpeed;

		if (this.transform.position.y > 0) {
			if (rb.useGravity != true) {
				rb.useGravity = true;
				rb.AddForce (transform.up * speed * mult);
				rb.AddForce (transform.forward * speed * mult);

				Random.seed = System.DateTime.Now.Millisecond;
				index = Random.Range(0, splash.Length);
				audSource.PlayOneShot(splash[index], .4f);
			}
			
		}
		if (this.transform.position.y + 3 < 0) {
			if (rb.useGravity != false){
				rb.useGravity = false;
				Random.seed = System.DateTime.Now.Millisecond;
				index = Random.Range(0, splash.Length);
				audSource.PlayOneShot(splash[index], .4f);
			}
		}

		if (this.transform.position.y < 0 && rb.useGravity == false) {
			rb.velocity = Vector3.Lerp (rb.velocity, Vector3.zero, 2f * Time.deltaTime);
		}
	}
}
