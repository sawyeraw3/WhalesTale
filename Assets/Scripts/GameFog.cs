using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFog : MonoBehaviour {


	public float waterLevel;
	private Color underwaterColor;
	private Color abovewaterColor;
	public GameObject player;
	public Material underwaterSkybox;
	public Material abovewaterSkybox;

	// Use this for initialization
	void Start () {
		abovewaterColor = new Color (1f, 1f, 1f, 1f);
		underwaterColor = new Color (0f, (15f / 255f), (163f / 255f), 1f);
	}
	
	// Update is called once per frame
	void Update () {

		if (player.transform.position.y < waterLevel) {

			RenderSettings.fogColor = underwaterColor;
			RenderSettings.fogDensity = 0.01f;
			RenderSettings.skybox = underwaterSkybox;
		
		} else {
			
			RenderSettings.fogColor = abovewaterColor;
			RenderSettings.fogDensity = 0.01f;
			RenderSettings.skybox = abovewaterSkybox;
		}
	}
}
