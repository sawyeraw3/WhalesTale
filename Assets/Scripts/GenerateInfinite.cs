using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateInfinite : MonoBehaviour {

	public GameObject plane;
	public GameObject player;
	public int planeSize = 10;
	public int halfTilesX = 10;
	public int halfTilesZ = 10;
	public int waterDepth = 100;
	float maxSmallSize = 3;
	[Range(0,1)]
	public float smallObjectsChance = 0.3f;
	public GameObject[] smallPrefabs;
	public GameObject[] bigPrefabs;
	public GameObject testObj;

	int seed;

	Vector3 startPos;

	Hashtable tiles = new Hashtable();
	int heightScale;

	public class Tile {

		public GameObject theTile;
		public GameObject theObj;

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

		public Tile(GameObject t, GameObject o, float ct) {
			theTile = t;
			theObj = o;
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
				Vector3 pos = new Vector3((x * planeSize+startPos.x), -1*waterDepth, (z * planeSize+startPos.z));
				GameObject t = (GameObject)Instantiate(plane, pos, Quaternion.identity);
				GenerateTerrain gt = t.GetComponent<GenerateTerrain> ();
				gt.perlinMesh (seed);

				Vector3 objPos = pos + gt.objV;
				GameObject o = (GameObject)Instantiate(testObj, objPos, Quaternion.identity);

				SpawnSmallObjects (t);

				string tilename = "Tile_" + ((int)(pos.x)).ToString() + "_" + ((int)(pos.z)).ToString();
				t.name = tilename;
				o.name = tilename;

				Tile tile = new Tile(t, o, updateTime);
				tiles.Add(tilename, tile);

				t.transform.parent = gameObject.transform;
				o.transform.parent = gameObject.transform;
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
					Vector3 pos = new Vector3((x * planeSize + playerX), -1*waterDepth, (z * planeSize + playerZ));
					string tilename = "Tile_" + ((int)(pos.x)).ToString() + "_" + ((int)(pos.z)).ToString();

					if (!tiles.ContainsKey(tilename)) {
						GameObject t = (GameObject)Instantiate(plane, pos, Quaternion.identity);
						GenerateTerrain gt = t.GetComponent<GenerateTerrain> ();
						gt.perlinMesh (seed);

						Vector3 objPos = pos + gt.objV;
						GameObject o = (GameObject)Instantiate(testObj, objPos, Quaternion.identity);

						SpawnSmallObjects (t);

						t.name = tilename;
						o.name = tilename;
						Tile tile = new Tile(t, o, updateTime);
						tiles.Add(tilename, tile);
						t.transform.parent = gameObject.transform;
						o.transform.parent = gameObject.transform;
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
					Destroy(tls.theObj);
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
			Vector3 spawnPos = tile.transform.position;
			spawnPos.y = 0;
			GameObject temp = Instantiate (smallPrefabs [randObj], spawnPos, new Quaternion(0,0,0,0), tile.transform);
			temp.transform.localScale *= randSize;
		}

	}

	void SpawnBigObjects(GameObject tile)
	{
		Mesh mesh = this.GetComponent<MeshFilter>().mesh;
		Vector3[] vertices = mesh.vertices;
	}
}