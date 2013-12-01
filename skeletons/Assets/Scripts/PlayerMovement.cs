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
	private Shooting shot;
	
	void Awake () {
		anim = GetComponent<Animator>();
		cc = this.GetComponent<CharacterController>();
		car = GetComponent<CharacterStats>();
		shot = GetComponent<Shooting>();
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
			
			moveDirection *= moveSpeed;
			moveDirection *= Time.deltaTime;
			
			cc.Move(moveDirection);
			
			Animate(moveDirection.magnitude, Input.GetKeyDown(KeyCode.Space));
			
			if (h == 0 && v == 0){
				moveDirection = forward;
			}
			
			float step = turnSpeed * Time.deltaTime;
    		Vector3 newDir = Vector3.RotateTowards(transform.forward, moveDirection, step, 0.0F);
			transform.rotation = Quaternion.LookRotation(newDir);
			
		}
	}
	
	void Animate (float v, bool striking){
		anim.SetFloat(HashIDs.movementSpeedFloat, v);
		anim.SetBool(HashIDs.strikeAnimBool, striking);
		if (shot != null && v == 0 && (anim.GetCurrentAnimatorStateInfo(1).nameHash == HashIDs.attackState)){
			Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2));
			shot.Shoot(ray.GetPoint(10f));
		}
		else {
			shot.Reset();
		}
	}
}
