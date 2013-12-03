using UnityEngine;
using System.Collections;
using System;

public class MainMenu : MonoBehaviour {
	
	public GUIStyle titleStyle;
	public GUIStyle headerStyle;
	public GUIStyle contentStyle;
    	
	float buttonHeight = Screen.height / 8;
	float firstButtonStartPoint = Screen.height * 0.1875f; //this is how much space should be left to get even top/bottom (for current height of 1/8)
	
	bool saveExists = false;
	
	enum Views {MenuView, HelpView, CreditsView};
	Views currentView = Views.MenuView;
	
	Rect getRectangleForNthButton(int nthButton) {
		if (nthButton == 1)
			return new Rect (120, firstButtonStartPoint, Screen.width - 240, buttonHeight);
			
		return new Rect (120, firstButtonStartPoint+(buttonHeight*(nthButton-1)), Screen.width - 240, buttonHeight);
	}
	
	void Start(){
		
		if (GameObject.FindGameObjectWithTag("PersistentLoader").GetComponent<PersistentLoader>().Exist("slot1")){
			saveExists = true;
		}
		else {
			saveExists = false;
		}
	
	}
	
	void OnGUI(){
		switch (currentView){
			
		case Views.MenuView:
			RenderMenuView();
			break;
		case Views.HelpView:
			RenderHelpView();
			break;
		case Views.CreditsView:
			RenderCreditsView();
			break;
		}
		
	}
	
	void RenderMenuView(){

		GUI.Label (new Rect (Screen.width / 2 -50, 20, 100, 20), "Intramural", titleStyle);
		
		if (GUI.Button (getRectangleForNthButton(1), "New Game")) {
			Time.timeScale = 1; // without this game freezes when selecting New game 2nd time onwards
			Application.LoadLevel("test");
		}

		if (saveExists){ 
			GUI.enabled = true; 
		}
		else{ 
			GUI.enabled = false; // to disable Continue button if there is no save game to load
		}

		if (GUI.Button (getRectangleForNthButton(2), "Continue")) { 
			GameObject.FindGameObjectWithTag("PersistentLoader").GetComponent<PersistentLoader>().LoadGame("slot1");		
		}
		GUI.enabled = true;
		
		if (GUI.Button (getRectangleForNthButton(3), "Help")){
			currentView = Views.HelpView;
		}
	
		if (GUI.Button (getRectangleForNthButton(4), "Credits")){			
			currentView = Views.CreditsView;
		}
			
		if (GUI.Button (getRectangleForNthButton(5), "Quit"))
			Application.Quit ();
	}
	
	void RenderHelpView(){
		GUI.Label (new Rect (20, 20, 100, 20), "Help", headerStyle);
		
		String helpContent = "Use WASD or arrow keys to move character\n" +
			"Use mouse to turn camera / character\n" +
			"Use Space bar to attack\n" +
			"Press Esc in game to open (and close) Pause Menu\n" +
			"Press I in game to open (and close) Inventory\n" +
			"Press C in game to pick up coins\n" +
			"Press E in game to use coins\n" +
			"Press X in game to drop breadcrumbs\n" +
			"Use mouse to navigate in menus";
		
		GUILayout.Label( helpContent, contentStyle);
		
		if (GUI.Button(new Rect (120, Screen.height - buttonHeight - 50, Screen.width-240, buttonHeight), "Back"))
			currentView = Views.MenuView;

		
		if (Input.GetKeyDown(KeyCode.Escape)){
			currentView = Views.MenuView;
		}
	
	}
	
	void RenderCreditsView(){
		
		GUI.Label (new Rect (20, 20, 100, 20), "Credits", headerStyle);
		
		String creditsContent = "Developed by Team Intramural\n\n" +
			"Characters, door, gold pile and vending machine from Unity Asset Store\n\n" +
			"Lantern, rock, wooden support and wooden plank by d123s404\n\n" +
			"Textures used in environmental models by Max Boughen, www.maxtextures.com *\n\n" +
			
			"Sounds from http://freesound.org \n" +
			"hit-wood12.wav by JanKoehl **\n" +
			"hitting body with blood.aif by nextmaking ***\n" +
			"Water Dripping in Cave.wav by Sclolex ****\n";
		
		String licenseInfo = "* http://www.mb3d.co.uk/mb3d/The_License_Agreement.html\n" +
			"** http://creativecommons.org/licenses/by/3.0/\n" +
			"*** http://creativecommons.org/licenses/sampling+/1.0/\n" +
			"**** http://creativecommons.org/publicdomain/zero/1.0/";
	
		
		GUILayout.Label(creditsContent, contentStyle);
		GUILayout.Label(licenseInfo, contentStyle);
		
		if (GUI.Button(new Rect (120, Screen.height - buttonHeight - 50, Screen.width-240, buttonHeight), "Back"))
			currentView = Views.MenuView;
		
		if (Input.GetKeyDown(KeyCode.Escape)){
			currentView = Views.MenuView;
		}
	}
}
