using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMaterialGroup : MonoBehaviour {

	public List<Material> materials = new List<Material>();
	private List<Material> backup = new List<Material>();
	private GameObject child;
	// Use this for initialization
	void Start () {
		Random.seed = System.DateTime.Now.Millisecond;

		for (int i = 0; i < this.transform.childCount ; i++){
			child = this.gameObject.transform.GetChild(i).gameObject;

			if (materials.Count == 0){
				materials = new List<Material>(backup);
				backup.RemoveRange(0, backup.Count);
			}
			int index = Random.Range(0, materials.Count - 1);
			child.GetComponent<Renderer>().material = materials[index];
			backup.Add(materials[index]);
			materials.RemoveAt(index);
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
