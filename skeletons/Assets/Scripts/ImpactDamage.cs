using UnityEngine;
using System.Collections;

/*
 * Deals damage to target and then deletes the parent object
 */
public class ImpactDamage : MonoBehaviour {

	public GameObject owner;	//The parent object of this attack
	public string targetTag;	//the tag of objects affected by this attack
	public float expire = 10;	//Delay until this object is autodestructed
	
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
