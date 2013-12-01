using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Manages the saving and loading of game objects that are instantiated in the scene at runtime.
 */
public class InstanceManager : MonoBehaviour, ISaveService {
	
	public static InstanceManager manager;	//The singleton of this class
	
	private Dictionary<string, GameObject> prefabDict;	//Dictionary mapping string names to instantiable prefabs
	public List<GameObject> prefabs;	//The list of prefabs that can be instantiated by this InstanceManager
	
	private int currentChildIndex = 0;	//index of the child currently being processed

	// Use this for initialization
	void Awake () {
		manager = this;
		
		//generate the name-prefab dictionary from the prefab list
		prefabDict = new Dictionary<string, GameObject>();	
		
		for (int i = 0; i < prefabs.Count; i++){
			prefabDict.Add(prefabs[i].name, prefabs[i]);
		}
	}
	
	/*
	 * Creates a new Game Object at [loc] facing [rot]. The object created is the prefab registered in the prefab list of this class with the name [name];
	 * if no prefab named [name] has been registered, throws ArgumentException
	 */
	public GameObject Instantiate(string name, Vector3 loc, Quaternion rot){
		if (!prefabDict.ContainsKey(name)){
			throw new System.ArgumentException("Error: Attempted to instantiate unregistered prefab " + name + "; add the missing prefab to the prefabs list in the InstanceManager script");
		}
		
		GameObject instance = GameObject.Instantiate(prefabDict[name], loc, rot) as GameObject;
		instance.transform.parent = this.transform;	//make this the parent to properly propagate save and load calls
		instance.name = instance.name.Remove(instance.name.Length - 7); //remove "clone" from the name to save properly
		return instance;
	}
	
	/*
	 * Saves the current state of all instantiated GameObjects in the slot specified by the current SaveControl manager
	 */
	public void SaveData(){
		SaveControl.manager.SaveInt(this.gameObject, "_childCount", transform.childCount);
		
		for (currentChildIndex = 0; currentChildIndex < transform.childCount; currentChildIndex++){
			SaveControl.manager.SaveString(this.gameObject, "_instance" + currentChildIndex, transform.GetChild(currentChildIndex).name);
			transform.GetChild(currentChildIndex).BroadcastMessage("SaveData", this);
		}
	}
	
	/*
	 * Loads the state of instantiated GameObjects from the slot specified by the current SaveControl manager
	 */
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
	
	//For the rest, see ISaveService
	
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
