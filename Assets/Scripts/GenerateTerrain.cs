using UnityEngine;
using System.Collections;

public class GenerateTerrain : MonoBehaviour {

	public int heightScale = 5;
	public float detailScale = 5.0f;
	public float medY;
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

		medY = maxY - minY;

		mesh.vertices = vertices;
		mesh.RecalculateBounds();
		mesh.RecalculateNormals();
		this.gameObject.AddComponent<MeshCollider>();
	}
}