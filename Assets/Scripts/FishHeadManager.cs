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
	}
	
	// Update is called once per frame
	void Update () {
		int count = 0;
		for(int i = 0; i < fishHeads.Count; i++) {
			GameObject head = fishHeads [i];
			if (!head) {
				fishHeads.Remove (head);
				i--;
				continue;
			}

			// if its close enough to the whale
			if (Mathf.Abs (Vector3.Distance (player.transform.position, head.transform.position)) < minDistance) {
				count++;
			} else {
				fishHeads.Remove (head);
				i--;
				Destroy (head);
			}
		}

		if (count < minHeads) {
			Debug.Log ("making new");
			GameObject head = Instantiate (fishHeadPrefab) as GameObject;
			head.transform.position = player.transform.position;
			fishHeads.Add (head);
		}
	}
}
