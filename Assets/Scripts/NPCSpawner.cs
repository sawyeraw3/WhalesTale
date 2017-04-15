using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour {

	public GameObject player;
	public GameObject whaleNPC;
	public GameObject fishHead;
	public float whaleSpawnDelay = 25f;
	public float fishSpawnDelay = 10f;
	private float timer1 = 0;
	private float timer2 = 0;

	// Use this for initialization
	void Start () {
		GameObject p = GameObject.Find("whale");
		player = p;
	}
	
	// Update is called once per frame
	void Update () {
		timer1 += Time.deltaTime;
		timer2 += Time.deltaTime;

		if (timer1 >= whaleSpawnDelay) {
			spawnWhale ();
			timer1 = 0;
		}
		if (timer2 >= fishSpawnDelay) {
			spawnFish ();
			timer2 = 0;
		}
	}

	void spawnWhale(){
		if (Random.value <= 0.5f) {
			int[] mult = { -1, 1 };
			Random.seed = System.DateTime.Now.Millisecond;
			GameObject whale = Instantiate (whaleNPC) as GameObject;
			whale.transform.position = new Vector3 (player.transform.position.x + Random.Range (250, 351) * mult [Random.Range (0, 2)], 
				Random.Range (-35f, -10f), 
				player.transform.position.z + Random.Range (250, 351) * mult [Random.Range (0, 2)]);
		}
	}

	void spawnFish(){
		int[] mult = { -1, 1 };
		Random.seed = System.DateTime.Now.Millisecond;
		GameObject whale = Instantiate (fishHead) as GameObject;
		whale.transform.position = new Vector3 (player.transform.position.x + Random.Range (150, 301) * mult [Random.Range (0, 2)], 
			Random.Range (-35f, -10f), 
			player.transform.position.z + Random.Range (150, 301) * mult [Random.Range (0, 2)]);
	}
}
