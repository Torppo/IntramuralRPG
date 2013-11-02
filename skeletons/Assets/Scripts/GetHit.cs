using UnityEngine;
using System.Collections;

public class GetHit : MonoBehaviour {
	
	private Animator anim;
	private bool startingHit = false;
	
	void Awake(){
		anim = GetComponent<Animator>();
	}
	
	public void TakeDamage(){
		if (!startingHit && !(anim.GetCurrentAnimatorStateInfo(0).nameHash == HashIDs.getHitState)){
			anim.SetBool(HashIDs.getHitAnimBool, true);
			startingHit = true;
		}
	}
	
	public void Update(){
		//reset hit trigger to prevent animation looping
		if (anim.GetCurrentAnimatorStateInfo(0).nameHash == HashIDs.getHitState){
			anim.SetBool(HashIDs.getHitAnimBool, false);
			startingHit = false;
		}
	}
}
