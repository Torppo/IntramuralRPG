using UnityEngine;
using System.Collections;

public class GetHit : MonoBehaviour {
	
	private Animator anim;
	private bool startingHit = false;
	private CharacterStats car;
	
	void Awake(){
		anim = GetComponent<Animator>();
		car = GetComponent<CharacterStats>();
	}
	
	public void TakeDamage(){
		if (!startingHit && !(anim.GetCurrentAnimatorStateInfo(0).nameHash == HashIDs.getHitState)){
			anim.SetBool(HashIDs.getHitAnimBool, true);
			startingHit = true;
			audio.Play();
			car.DealDamage(car.damage);
		}
	}
	
	public void Update(){
		if(anim.GetCurrentAnimatorStateInfo(0).nameHash == HashIDs.dyingState) {
			anim.SetBool(HashIDs.isAliveBool, true);
		}
		//reset hit trigger to prevent animation looping
		if (anim.GetCurrentAnimatorStateInfo(0).nameHash == HashIDs.getHitState){
			anim.SetBool(HashIDs.getHitAnimBool, false);
			startingHit = false;
		}
	}
}
