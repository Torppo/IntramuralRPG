using UnityEngine;
using System.Collections;

/*
 * Makes the parent object follow the player when they enter the trigger zone
 */
public class FindTarget : MonoBehaviour {
	
	private Follow follow; //the parent object's follow script
	
	void Start () {
		follow = transform.parent.GetComponent<Follow>();
	}
	
	void Update () {
		if (follow.followTarget && follow.followTarget.GetComponent<CharacterStats>().isAlive == false){
			follow.followTarget = null;
		}
	}
	
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player"){
			follow.followTarget = other.gameObject;
		}
	}
	
	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Player"){
			follow.followTarget = null;
		}
	}
}
