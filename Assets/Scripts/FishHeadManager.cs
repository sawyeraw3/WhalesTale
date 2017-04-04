using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHeadManager : MonoBehaviour {

	public GameObject fishHeadPrefab;
	public GameObject player;
	private List<GameObject> fishHeads;
	public float minDistance;
	public int minHeads;

	// Use this for initialization
	void Start () {
		fishHeads = new List<GameObject> ();
		for(int i = 0; i < minHeads; i++) {
			GameObject head = Instantiate (fishHeadPrefab) as GameObject;
			head.transform.position = player.transform.position;
			fishHeads.Add (head);
		}
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < fishHeads.Count; i++) {
			GameObject head = fishHeads [i];
			if (!head) {
				fishHeads.Remove (head);
				continue;
			}

			// if its far enough from the whale
			float distance = Mathf.Abs (Vector3.Distance (player.transform.position, head.transform.position));
			if (distance > minDistance) {
				fishHeads.Remove (head);
				Destroy (head);

				GameObject newHead = Instantiate (fishHeadPrefab) as GameObject;
				newHead.transform.position = player.transform.position + Random.insideUnitSphere * minDistance / 2;
				fishHeads.Add (newHead);
			}
		}
	}
}
