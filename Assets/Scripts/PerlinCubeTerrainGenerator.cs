using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinCubeTerrainGenerator : MonoBehaviour {

	public GameObject terrainCube;
	public Material[] materials;
	public float maxY;
	//Can use numElements when data is a "square"
	public int mapHeight;
	public int mapWidth;
	float cubeSideLength;

	Vector2 shift = new Vector2(0,0);
	float zoom = .1f;

	/*
	public float numElements;
	public float slope;
	float prevCubeY = 0f;
	float minX;
	float maxX;
	float minY = 0f;
	float maxY = 5f;
	float minZ;
	float maxZ;
	*/

	// Use this for initialization
	void Start () {
		/*
		minX = -numElements;
		maxX = numElements;

		float cubeScaleY = terrainCube.transform.localScale.y;
		
		minZ = -numElements;
		maxZ = numElements;
		*/

		int xIn = 0;
		int zIn = 0;

		cubeSideLength = terrainCube.transform.localScale.x;


		float[,] heightMap = setHeightArray();

		for (float x = 0; x < mapWidth; x ++) {
			zIn = 0;
			for (float z = 0; z < mapHeight; z ++) {
				float cubeY = heightMap [xIn, zIn];
				Vector3 pos = new Vector3 (x * cubeSideLength, cubeY, z * cubeSideLength);
				GameObject temp = Instantiate (terrainCube, pos, Quaternion.identity);

				Renderer rend = temp.GetComponent<Renderer> ();

				float increment = maxY / materials.Length;
				for (float c = increment; c <= maxY; c += increment) {
					if (cubeY <= c) {
						rend.material.SetColor ("_Color", materials[(int)((c / increment) - 1)].color);
						break;
					}
				}

				//float shadeOfBlue = 255f - (255f / cubeY);
				

				/*if (nextCubeY < 2f) {
					Color c1 = new Color(0f, 0f, 255f);
					mat.color = c1;
					mR.material = mat;
				} else if (nextCubeY < 3f) {
					Color c1 = new Color(255f, 105f, 255f);
					mat.color = c1;
					mR.material = mat;
				} else if (nextCubeY < 3) {
					Color c1 = new Color(30f, 144f, 255f);
					mat.color = c1;
					mR.material = mat;
				} else {
					Color c1 = new Color(0f, 190f, 255f);
					mat.color = c1;
					mR.material = mat;
				}*/

				temp.name = /*rend.material.name + */" tCube_" + x.ToString () + "_" + cubeY.ToString () + "_" + z.ToString ();
				zIn++;
			}
			xIn++;
		}

		/*
		for(int z = minZ; z < maxZ; z+=(int)terrainCube.transform.localScale.z) {
			for (int x = minX; x < maxX; x+=(int)terrainCube.transform.localScale.x) {
				//int ranY = Random.Range (maxY, minY);
				int ranY;
				if (prevCubeY + slope > maxY) {
					ranY = Random.Range (prevCubeY - slope, maxY);
				} else if (prevCubeY - slope < minY) {
					ranY = Random.Range (minY, prevCubeY + slope);
				} else {
					ranY = Random.Range (prevCubeY - slope, prevCubeY + slope);
				}

				prevCubeY = ranY;

				Vector3 pos = new Vector3 (x, ranY, z);
				GameObject temp = Instantiate (terrainCube, pos, Quaternion.identity);


				temp.name = "tCube_" + x.ToString () + "_" + ranY.ToString () + "_" + z.ToString ();
	
			}
		}
		*/
	}

	float[,] setHeightArray() {
		float[,] map = new float[mapWidth,mapHeight];
		for (int x = 0; x < mapWidth; x++) {//+= terrainCube.transform.localScale.x) {
			for (int z = 0; z < mapHeight; z++) {// += terrainCube.transform.localScale.z) {
				Vector2 pos = zoom * (new Vector2(x,z)) + shift;
				float noise = Mathf.PerlinNoise(pos.x, pos.y);
				for (float n = 1; n <= maxY; n++) {
					if (noise < ((1f / maxY) * n)) {
						map [x, z] = (n - 1f);
						break;
					}
				}
				/*
				if (noise < 0.125f) {
					map [x, z] = 0f;
				} else if (noise < 0.25f) {
					map [x, z] = 1.0f * scale;
				} else if (noise < 0.375f) {
					map [x, z] = 2.0f * scale;
				}  else if (noise < 0.5f) {
					map [x, z] = 3.0f * scale;
				} else if (noise < 0.625f) {
					map [x, z] = 4.0f * scale;
				} else if (noise < 0.75f) {
					map [x, z] = 5.0f * scale;
				} else if (noise < 0.875f) {
					map [x, z] = 6.0f * scale;
				} else {
					map [x, z] = 7.0f * scale;
				}*/
			}
		}
		return map;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
