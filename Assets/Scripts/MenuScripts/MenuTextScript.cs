using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuTextScript : MonoBehaviour {

	private int clickCount;
	private Animator textAnim;
	private Animator imageAnim;
	private Text loadText;
	private bool loadScene = false;
	// Use this for initialization
	void Start () {
		clickCount = 0;
		textAnim = GameObject.Find ("Text").GetComponent<Animator> ();
		imageAnim = GameObject.Find ("Image").GetComponent<Animator> ();
		loadText = GameObject.Find ("LoadText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) || GvrController.ClickButtonUp)
			clickCount++;

		if (clickCount == 1) {
			textAnim.SetTrigger ("TextFadeOut");
			imageAnim.SetTrigger ("ImageFadeIn");
		}
		if (clickCount >= 2 && loadScene == false) {
			imageAnim.SetTrigger ("ImageFadeOut");
			if (imageAnim.GetCurrentAnimatorStateInfo (0).normalizedTime > 1 && imageAnim.GetCurrentAnimatorStateInfo (0).IsName("FadeOut") && !imageAnim.IsInTransition (0)) {
				loadScene = true;

				// ...change the instruction text to read "Loading..."
				loadText.text = "Loading...";

				// ...and start a coroutine that will load the desired scene.
				StartCoroutine(LoadNewScene());
			}
		}

		if (loadScene == true) {

			// ...then pulse the transparency of the loading text to let the player know that the computer is still working.
			loadText.color = new Color(loadText.color.r, loadText.color.g, loadText.color.b, Mathf.PingPong(Time.time, 1));

		}

	}

	IEnumerator LoadNewScene() {

		// This line waits for 3 seconds before executing the next line in the coroutine.
		// This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
		yield return new WaitForSeconds(3);

		// Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
		AsyncOperation async = SceneManager.LoadSceneAsync(1); //while the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
		while (!async.isDone) {
			yield return null;
		}

	}
}
