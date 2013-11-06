using UnityEngine;
using System.Collections;

/*
 * Script for making an object with a character controller follow another object
 */
public class Follow : MonoBehaviour {
	
	public GameObject followTarget;
	
	public float moveSpeed = 2f;	//the follow speed
	public float turnSpeed = 10f;	//the turning speed
	public float stopDistance = 0.5f;	//how close the object should get to the player before stopping
	
	private Animator anim;
	private CharacterStats car;
	
	void Start () {
		anim = GetComponent<Animator>();
		car = GetComponent<CharacterStats>();
	}
	
	void Update () {
		CharacterController cc = this.GetComponent<CharacterController>();
		
		if (followTarget == null || car.isAlive == false){
			//stop if there's no target
			anim.SetFloat(HashIDs.movementSpeedFloat, 0f);
			return;
		}
		
		//face target
		Vector3 targetDir =  -1 *(transform.position - followTarget.transform.position);
		targetDir.y = 0;
		float step = turnSpeed * Time.deltaTime;
    	Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
		transform.rotation = Quaternion.LookRotation(newDir);
		
		if ((transform.position - followTarget.transform.position).magnitude > stopDistance){
			//move towards target
			Vector3 moveDirection = transform.forward;
			moveDirection *= moveSpeed;
			moveDirection *= Time.deltaTime;
			
			cc.Move(moveDirection);
			
			anim.SetFloat(HashIDs.movementSpeedFloat, moveSpeed);
			
		}
		else {
			anim.SetFloat(HashIDs.movementSpeedFloat, 0f);
			anim.SetBool(HashIDs.strikeAnimBool, true);
		}
		
		if (anim.GetCurrentAnimatorStateInfo(1).nameHash == HashIDs.attackState){
			anim.SetBool(HashIDs.strikeAnimBool, false);
		}
	}
	
}
