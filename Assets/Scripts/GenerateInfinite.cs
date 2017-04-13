using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateInfinite : MonoBehaviour {

	public GameObject plane;
	public GameObject player;
	public int planeSize = 10;
	public int halfTilesX = 10;
	public int halfTilesZ = 10;
	public int waterDepth = -20;
	float maxSmallSize = 3;
	[Range(0,1)]
	public float smallObjectsChance = 0.3f;
	public GameObject[] smallPrefabs;
	public GameObject[] bigPrefabs;

	int seed;

	Vector3 startPos;

	Hashtable tiles = new Hashtable();
	int heightScale;

	public class Tile {

		public GameObject theTile;
		public float creationTime;

		public Tile() {
		}

		public Tile(GameObject t) {
			theTile = t;
		}

		public Tile(GameObject t, float ct) {
			theTile = t;
			creationTime = ct;
		}
	}

	void Start() {
		seed = 100;

		heightScale = plane.GetComponent<GenerateTerrain> ().heightScale;

		this.gameObject.transform.position = Vector3.zero;
		startPos = Vector3.zero;

		float updateTime = Time.realtimeSinceStartup;

		for (int x = -halfTilesX; x < halfTilesX; x++) {
			for (int z = -halfTilesZ; z < halfTilesZ; z++) {
				Vector3 pos = new Vector3((x * planeSize+startPos.x), waterDepth, (z * planeSize+startPos.z));

				GameObject t = (GameObject) Instantiate(plane, pos, Quaternion.identity);
				t.GetComponent<GenerateTerrain> ().perlinMesh (seed);

				SpawnSmallObjects (t);

				string tilename = "Tile_" + ((int)(pos.x)).ToString() + "_" + ((int)(pos.z)).ToString();
				t.name = tilename;

				Tile tile = new Tile(t, updateTime);
				tiles.Add(tilename, tile);
				t.transform.parent = gameObject.transform;
			}
		}
	}

	void Update() {

		//determine how far the player has moved since last terrain update
		int xMove = (int)(player.transform.position.x - startPos.x);
		int zMove = (int)(player.transform.position.z - startPos.z);

		if (Mathf.Abs(xMove) > +planeSize || Mathf.Abs(zMove) >= planeSize) {
			float updateTime = Time.realtimeSinceStartup;

			//force integer position and round to nearest tilesize
			int playerX = (int)(Mathf.Floor(player.transform.position.x / planeSize) * planeSize);
			int playerZ = (int)(Mathf.Floor(player.transform.position.z / planeSize) * planeSize);

			for (int x = -halfTilesX; x < halfTilesX; x++) {
				for (int z = -halfTilesZ; z < halfTilesZ; z++) {
					Vector3 pos = new Vector3((x * planeSize + playerX), waterDepth, (z * planeSize + playerZ));
					string tilename = "Tile_" + ((int)(pos.x)).ToString() + "_" + ((int)(pos.z)).ToString();

					if (!tiles.ContainsKey(tilename)) {
						GameObject t = (GameObject)Instantiate(plane, pos, Quaternion.identity);

						t.GetComponent<GenerateTerrain> ().perlinMesh (seed);

						SpawnSmallObjects (t);

						t.name = tilename;
						Tile tile = new Tile(t, updateTime);
						tiles.Add(tilename, tile);
						t.transform.parent = gameObject.transform;
					}
					else {
						(tiles[tilename] as Tile).creationTime = updateTime;
					}
				}
			}

			//destroy all tiles not just created or with time updated
			//and put new tiles and tiles to be kept in a new hastable
			Hashtable newTerrain = new Hashtable();
			foreach (Tile tls in tiles.Values) {
				if (tls.creationTime != updateTime) {
					Destroy(tls.theTile);
				}
				else {
					newTerrain.Add(tls.theTile.name, tls);
				}
			}

			//copy new hastable contents to the working hashtable
			tiles = newTerrain;
			startPos = player.transform.position;
		}
	}

	void SpawnSmallObjects(GameObject tile)
	{
		Mesh mesh = tile.GetComponent<MeshFilter>().mesh;
		Vector3[] vertices = mesh.vertices;
		Vector3 minV = vertices [0];
		Vector3 maxV = vertices [vertices.Length - 1];
		
		float objectHere = Random.value;

		if(objectHere < smallObjectsChance) {
			int findSpot = Random.Range (0, vertices.Length);
			Vector3 randSpot = vertices [findSpot];
			int randObj = Random.Range (0, smallPrefabs.Length);
			float randSize = Random.Range (0.25f, maxSmallSize);
			int chooseVertice = Random.Range (0, vertices.Length);
			Vector3 meshPos = vertices [chooseVertice];
			Vector3 spawnPos = tile.transform.position;

			GameObject temp = Instantiate (smallPrefabs [randObj], spawnPos, new Quaternion(0,0,0,0), tile.transform);
			temp.transform.localPosition = meshPos;

			temp.transform.localScale *= randSize;
		}

	}

	void SpawnBigObjects(GameObject tile)
	{
		Mesh mesh = this.GetComponent<MeshFilter>().mesh;
		Vector3[] vertices = mesh.vertices;
	}
}