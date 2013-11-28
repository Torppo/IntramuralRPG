using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LootDrop : MonoBehaviour {
	
	public int moneyMin;
	public int moneyMax;
	
	public List<string> items;
	
	public List<GameObject> disableOnDrop;
	
	private CharacterStats car;
	private bool hasDropped = false;
	
	// Use this for initialization
	void Awake () {
		car = GetComponent<CharacterStats>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasDropped && !car.isAlive){
			int droppedMoney = Random.Range(moneyMin, moneyMax);
			if (droppedMoney < 0) droppedMoney = 0;
			if (droppedMoney != 0){
				GameObject o = InstanceManager.manager.Instantiate("CoinPile", transform.position, transform.rotation);
				o.BroadcastMessage("SetItemValues", new Pair<string, int>(InventoryItem.gold, droppedMoney));
			}
			for (int i = 0; i < items.Count; i++){
				InstanceManager.manager.Instantiate(items[i], transform.position, transform.rotation);
			}
			if (disableOnDrop != null){
				for (int i = 0; i < disableOnDrop.Count; i++){
					disableOnDrop[i].SetActive(false);
				}
			}
			this.enabled = false;
		}
	}
}
