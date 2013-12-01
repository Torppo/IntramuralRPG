using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {
	
	public GameObject owner;
	public string item;
	public int quantity = 0;
	public float markerLightIntensity = 5f;

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerStay (Collider other){
		if (other.tag == Tags.player){
			light.intensity = markerLightIntensity;
			if (Input.GetKeyDown(KeyCode.C)){
				other.GetComponent<Inventory>().AddItems(item, quantity);
				GameObject.Destroy(owner);
			}
		}
	}
	
	void SetItemValues (Pair<string, int> vals){
		this.item = vals.First;
		this.quantity = vals.Second;
	}
	
	void OnTriggerExit (Collider other) {
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
