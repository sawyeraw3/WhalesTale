using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFog : MonoBehaviour {


	public float waterLevel;
	private FogMode mode;
	private Color abovewaterColor;
	public GameObject player;
	public Color underwaterColor;
	private bool underwater = false;
	//public Material underwaterSkybox;
	//public Material abovewaterSkybox;

	// Use this for initialization
	void Start () {
		//abovewaterColor = new Color (1f, 1f, 1f, 1f);
		underwaterColor = new Color ((40f / 255f), 0f, (255f / 255f));
	}
	
	// Update is called once per frame
	void Update () {

		if (player.transform.position.y < waterLevel && !underwater) {
			underwater = true;
			RenderSettings.fog = true;
			RenderSettings.fogMode = FogMode.ExponentialSquared;
			RenderSettings.fogDensity = 0.05f;
			RenderSettings.fogColor = underwaterColor;
			//RenderSettings.skybox = underwaterSkybox;
		
		}/* else {
			
			RenderSettings.fogColor = abovewaterColor;
			RenderSettings.fogDensity = 0.01f;
			//RenderSettings.skybox = abovewaterSkybox;
		}*/
	}
}
