using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFog : MonoBehaviour {


	public float waterLevel;
	private FogMode mode;
	private Color abovewaterColor;
	public GameObject camera;
	public GameObject player;
	private Color underwaterColor;
	public Material underwaterSkybox;
	public Material abovewaterSkybox;

	// Use this for initialization
	void Start () {
		abovewaterColor = new Color (1f, 1f, 1f, 1f);
		underwaterColor = new Color ((40f / 255f), 0f, (255f / 255f));
	}
	
	// Update is called once per frame
	void Update () {

		if (camera.transform.position.y > waterLevel) {
			//RenderSettings.fog = true;
			//RenderSettings.fogMode = FogMode.ExponentialSquared;
			RenderSettings.fogColor = abovewaterColor;
			RenderSettings.fogDensity = 0.01f;
			RenderSettings.skybox = abovewaterSkybox;
		
		} else {

			RenderSettings.fogDensity = 0.02f;
			RenderSettings.fogColor = underwaterColor;
			RenderSettings.skybox = underwaterSkybox;
		}
	}
}
