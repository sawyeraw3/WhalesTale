using UnityEngine;
using System.Collections;

public class GenerateInfinite : MonoBehaviour {

	public GameObject plane;
	public GameObject player;
	public int planeSize = 10;
	public int halfTilesX = 10;
	public int halfTilesZ = 10;
	public int waterDepth = -20;
	public GameObject[] floorObjects;
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


}