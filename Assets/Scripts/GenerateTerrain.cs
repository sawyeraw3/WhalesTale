using UnityEngine;
using System.Collections;

public class GenerateTerrain : MonoBehaviour {

	public int heightScale = 5;
	public float detailScale = 5.0f;
	//public int seed;
	public Material[] materials;
	public Vector3 minV = Vector3.zero;
	public Vector3 maxV = Vector3.zero;
	public Vector3 objV = Vector3.zero;

	// Use this for initialization
	void Start() {
		
	}

	public void perlinMesh(int seed) {
		Mesh mesh = this.GetComponent<MeshFilter>().mesh;
		Vector3[] vertices = mesh.vertices;
		int ranV = Random.Range (0, vertices.Length - 1);
		for (int v = 0; v < vertices.Length; v++) {
			vertices[v].y = Mathf.PerlinNoise( ((vertices[v].x + this.transform.position.x) / detailScale) + seed,
				((vertices[v].z + this.transform.position.z)/detailScale) + seed ) * heightScale;
			if (vertices [v].y > maxV.y) {
				maxV = vertices [v];
			}
			if (vertices [v].y < minV.y) {
				minV = vertices [v];
			}
			if (v == ranV) {
				objV = vertices [v];
			}
		}

		if (materials.Length > 0) {
			float increment = heightScale / materials.Length;
			for (float y = increment; y <= heightScale; y += increment) {
				if (/*(maxY / 4) * 3*/ maxV.y <= y) {
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