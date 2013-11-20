using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InstanceManager : MonoBehaviour, ISaveService {
	
	public static InstanceManager manager;
	
	private Dictionary<string, GameObject> prefabDict;
	public List<GameObject> prefabs;
	
	private int currentChildIndex = 0;

	// Use this for initialization
	void Awake () {
		manager = this;
		
		prefabDict = new Dictionary<string, GameObject>();
		
		for (int i = 0; i < prefabs.Count; i++){
			prefabDict.Add(prefabs[i].name, prefabs[i]);
		}
	}
	
	public GameObject Instantiate(string name, Vector3 loc, Quaternion rot){
		if (!prefabDict.ContainsKey(name)){
			throw new System.ArgumentException("Error: Attempted to instantiate unregistered prefab " + name + "; add the missing prefab to the prefabs list in the InstanceManager script");
		}
		
		GameObject instance = GameObject.Instantiate(prefabDict[name], loc, rot) as GameObject;
		instance.transform.parent = this.transform;
		instance.name = instance.name.Remove(instance.name.Length - 7); //remove "clone" from the name to save properly
		return instance;
	}
	
	public void SaveData(){
		SaveControl.manager.SaveInt(this.gameObject, "_childCount", transform.childCount);
		
		for (currentChildIndex = 0; currentChildIndex < transform.childCount; currentChildIndex++){
			SaveControl.manager.SaveString(this.gameObject, "_instance" + currentChildIndex, transform.GetChild(currentChildIndex).name);
			transform.GetChild(currentChildIndex).BroadcastMessage("SaveData", this);
		}
	}
	
	public void LoadData(){
		int childCount = SaveControl.manager.LoadInt(this.gameObject, "_childCount");
		
		for (currentChildIndex = 0; currentChildIndex < childCount; currentChildIndex++){
			string instanceName = SaveControl.manager.LoadString(this.gameObject, "_instance" + currentChildIndex);
			if (!prefabDict.ContainsKey(instanceName)){
				Debug.LogError("Warning! Attempted to load unregistered prefab " + instanceName);
				continue;
			}
			GameObject instance = Instantiate(instanceName, Vector3.zero, Quaternion.identity);
			instance.BroadcastMessage("LoadData", this);
		}
	}
	
	public void SaveString(GameObject obj, string name, string content) {
		SaveControl.manager.SaveString(this.gameObject, "_instance" + currentChildIndex + "." + obj.name + "." + name, content);
	}
	
	public string LoadString(GameObject obj, string name){
		return SaveControl.manager.LoadString(this.gameObject, "_instance" + currentChildIndex + "." + obj.name + "." + name);
	}
	
	public void SaveFloat(GameObject obj, string name, float content) {
		SaveControl.manager.SaveFloat(this.gameObject, "_instance" + currentChildIndex + "." + obj.name + "." + name, content);
	}
	
	public float LoadFloat(GameObject obj, string name){
		return SaveControl.manager.LoadFloat(this.gameObject, "_instance" + currentChildIndex + "." + obj.name + "." + name);
	}
	
	public void SaveInt(GameObject obj, string name, int content) {
		SaveControl.manager.SaveInt(this.gameObject, "_instance" + currentChildIndex + "." + obj.name + "." + name, content);
	}
	
	public int LoadInt(GameObject obj, string name){
		return SaveControl.manager.LoadInt(this.gameObject, "_instance" + currentChildIndex + "." + obj.name + "." + name);
	}
}
