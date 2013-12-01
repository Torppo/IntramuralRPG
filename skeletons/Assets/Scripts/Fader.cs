using UnityEngine;
using System.Collections;

/*
 * Fades screen to black
 */
public class Fader : MonoBehaviour {
	public float fadeSpeed = 0.5f;	//Speed of the fade
	
	public bool fadeToBlack = false;	//Are we fading?

	// Use this for initialization
	void Awake () {
		//Scale the fader to the entire screen
		guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
	}
	
	// Update is called once per frame
	void Update () {
		if (fadeToBlack)
			guiTexture.color = Color.Lerp(guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);
	}
}
