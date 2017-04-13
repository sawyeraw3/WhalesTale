using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleNPCspawner : MonoBehaviour {

	public GameObject player;
	public GameObject whaleNPC;
	private int[] mult = {-1, 1};


	// Use this for initialization
	void Start () {
		GameObject p = GameObject.Find("whale");
		player = p;
		StartCoroutine (spawn());
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	IEnumerator spawn(){
		while (true) {
			Random.seed = System.DateTime.Now.Millisecond;
			yield return new WaitForSeconds (Random.Range(100f,300f));
			GameObject whale = Instantiate(whaleNPC) as GameObject;
			whale.transform.position = new Vector3(player.transform.position.x + 120f * mult[Random.Range(0, mult.Length)], 
														Random.Range(-35f, -10f), 
															player.transform.position.z + 120f * mult[Random.Range(0, mult.Length)]);
		



		}

	}
}
