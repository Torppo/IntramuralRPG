using UnityEngine;
using System.Collections;

/*
 * Tag and layer constants
 */
public class Tags{
	public static readonly string player = "Player";	//Player game object
	public static readonly string enemy = "Enemy";	//Enemy game objects
	public static readonly string screenFader = "ScreenFader";	//The screen fader
	
	public static readonly string saveControl = "SaveControl"; //The save controller
	public static readonly string persistentLoader = "PersistentLoader"; //The persistent loader object
	
	public static readonly int layer_dynamic = 8;	//Objects that are allowed to obscure the camera view
}
