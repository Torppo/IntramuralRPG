using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	public bool paused = false;
	
	private MouseOrbitImproved moi;
		
	void Awake(){
		moi = GetComponent<MouseOrbitImproved>();
	}
	
	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)){
			if (!paused){
				Time.timeScale = 0.0f;
				paused = true;
				moi.enabled = false;
			}
			else {
				Time.timeScale = 1.0f;
				paused = false;
				moi.enabled = true;
			}
		}
	}
	
	void OnGUI() {
		if (!paused) return;
		
		GUI.Box(new Rect(100, 50, Screen.width - 200, Screen.height - 100), "Pause Menu");
		
		if (GUI.Button(new Rect(120,Screen.height/6,Screen.width-240,Screen.height/6), "Save")) {
			GameObject.FindGameObjectWithTag("SaveControl").GetComponent<SaveControl>().DoSave();
		}
		if (GUI.Button(new Rect(120,Screen.height/3,Screen.width-240,Screen.height/6), "Load")) { 
			GameObject.FindGameObjectWithTag("PersistentLoader").GetComponent<PersistentLoader>().LoadGame("slot1");
		}
		if (GUI.Button(new Rect(120,Screen.height/2,Screen.width-240,Screen.height/6), "Delete Save Data")) PlayerPrefs.DeleteAll();
		if (GUI.Button(new Rect(120,2*Screen.height/3,Screen.width-240,Screen.height/6), "Quit")) Application.Quit();
	}
}
