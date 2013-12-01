using UnityEngine;
using System.Collections;

public class VendingMachine : MonoBehaviour {
	
	public GUIText shoptext;
	
	public Vector3 textpos;
	
	public int potionCost = 10;
	public string buyPrompt = "(E) Buy healing potion - 10 gold";
	
	void Awake(){
		shoptext.text = buyPrompt;
		Vector3 newpos = shoptext.transform.InverseTransformPoint(textpos);
		Debug.Log(newpos);
		shoptext.transform.localPosition = newpos;
		Debug.Log(shoptext.transform.position);
	}
	
	// Use this for initialization
	void OnTriggerStay (Collider other) {
		if (other.tag != Tags.player) return;
		shoptext.enabled = true;
		
		Debug.Log("trigger");
		if (Input.GetKeyDown(KeyCode.E)){
			Inventory inv = other.GetComponent<Inventory>();
			if (inv.Contains(InventoryItem.gold, 10)){
				inv.RemoveItems(InventoryItem.gold, 10);
				inv.AddItems(InventoryItem.healthPot, 1);
			}
		}
	}
	
	void OnTriggerExit (Collider other) {
		if (other.tag != Tags.player) return;
		shoptext.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
