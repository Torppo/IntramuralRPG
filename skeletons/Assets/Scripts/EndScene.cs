using UnityEngine;
using System.Collections;

/*
 * Restarts scene when the player reaches the end of the level
 */
public class EndScene : MonoBehaviour {
	
	public GUIText wintext;	//The text displayed when the player reaches the end
	public float restartCountdown = 3f;	//Countdown to end of level
	private bool endTriggered = false;	//Has the player reached the end?

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == Tags.player){
			//show win text
			wintext.gameObject.SetActive(true);
			//Fade to black
			GameObject.FindGameObjectWithTag(Tags.screenFader).GetComponent<Fader>().fadeToBlack = true;
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
