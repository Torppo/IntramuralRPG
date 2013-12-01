using UnityEngine;
using System.Collections;

/*
 * Game logic for a vending machine that sells the player healing potions
 */
public class VendingMachine : MonoBehaviour {
	
	public GUIText shoptext;	//A GUI text prompt showing buyPrompt
	
	public Vector3 textpos;	//The position of the buy prompt on the screen
	
	public int potionCost = 10;	//The cost of a health potion
	public string buyPrompt = "(E) Buy healing potion - 10 gold";	//The text of the buy prompt
	
	void Awake(){
		shoptext.text = buyPrompt;
		shoptext.transform.position = textpos;	//set the prompt to correct screen position - otherwise, its position depends on the position of the vending machine
	}
	
	// Use this for initialization
	void OnTriggerStay (Collider other) {
		if (other.tag != Tags.player) return;
		shoptext.enabled = true;	//show the buy prompt
		
		if (Input.GetKeyDown(KeyCode.E)){	//Sell potion when the player presses E
			Inventory inv = other.GetComponent<Inventory>();
			if (inv.Contains(InventoryItem.gold, 10)){
				inv.RemoveItems(InventoryItem.gold, 10);
				inv.AddItems(InventoryItem.healthPot, 1);
			}
		}
	}
	
	void OnTriggerExit (Collider other) {
		if (other.tag != Tags.player) return;
		shoptext.enabled = false;	//hide buy prompt
	}
	
}
