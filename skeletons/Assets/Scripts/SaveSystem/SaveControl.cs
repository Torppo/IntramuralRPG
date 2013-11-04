using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveControl : MonoBehaviour, ISaveService {
	
	public static SaveControl manager;
	
	public string slotname = "slot1";
	
	// Use this for initialization
	void Awake () {
		manager = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.F1)) DoSave();
		if (Input.GetKeyDown(KeyCode.F2)) { 
			GameObject.FindGameObjectWithTag("PersistentLoader").GetComponent<PersistentLoader>().LoadGame(slotname);
		}
		if (Input.GetKeyDown(KeyCode.F12)) PlayerPrefs.DeleteAll();
	}
	
	public void DoSave(){
		this.BroadcastMessage("SaveData", this);
		InstanceManager.manager.SaveData();
		PlayerPrefs.SetInt(slotname + "._level", Application.loadedLevel);

	}
	
	public void DoLoad(){
		this.BroadcastMessage("LoadData", this);
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
