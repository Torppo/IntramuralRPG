using UnityEngine;
using System.Collections;


/*
 * A script used to spawn projectiles.
 */
public class Shooting : MonoBehaviour {
		
	public string projectile = null;	//the name of the projectile prefab
	public float projectileDelay = 0;	//the delay between starting the attack and spawning the projectile
	public Vector3 projectileOffset = Vector3.zero;	//Where the projectile spawns in relation to the player
	public float targetYOffset = 0.5f;	//How much above the target we should aim (to avoid aiming at their feet)
	
	private float projectileTimer = 0;	//Time since the shot was scheduled
	private bool shooting = false;	//Are we waiting for the projectile delay right now?
	private bool canShoot = true;	//Can we schedule another shot?
	
	private Animator anim;
	
	void Awake () {
		anim = GetComponent<Animator>();
	}
	
	/*
	 * Spawns a projectile after a delay. Needs to be called every frame between triggering the shot and actually firing
	 */
	public void Shoot(Vector3 target){
		//Check if shooting is possible
		if (!string.IsNullOrEmpty(projectile) && canShoot){
			canShoot = false;	//don't allow scheduling another shot
			shooting = true;
			projectileTimer = 0;
		}
		
		//Are we still in attack animation?
		if (anim.GetCurrentAnimatorStateInfo(1).nameHash == HashIDs.attackState){			
			if (shooting){
				projectileTimer += Time.deltaTime;	//Increment timer
				
				//Timer expired, spawn projectile
				if (projectileTimer >= projectileDelay){
					shooting = false;
					//Construct spawn position from offsets
					Vector3 spawnPos = Vector3.zero;
					spawnPos += transform.right * projectileOffset.x;
					spawnPos += transform.up * projectileOffset.y;
					spawnPos += transform.forward * projectileOffset.z;
					//Don't shoot at their feet
					target.y += targetYOffset;
					//spawn projectile
					InstanceManager.manager.Instantiate(projectile, 
						spawnPos + transform.position, 
						Quaternion.LookRotation(-1 * (spawnPos + transform.position - target)));
				}
			}
		}
		//Attack interrupted, cancel shot
		else if (shooting == false) {
			canShoot = true;
		}
	}
	
	/*
	 * Resets the shooting state and shot timer
	 */
	public void Reset(){
		projectileTimer = 0;
		shooting = false;
		canShoot = true;
	}
}
