using UnityEngine;
using System.Collections;

public class GenerateTerrain : MonoBehaviour {

	public int heightScale = 5;
	public float detailScale = 5.0f;
	public Material[] materials;
	float minY = 0;
	float maxY = 0;

	// Use this for initialization
	void Start() {
		Mesh mesh = this.GetComponent<MeshFilter>().mesh;
		Vector3[] vertices = mesh.vertices;
		for (int v = 0; v < vertices.Length; v++) {
			vertices[v].y = Mathf.PerlinNoise((vertices[v].x + this.transform.position.x)/detailScale,
				(vertices[v].z + this.transform.position.z)/detailScale)*heightScale;
			if (vertices [v].y > maxY) {
				maxY = vertices [v].y;
			} else if (vertices [v].y < minY) {
				minY = vertices [v].y;
			}
		}

		if (materials.Length > 0) {
			float increment = heightScale / materials.Length;
			for (float y = increment; y <= heightScale; y += increment) {
				if (maxY <= y) {
					Renderer rend = gameObject.GetComponent<Renderer> ();
					rend.material.SetColor ("_Color", materials [(int)((y / increment) - 1)].color);
					break;
				}
			}
		}


		mesh.vertices = vertices;
		mesh.RecalculateBounds();
		mesh.RecalculateNormals();
		this.gameObject.AddComponent<MeshCollider>();

	}
}