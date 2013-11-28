using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	public bool paused = false;
	public bool inv = false;
	public bool menu = false;
	
	private MouseOrbitImproved moi;
	private Inventory inventory;
		
	void Awake(){
		moi = GetComponent<MouseOrbitImproved>();
		inventory = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<Inventory>();
	}
	
	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)){
			if (!paused && !inv){
				OpenMenu();
				menu = true;
			}
			else if (menu) {
				CloseMenu();
				menu = false;
			}
		}
		if (Input.GetKeyDown(KeyCode.I)){
			if (!paused && !menu){
				OpenMenu();
				inv = true;
			}
			else if (inv) {
				CloseMenu();
				inv = false;
			}
		}
	}
	
	private void OpenMenu(){
		Time.timeScale = 0.0f;
		paused = true;
		moi.enabled = false;
	}
	
	private void CloseMenu(){
		Time.timeScale = 1.0f;
		paused = false;
		moi.enabled = true;
	}
	
	void OnGUI() {
		
		if (menu) {
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
		else if (inv){
			GUI.Box(new Rect(100, 50, Screen.width - 200, Screen.height - 100), "Inventory");
			for (int i = 0; i < inventory.items.Count; i++){
				if (GUI.Button(new Rect(120 + (Screen.width-240)/3*(i%3),
					80 + Screen.height/10 * (i/3),
					(Screen.width-240)/3,
					Screen.height/10),
					inventory.items[i].name + ": " + inventory.items[i].amount)) {
					
					if (inventory.items[i].amount > 0) inventory.items[i].Use();
				}
			}
			if (GUI.Button(new Rect(120,
					Screen.height - (80 + Screen.height/10),
					Screen.width-240,
					Screen.height/10),
					"Close")) {
				CloseMenu();
				inv = false;
			}
		}
	}
}
