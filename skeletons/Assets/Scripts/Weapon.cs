using UnityEngine;
using System.Collections;

/*
 * Handles weapon collisions
 */
public class Weapon : MonoBehaviour {
	
	public GameObject owner;	//the character holding this weapon
	public string targetTag;	//the tag of objects affected by this weapon
	
	
	void OnTriggerStay(Collider other){
		if (other.tag == targetTag && owner.GetComponent<Animator>().GetCurrentAnimatorStateInfo(1).nameHash == HashIDs.attackState){
			other.gameObject.GetComponent<GetHit>().TakeDamage();
		}
	}
}
