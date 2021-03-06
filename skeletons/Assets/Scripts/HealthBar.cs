﻿using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	public float barDisplay; //current progress
	public Vector2 pos = new Vector2(20,40);
	public Vector2 size = new Vector2(60,20);
	public Texture2D emptyTex;
	public Texture2D fullTex;
	public GUIStyle style;
	
	public float restartCountdown = 3f;
	
	public CharacterStats hero;
	
	void OnGUI() {
		//draw the background:
		GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
		//GUI.skin.box.stretchHeight = true;
		//GUI.skin.box.stretchWidth = true;
		GUI.Box(new Rect(0,0, size.x, size.y), emptyTex,style);
		
		//draw the filled-in part:
		GUI.BeginGroup(new Rect(0,0, size.x * barDisplay, size.y));
		//GUI.skin.box.stretchHeight = true;
		//GUI.skin.box.stretchWidth = true;
		GUI.Box(new Rect(0,0, size.x, size.y), fullTex, style);
		GUI.EndGroup();
		GUI.EndGroup();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		barDisplay = hero.getHealth();
		if (hero.isAlive == false){
			GameObject.FindGameObjectWithTag(Tags.screenFader).GetComponent<Fader>().fadeToBlack = true;
			if (restartCountdown >= 0f){
				restartCountdown -= Time.deltaTime;	
			}
			else {
				Application.LoadLevel(0);	
			}
		}
	}
}
