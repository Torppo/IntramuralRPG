using UnityEngine;
using System.Collections;

public class EndScene : MonoBehaviour {
	
	public GUIText wintext;
	public float restartCountdown = 3f;
	private bool endTriggered = false;

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player"){
			wintext.gameObject.SetActive(true);
			GameObject.FindGameObjectWithTag("ScreenFader").GetComponent<Fader>().fadeToBlack = true;
			endTriggered = true;
		}
	}
	
	void Update(){
		if (endTriggered){
			
			if (restartCountdown >= 0f){
				restartCountdown -= Time.deltaTime;	
			}
			else {
				Application.LoadLevel(Application.loadedLevel);	
			}
		}	
	}
}
