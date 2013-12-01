using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	
	public List<InventoryItem> items;	//The list of items in player inventory
	
	// Use this for initialization
	void Awake () {
		items = new List<InventoryItem>();
		AddItems(InventoryItem.falchion, 1); //Always start with sword
	}
	
	public void AddItems(InventoryItem item, int quantity){
		if (quantity < 1) throw new System.ArgumentException("Must add a positive number of items");
		int index = GetIndex(item);
		if (index < 0) {
			items.Add(item);
			item.amount = quantity;
		}
		else {
			items[index].amount = items[index].amount + quantity;
		}
	}
	
	public void AddItems(string item, int quantity){
		AddItems(InventoryItem.FromString(item), quantity);
	}
	
	private int GetIndex(InventoryItem item){
		for (int i = 0; i < items.Count; i++){
			if ((items[i] != null) && (items[i].Equals(item))){
				return i;
			}
		}
		return -1;
	}
	
	public bool Contains(InventoryItem item, int quantity){
		int index = GetIndex(item);
		if (index < 0) return false;
		if (items[index].amount < quantity) return false;
		return true;
	}
	
	public bool Contains(string item, int quantity){
		return Contains(InventoryItem.FromString(item), quantity);
	}
	
	public int Amount(InventoryItem item){
		int index = GetIndex(item);
		if (index < 0) return 0;
		return items[index].amount;
	}
	
	public int Amount(string item){
		return Amount(InventoryItem.FromString(item));
	}
	
	public void RemoveItems(InventoryItem item, int quantity){
		if (quantity < 1) throw new System.ArgumentException("Must remove a positive number of items");
		if (!Contains(item, quantity)) throw new System.ArgumentException("Attempted to remove more of " + item + " than exists in inventory");
		int index = GetIndex(item);
		items[index].amount = items[index].amount - quantity;
	}
	
	public void RemoveItems(string item, int quantity){
		RemoveItems(InventoryItem.FromString(item), quantity);
	}
}
