using UnityEngine;
using System.Collections;

/*
 * The script that stores a character's stats.
 */
public class CharacterStats : MonoBehaviour {
	public int maxHealth = 100;	//This character's maximum health
	public int health = 50;	//This character's current health
	public int damage = 10;	//The damage that this character takes from attacks (should maybe be on weapon?)
	public bool isAlive = true;	//Is this character alive?
	private Animator anim;	//This character's animation controller
	
	public ParticleSystem bloodEmitter;	//Emitter for effects when taking damage
	
	/*
	 * Returns this character's health in relation to max health
	 */
	public float getHealth(){
		return (float) health / maxHealth;
	}
	
	/*
	 * Damages this character.
	 * Parameter damage: the amount of damage dealt
	 */
	public void DealDamage(int damage) {
		if(isAlive) {
			health -= damage;
			bloodEmitter.Play();	//splash blood
			if(health <= 0) {
				isAlive = false;
				anim.SetBool(HashIDs.isAliveBool, false);
				this.collider.enabled = false;
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
	
	public void SaveData(ISaveService sc){
		sc.SaveInt(this.gameObject, "stats.isAlive", isAlive ? 1 : 0);
		sc.SaveInt(this.gameObject, "stats.health", health);
	}
	
	public void LoadData(ISaveService sc){
		isAlive = sc.LoadInt(this.gameObject, "stats.isAlive") == 1? true : false;
		health = sc.LoadInt(this.gameObject, "stats.health");
		if (isAlive == false){	
			//deactivate dead character to prevent death animation from replaying on load
			this.gameObject.SetActive(false);
			anim.SetBool(HashIDs.isAliveBool, false);
		}
	}
}
