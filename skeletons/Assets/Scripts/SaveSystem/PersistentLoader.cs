using UnityEngine;
using System.Collections;

public class PersistentLoader : MonoBehaviour {
	
	public bool loading = false;
	public string saveSlot;
	
	public void Awake(){
		if (GameObject.FindGameObjectsWithTag("PersistentLoader").Length > 1){
			GameObject.Destroy(this.gameObject);
		}
		else GameObject.DontDestroyOnLoad(this);
	}
	
	public void LoadGame(string saveSlot){
		if (!PlayerPrefs.HasKey(saveSlot + "._level")){
			throw new System.ArgumentException("Unable to load save " + saveSlot + ": level parameter not found");
		}
		else {
			this.saveSlot = saveSlot;
			loading = true;
			Application.LoadLevel(PlayerPrefs.GetInt(saveSlot + "._level"));
		}
	}
	
	public void Update(){
		if (loading && !Application.isLoadingLevel){
			loading = false;
			SaveControl sc = GameObject.FindGameObjectWithTag("SaveControl").GetComponent<SaveControl>();
			sc.slotname = saveSlot;
			sc.DoLoad();
		}
	}
}
