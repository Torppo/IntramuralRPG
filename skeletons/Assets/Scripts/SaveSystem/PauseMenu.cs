using UnityEngine;
using System.Collections;

/*
 * Draws and controls the pause and inventory menus
 */
public class PauseMenu : MonoBehaviour {

	public bool paused = false;	//is the game paused?
	public bool inv = false;	//is the inventory open?
	public bool menu = false;	//is the pause menu open?
	
	private MouseOrbitImproved moi;
	private Inventory inventory;
		
	void Awake(){
		moi = GetComponent<MouseOrbitImproved>();
		inventory = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<Inventory>();
	}
	
	void Update(){
		//player pressed menu key
		if (Input.GetKeyDown(KeyCode.Escape)){
			//menu & inventory not open
			if (!paused && !inv){
				OpenMenu();
				menu = true;
			}
			//menu open
			else if (menu) {
				CloseMenu();
				menu = false;
			}
		}
		//player pressed inventory key
		if (Input.GetKeyDown(KeyCode.I)){
			//menu & inventory not open
			if (!paused && !menu){
				OpenMenu();
				inv = true;
			}
			//menu open
			else if (inv) {
				CloseMenu();
				inv = false;
			}
		}
	}
	
	//Open a menu
	private void OpenMenu(){
		Time.timeScale = 0.0f;
		paused = true;
		moi.enabled = false;
	}
	//Close a menu
	private void CloseMenu(){
		Time.timeScale = 1.0f;
		paused = false;
		moi.enabled = true;
	}
	
	void OnGUI() {
		//Pause menu open
		if (menu) {
			//background
			GUI.Box(new Rect(100, 50, Screen.width - 200, Screen.height - 100), "Pause Menu");
			//save button
			if (GUI.Button(new Rect(120,Screen.height/6,Screen.width-240,Screen.height/6), "Save")) {
				GameObject.FindGameObjectWithTag(Tags.saveControl).GetComponent<SaveControl>().DoSave();
			}
			//load button
			if (GUI.Button(new Rect(120,Screen.height/3,Screen.width-240,Screen.height/6), "Load")) { 
				GameObject.FindGameObjectWithTag(Tags.persistentLoader).GetComponent<PersistentLoader>().LoadGame("slot1");
			}
			//delete saves button
			if (GUI.Button(new Rect(120,Screen.height/2,Screen.width-240,Screen.height/6), "Delete Save Data")) PlayerPrefs.DeleteAll();
			//exit button
			if (GUI.Button(new Rect(120,2*Screen.height/3,Screen.width-240,Screen.height/6), "Quit")) Application.Quit();
		}
		
		//Inventory open
		else if (inv){
			//background
			GUI.Box(new Rect(100, 50, Screen.width - 200, Screen.height - 100), "Inventory");
			//draw item buttons
			for (int i = 0; i < inventory.items.Count; i++){
				if (GUI.Button(new Rect(120 + (Screen.width-240)/3*(i%3),//left edge
					80 + Screen.height/10 * (i/3),//top edge
					(Screen.width-240)/3,//width
					Screen.height/10),//height
					inventory.items[i].name + ": " + inventory.items[i].amount)) {//name
					//use item when clicked
					if (inventory.items[i].amount > 0) inventory.items[i].Use();
				}
			}
			//close button
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
