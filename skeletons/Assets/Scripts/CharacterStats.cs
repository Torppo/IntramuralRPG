using UnityEngine;
using System.Collections;

public class CharacterStats : MonoBehaviour {
	public int maxHealth = 100;
	public int health = 50;
	public int damage = 10;
	public bool isAlive = true;
	private Animator anim;
	
	public ParticleSystem bloodEmitter;
	
	public float getHealth(){
		return (float) health / maxHealth;
	}
	public void DealDamage(int damage) {
		if(isAlive) {
			health -= damage;
			bloodEmitter.Play();
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
	
	public void SaveData(ISaveService sc){
		sc.SaveInt(this.gameObject, "isAlive", isAlive ? 1 : 0);
		sc.SaveInt(this.gameObject, "health", health);
	}
	
	public void LoadData(ISaveService sc){
		isAlive = sc.LoadInt(this.gameObject, "isAlive") == 1? true : false;
		health = sc.LoadInt(this.gameObject, "health");
		if (isAlive == false){
			this.gameObject.SetActive(false);
			anim.SetBool("isAlive", false);
		}
	}
}
