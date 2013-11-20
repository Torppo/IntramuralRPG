using UnityEngine;
using System.Collections;

/*
 * Script that handles the player movement and animation
 */
public class PlayerMovement : MonoBehaviour {
	
	public float moveSpeed = 10f; //player movement speed
	public float turnSpeed = 100f;	//player turn speed
	
	public Camera cam;
	
	private Animator anim;
	private CharacterController cc;
	private CharacterStats car;
	
	void Awake () {
		anim = GetComponent<Animator>();
		cc = this.GetComponent<CharacterController>();
		car = GetComponent<CharacterStats>();
	}
	
	void Update () {
		float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
		Move(h,v);
	}
	
	void Move(float h, float v){
		
		if (anim.GetCurrentAnimatorStateInfo(0).nameHash != HashIDs.getHitState && car.isAlive){	//don't move during hitstun
			
			Vector3 moveDirection;
			Vector3 right = cam.transform.right;
			right.y = 0;
			Vector3 forward = cam.transform.forward;
			forward.y = 0;
			
			moveDirection = right.normalized * h + forward.normalized * v;
			float step = turnSpeed * Time.deltaTime;
    		Vector3 newDir = Vector3.RotateTowards(transform.forward, moveDirection, step, 0.0F);
			transform.rotation = Quaternion.LookRotation(newDir);
			
			moveDirection *= moveSpeed;
			moveDirection *= Time.deltaTime;
			
			cc.Move(moveDirection);
			
			
			Animate(moveDirection.magnitude, Input.GetKeyDown(KeyCode.Space));
		}
	}
	
	void Animate (float v, bool striking){
		anim.SetFloat(HashIDs.movementSpeedFloat, v);
		anim.SetBool(HashIDs.strikeAnimBool, striking);
	}
}
