using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayFPS : MonoBehaviour
{
	Text fpsText;
	float deltaTime = 0.0f;

	void Start()
	{
		fpsText = gameObject.GetComponentInChildren<Text> ();
	}

	void Update()
	{
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

		//int w = Screen.width, h = Screen.height;
		//GUIStyle style = new GUIStyle();
		//Rect rect = new Rect(0, 0, w, h * 2 / 100);
		//style.alignment = TextAnchor.UpperLeft;
		//style.fontSize = h * 2 / 100;
		//style.normal.textColor = new Color (255.0f, 255.0f, 0.0f, 0.5f);
		//float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		string text = string.Format("{0:0.0} fps", fps);//string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
		//GUI.Label(rect, text, style);
		fpsText.text = text;
	}
}