using UnityEngine;
using System.Collections;

public class TargetSeeker : MonoBehaviour {
	
	private Follow follow;
	
	// Use this for initialization
	void Start () {
		follow = transform.parent.GetComponent<Follow>();
	}
	
	// Update is called once per frame
	void Update () {
	
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
