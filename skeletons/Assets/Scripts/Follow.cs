using UnityEngine;
using System.Collections;

/*
 * Script for making an object with a character controller follow and attack another object
 */
public class Follow : MonoBehaviour {
	
	public GameObject followTarget;	//The character we're following
	
	public float moveSpeed = 2f;	//the follow speed
	public float turnSpeed = 10f;	//the turning speed
	public float stopDistance = 0.5f;	//how close the object should get to the player before stopping
	
	
	private Animator anim;
	private CharacterStats car;
	private Shooting shot;
	private CharacterController cc;
	
	void Start () {
		anim = GetComponent<Animator>();
		car = GetComponent<CharacterStats>();
		shot = GetComponent<Shooting>();
		cc = this.GetComponent<CharacterController>();
	}
	
	void Update () {
		
		if (followTarget == null || car.isAlive == false){
			//stop if there's no target
			anim.SetFloat(HashIDs.movementSpeedFloat, 0f);
			anim.SetBool(HashIDs.strikeAnimBool, false);
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
		//Attack if we're next to the target
		else {
			anim.SetFloat(HashIDs.movementSpeedFloat, 0f);
			anim.SetBool(HashIDs.strikeAnimBool, true);
			
			//Shoot if we have a shot
			if (shot != null){
				shot.Shoot(followTarget.transform.position);
			}
		}
		
		if (anim.GetCurrentAnimatorStateInfo(1).nameHash == HashIDs.attackState){
			//reset attack animation trigger
			anim.SetBool(HashIDs.strikeAnimBool, false);
		}
			
	}
	
}
