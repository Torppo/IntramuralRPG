using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour {
	public float fadeSpeed = 0.5f;
	
	public bool fadeToBlack = false;

	// Use this for initialization
	void Awake () {
		guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
	}
	
	// Update is called once per frame
	void Update () {
		if (fadeToBlack)
			guiTexture.color = Color.Lerp(guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);
	}
}
