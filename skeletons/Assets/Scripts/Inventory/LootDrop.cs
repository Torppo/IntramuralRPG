using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Makes an enemy drop items upon death
 */
public class LootDrop : MonoBehaviour {
	
	public int moneyMin;	//minimum amount of gold dropped
	public int moneyMax;	//maximum amount of gold dropped
	
	public List<string> items;	//items dropped: must be names registered in InstanceManager
	
	public List<GameObject> disableOnDrop;	//List of gameObjects that should be disabled when loot is dropped - use this to remove dropped weapons etc. from corpses
	
	private CharacterStats car;
	private bool hasDropped = false;	//Did we drop the loot yet?
	
	// Use this for initialization
	void Awake () {
		car = GetComponent<CharacterStats>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasDropped && !car.isAlive){
			int droppedMoney = Random.Range(moneyMin, moneyMax);
			if (droppedMoney < 0) droppedMoney = 0; //don't drop negative gold
			if (droppedMoney != 0){ //drop gold
				GameObject o = InstanceManager.manager.Instantiate("CoinPile", transform.position, transform.rotation);
				o.BroadcastMessage("SetItemValues", new Pair<string, int>(InventoryItem.gold, droppedMoney)); //set gold amount
			}
			for (int i = 0; i < items.Count; i++){	//drop other items
				InstanceManager.manager.Instantiate(items[i], transform.position, transform.rotation);
			}
			if (disableOnDrop != null){	//disable items
				for (int i = 0; i < disableOnDrop.Count; i++){
					disableOnDrop[i].SetActive(false);
				}
			}
			this.enabled = false;
		}
	}
}
