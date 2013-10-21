using UnityEngine;
using System.Collections;

public class GetHit : MonoBehaviour {
	
	private Animator anim;
	
	void Awake(){
		anim = GetComponent<Animator>();
	}
	
	public void TakeDamage(){
		if (!(anim.GetCurrentAnimatorStateInfo(0).nameHash == HashIDs.getHitState)){
			anim.SetBool(HashIDs.getHitAnimBool, true);
		}
	}
	
	public void LateUpdate(){
		//reset hit trigger to prevent animation looping
		anim.SetBool(HashIDs.getHitAnimBool, false);
	}
}
