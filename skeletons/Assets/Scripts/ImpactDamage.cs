using UnityEngine;
using System.Collections;

public class ImpactDamage : MonoBehaviour {

	public GameObject owner;	//the character holding this weapon
	public string targetTag;	//the tag of objects affected by this weapon
	public float expire = 10;
	
	private float expireTimer = 0;
	
	void Update() {
		expireTimer += Time.deltaTime;
		if (expireTimer > expire) GameObject.Destroy(owner);
	}
	
	void OnTriggerEnter(Collider other){
		if (other.tag == targetTag){
			other.gameObject.GetComponent<GetHit>().TakeDamage();
		}
		if (other.gameObject != owner && !other.isTrigger){
			GameObject.Destroy(owner);
		}
		
	}
}
