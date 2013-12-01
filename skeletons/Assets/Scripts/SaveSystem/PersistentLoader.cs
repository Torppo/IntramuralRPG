using UnityEngine;
using System.Collections;

/*
 * Persistent GameObject that allows loading scenes while loading game state
 */
public class PersistentLoader : MonoBehaviour {
	
	public bool loading = false;	//Are we loading state right now?
	public string saveSlot;	//Where should we load from?
	
	
	public void Awake(){
		//Singleton - only allow one instance
		if (GameObject.FindGameObjectsWithTag(Tags.persistentLoader).Length > 1){
			GameObject.Destroy(this.gameObject);
		}
		//Make this persistent so it stays alive when changing scenes
		else GameObject.DontDestroyOnLoad(this);
	}
	
	/*
	 * Load the game stored in slot [saveSlot]
	 */
	public void LoadGame(string saveSlot){
		if (!PlayerPrefs.HasKey(saveSlot + "._level")){
			throw new System.ArgumentException("Unable to load save " + saveSlot + ": level parameter not found");
		}
		else {
			this.saveSlot = saveSlot;	//store the slot for later use
			loading = true;
			//Load the scene where the save occurred
			Application.LoadLevel(PlayerPrefs.GetInt(saveSlot + "._level"));
		}
	}
	
	public void Update(){
		//Are we loading state and is the scene loaded
		if (loading && !Application.isLoadingLevel){
			loading = false;
			SaveControl sc = GameObject.FindGameObjectWithTag(Tags.saveControl).GetComponent<SaveControl>();
			sc.slotname = saveSlot;
			sc.DoLoad();	//Load state
			Time.timeScale = 1.0f;	//Unpause the game
		}
	}
}
