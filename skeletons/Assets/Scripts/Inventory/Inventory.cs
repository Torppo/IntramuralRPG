using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Inventory system script: stores a list of InventoryItems and provides methods for manipulating it
 */
public class Inventory : MonoBehaviour {
	
	public List<InventoryItem> items;	//The list of items in player inventory
	
	// Use this for initialization
	void Awake () {
		items = new List<InventoryItem>();
		AddItems(InventoryItem.falchion, 1); //Always start with sword
	}
	
	/*
	 * add [quantity] of [item]s to the inventory
	 */
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
	/*
	 * add [quantity] of [item]s to the inventory
	 * item must be a static string constant from InventoryItem.cs
	 */
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
	/*
	 * returns true if inventory contains at least [quantity] of [item]
	 */
	public bool Contains(InventoryItem item, int quantity){
		int index = GetIndex(item);
		if (index < 0) return false;
		if (items[index].amount < quantity) return false;
		return true;
	}
	/*
	 * returns true if inventory contains at least [quantity] of [item]
	 * item must be a static string constant from InventoryItem.cs
	 */
	public bool Contains(string item, int quantity){
		return Contains(InventoryItem.FromString(item), quantity);
	}
	/*
	 * Returns the amount of [item] currently in the inventory. Returns 0 if the item is not present in the inventory.
	 */
	public int Amount(InventoryItem item){
		int index = GetIndex(item);
		if (index < 0) return 0;
		return items[index].amount;
	}
	/*
	 * Returns the amount of [item] currently in the inventory. Returns 0 if the item is not present in the inventory.
	 * item must be a static string constant from InventoryItem.cs
	 */
	public int Amount(string item){
		return Amount(InventoryItem.FromString(item));
	}
	/*
	 * Removes [quantity] of [item]s from the inventory. Throws ArgumentException if the player has less [item]s than [quantity]; use Amount() to check beforehand.
	 */
	public void RemoveItems(InventoryItem item, int quantity){
		if (quantity < 1) throw new System.ArgumentException("Must remove a positive number of items");
		if (!Contains(item, quantity)) throw new System.ArgumentException("Attempted to remove more of " + item + " than exists in inventory");
		int index = GetIndex(item);
		items[index].amount = items[index].amount - quantity;
	}
	/*
	 * Removes [quantity] of [item]s from the inventory. Throws ArgumentException if the player has less [item]s than [quantity]; use Amount() to check beforehand.
	 * item must be a static string constant from InventoryItem.cs
	 */
	public void RemoveItems(string item, int quantity){
		RemoveItems(InventoryItem.FromString(item), quantity);
	}
	
	public void SaveData(ISaveService sc){
		sc.SaveInt(this.gameObject, "inventory.count", items.Count);
		for (int i = 0; i< items.Count; i++){
			sc.SaveString(this.gameObject, "inventory.items."+ i +".name", items[i].name);
			sc.SaveInt(this.gameObject, "inventory.items."+ i +".amount", items[i].amount);
		}
	}
	
	public void LoadData(ISaveService sc){
		items = new List<InventoryItem>();
		int count = sc.LoadInt(this.gameObject, "inventory.count");
		for (int i = 0; i< count; i++){
			string name = sc.LoadString(this.gameObject, "inventory.items."+ i +".name");
			int amount = sc.LoadInt(this.gameObject, "inventory.items."+ i +".amount");
			
			InventoryItem it = InventoryItem.FromString(name);
			it.amount = amount;
			items.Add(it);
		}
	}
}
