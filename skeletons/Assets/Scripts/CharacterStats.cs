using UnityEngine;
using System.Collections;

public class CharacterStats : MonoBehaviour {
	public int maxHealth = 100;
	public int health = 50;
	public int damage = 10;
	public bool isAlive = true;
	private Animator anim;
	
	public float getHealth(){
		return (float) health / maxHealth;
	}
	public void DealDamage(int damage) {
		if(isAlive) {
			health -= damage;
			if(health <= 0) {
				isAlive = false;
				anim.SetBool("isAlive", false);
			}
		}
	}
	
	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
