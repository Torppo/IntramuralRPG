using UnityEngine;
using System.Collections;

public class HeroMain : MonoBehaviour {
	public int maxHealth = 100;
	public int health = 50;
	
	public float getHealth(){
		return (float) health / maxHealth;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
