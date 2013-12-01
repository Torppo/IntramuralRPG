using UnityEngine;
using System.Collections;

public class InventoryItem {
	
	public static string gold = "Gold";	//Money name
	public static string healthPot = "Health Potion";	//Health potion name
	public static string staff = "Element Staff";	//Fireball staff name
	public static string falchion = "Falchion";	//Starting sword name
	
	public string name;	//the name of this item
	private int _amount;	//The quantity of this item in inventory
	public int amount {
		get{return this._amount;}
		set {
			if (value < 0) throw new System.ArgumentException("Trying to set a negative amount of " + name);
			this._amount = value;
		}
	}
	
	public InventoryItem(string name, int amount = 0){
		this.name = name;
		this._amount = amount;
	}	
	
	public bool Equals(InventoryItem other){
		if (other == null) return false;
		else return name == other.name;
	}
	
	public delegate void UseCallback();
	
	public UseCallback Use = Empty;	//What does this item do when used from inventory
	
	/*
	 * Creates an inventory item from a string.
	 * name: the name of the item to be created. Acceptable names are static strings in this class.
	 */
	public static InventoryItem FromString(string name){
		if (name == gold){
			InventoryItem i = new InventoryItem(gold);
			return i;
		}
		if (name == healthPot){
			InventoryItem i = new InventoryItem(healthPot);
			i.Use = i.HealPlayer;
			return i;
		}
		if (name == falchion){
			InventoryItem i = new InventoryItem(falchion);
			i.Use = SwitchToFalchion;
			return i;
		}
		if (name == staff){
			InventoryItem i = new InventoryItem(staff);
			i.Use = SwitchToStaff;
			return i;
		}
		
		throw new System.ArgumentException("Unknown item name: " + name);
	}
	
	/*
	 * Blank use method - "this item can't be used"
	 */
	private static void Empty() {}
	/*
	 * Healing potion use method - restores 25 life up to max and consumes a potion
	 */
	private void HealPlayer() {
		CharacterStats stats = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<CharacterStats>();
		if (stats.health == stats.maxHealth) return;
		stats.health = System.Math.Min(stats.maxHealth, stats.health + 25);
		this._amount--;
	}
	/*
	 * Falchion use method - equip
	 */
	private static void SwitchToFalchion() {
		WeaponSwitch ws = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<WeaponSwitch>();
		Shooting shot = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<Shooting>();
		ws.SelectWeapon(WeaponSwitch.falchion);
		shot.projectile = null;
	}
	/*
	 * Element staff use method - equip
	 */
	private static void SwitchToStaff() {
		WeaponSwitch ws = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<WeaponSwitch>();
		Shooting shot = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<Shooting>();
		ws.SelectWeapon(WeaponSwitch.elementStaff);
		shot.projectile = "PlayerFireball";
	}
}
