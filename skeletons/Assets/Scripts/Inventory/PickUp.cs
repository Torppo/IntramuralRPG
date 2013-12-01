using UnityEngine;
using System.Collections;

/*
 * A script that allows players to pick up an item and add it to the inventory
 */
public class PickUp : MonoBehaviour {
	
	public GameObject owner;	//the parent GameObject of the picked up object
	public string item;	//The item that the player receives - must be a static string constant from InventoryItem
	public int quantity = 0;	//the amount of items that the player receives
	public float markerLightIntensity = 5f;	//The intensity of the light that highlights the item ehen the player is near

	
	void OnTriggerStay (Collider other){
		if (other.tag == Tags.player){
			light.intensity = markerLightIntensity;	//highlight the drop
			if (Input.GetKeyDown(KeyCode.C)){	//Pick up when the player presses C
				other.GetComponent<Inventory>().AddItems(item, quantity);
				GameObject.Destroy(owner);
			}
		}
	}
	
	/*
	 * Sets the item and quantity values of this loot drop
	 */
	void SetItemValues (Pair<string, int> vals){
		this.item = vals.First;
		this.quantity = vals.Second;
	}
	
	void OnTriggerExit (Collider other) { // turn of the hoghlight
		if (other.tag == Tags.player) light.intensity = 0f;
	}
	
	public void SaveData(ISaveService sc){
		sc.SaveString(owner, "pickup.item", item);
		sc.SaveInt(owner, "pickup.quantity", quantity);
	}
	
	public void LoadData(ISaveService sc){
		item = sc.LoadString(owner, "pickup.item");
		quantity = sc.LoadInt(owner, "pickup.quantity");
	}
}
