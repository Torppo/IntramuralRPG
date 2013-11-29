using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {
	public Transform target;
	public CharacterStats healthScript;
	public float maxWidth = 150;
	public Vector3 wantedPos;
	public float health;

	// Use this for initialization
	void Start () {
		//health = healthScript.getHealth();
	}
	
	// Update is called once per frame
	void Update () {
		//guiTexture.pixelInset.width = maxWidth *  health / 100;
		wantedPos = Camera.main.WorldToViewportPoint (target.position);
		transform.position = wantedPos;
	}
}
