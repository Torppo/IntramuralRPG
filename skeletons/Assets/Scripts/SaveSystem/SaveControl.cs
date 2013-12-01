using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/*
 * Object that manages saving and loading GameObjects that are in the scene by default
 */
public class SaveControl : MonoBehaviour, ISaveService {
	
	public static SaveControl manager;	//Singleton instance
	
	public string slotname = "slot1";	//The slot we save to and load from
	
	// Use this for initialization
	void Awake () {
		manager = this;
	}
	
	// Update is called once per frame
	void Update () {
		//Keyboard commands
		if (Input.GetKeyDown(KeyCode.F1)) DoSave();
		if (Input.GetKeyDown(KeyCode.F2)) { 
			GameObject.FindGameObjectWithTag("PersistentLoader").GetComponent<PersistentLoader>().LoadGame(slotname);
		}
		if (Input.GetKeyDown(KeyCode.F12)) PlayerPrefs.DeleteAll();
	}
	
	//Save current state to the selected slot
	public void DoSave(){
		//Save states of all children
		this.BroadcastMessage("SaveData", this);
		//Save states of instances
		InstanceManager.manager.SaveData();
		//Save index of current scene
		PlayerPrefs.SetInt(slotname + "._level", Application.loadedLevel);

	}
	
	//Load state from selected slot
	public void DoLoad(){
		//Load children
		this.BroadcastMessage("LoadData", this);
		//Load instances
		InstanceManager.manager.LoadData();
	}
	
	public void SaveString(GameObject obj, string name, string content) {
		PlayerPrefs.SetString(slotname + "." + obj.name + "." + name, content);
	}
	
	public string LoadString(GameObject obj, string name){
		return PlayerPrefs.GetString(slotname + "." + obj.name + "." + name);
	}
	
	public void SaveFloat(GameObject obj, string name, float content) {
		PlayerPrefs.SetFloat(slotname + "." + obj.name + "." + name, content);
	}
	
	public float LoadFloat(GameObject obj, string name){
		return PlayerPrefs.GetFloat(slotname + "." + obj.name + "." + name);
	}
	
	public void SaveInt(GameObject obj, string name, int content) {
		PlayerPrefs.SetInt(slotname + "." + obj.name + "." + name, content);
	}
	
	public int LoadInt(GameObject obj, string name){
		return PlayerPrefs.GetInt(slotname + "." + obj.name + "." + name);
	}
	
	public static string Serialize(object o){
		BinaryFormatter bf = new BinaryFormatter();
		MemoryStream m = new MemoryStream();
		bf.Serialize(m, o);
		return System.Convert.ToBase64String(m.GetBuffer());
	}
	
	public static object Deserialize(string serial){
		if (string.IsNullOrEmpty(serial)) return null;
		
		BinaryFormatter bf = new BinaryFormatter();
		MemoryStream m = new MemoryStream(System.Convert.FromBase64String(serial));
		return bf.Deserialize(m);
	}
}
