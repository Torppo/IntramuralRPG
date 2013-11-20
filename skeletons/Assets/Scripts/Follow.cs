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
	
	public string projectile = null;
	public float projectileDelay = 0;
	public Vector3 projectileOffset = Vector3.zero;
	private float projectileTimer = 0;
	private bool shooting = false;
	private bool canShoot = true;
	
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
		else {
			anim.SetFloat(HashIDs.movementSpeedFloat, 0f);
			anim.SetBool(HashIDs.strikeAnimBool, true);
			
			if (!string.IsNullOrEmpty(projectile) && canShoot){
				canShoot = false;
				shooting = true;
				projectileTimer = 0;
			}
		}
		
		if (anim.GetCurrentAnimatorStateInfo(1).nameHash == HashIDs.attackState){
			anim.SetBool(HashIDs.strikeAnimBool, false);
			
			if (shooting){
				projectileTimer += Time.deltaTime;
				if (projectileTimer >= projectileDelay){
					shooting = false;
					Vector3 spawnPos = Vector3.zero;
					spawnPos += transform.right * projectileOffset.x;
					spawnPos += transform.up * projectileOffset.y;
					spawnPos += transform.forward * projectileOffset.z;
					Vector3 target = followTarget.transform.position;
					target.y += projectileOffset.y;
					InstanceManager.manager.Instantiate(projectile, 
						spawnPos + transform.position, 
						Quaternion.LookRotation(-1 * (spawnPos + transform.position - target)));
				}
			}
		}
		else if (shooting == false) {
			canShoot = true;
		}
	}
	
}
