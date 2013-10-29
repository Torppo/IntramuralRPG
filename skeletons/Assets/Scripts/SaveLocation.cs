using UnityEngine;
using System.Collections;

public class SaveLocation : MonoBehaviour {
	
	private SaveControl sc;
	
	// Use this for initialization
	void Awake () {
		sc = GameObject.FindGameObjectWithTag("SaveControl").GetComponent<SaveControl>();
	}
	
	public void SaveData(){
		sc.SaveFloat(this.gameObject, "location.position.x", this.transform.position.x);
		sc.SaveFloat(this.gameObject, "location.position.y", this.transform.position.y);
		sc.SaveFloat(this.gameObject, "location.position.z", this.transform.position.z);
		
		sc.SaveFloat(this.gameObject, "location.rotation.x", this.transform.rotation.x);
		sc.SaveFloat(this.gameObject, "location.rotation.y", this.transform.rotation.y);
		sc.SaveFloat(this.gameObject, "location.rotation.z", this.transform.rotation.z);
		sc.SaveFloat(this.gameObject, "location.rotation.w", this.transform.rotation.w);
		
		sc.SaveFloat(this.gameObject, "location.scale.x", this.transform.localScale.x);
		sc.SaveFloat(this.gameObject, "location.scale.y", this.transform.localScale.y);
		sc.SaveFloat(this.gameObject, "location.scale.z", this.transform.localScale.z);
	}
	
	public void LoadData(){
		this.transform.position = new Vector3(
			sc.LoadFloat(this.gameObject, "location.position.x"),
			sc.LoadFloat(this.gameObject, "location.position.y"),
			sc.LoadFloat(this.gameObject, "location.position.z"));
		this.transform.rotation = new Quaternion(
			sc.LoadFloat(this.gameObject, "location.rotation.x"),
			sc.LoadFloat(this.gameObject, "location.rotation.y"),
			sc.LoadFloat(this.gameObject, "location.rotation.z"),
			sc.LoadFloat(this.gameObject, "location.rotation.w"));
		this.transform.localScale = new Vector3(
			sc.LoadFloat(this.gameObject, "location.scale.x"),
			sc.LoadFloat(this.gameObject, "location.scale.y"),
			sc.LoadFloat(this.gameObject, "location.scale.z"));
	}
}
