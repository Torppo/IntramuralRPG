using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {
		
	public string projectile = null;
	public float projectileDelay = 0;
	public Vector3 projectileOffset = Vector3.zero;
	private float projectileTimer = 0;
	private bool shooting = false;
	private bool canShoot = true;
	
	private Animator anim;
	
	void Awake () {
		anim = GetComponent<Animator>();
	}
	
	public void Shoot(Vector3 target){
		if (!string.IsNullOrEmpty(projectile) && canShoot){
			canShoot = false;
			shooting = true;
			projectileTimer = 0;
		}
		
		if (anim.GetCurrentAnimatorStateInfo(1).nameHash == HashIDs.attackState){			
			if (shooting){
				projectileTimer += Time.deltaTime;
				if (projectileTimer >= projectileDelay){
					shooting = false;
					Vector3 spawnPos = Vector3.zero;
					spawnPos += transform.right * projectileOffset.x;
					spawnPos += transform.up * projectileOffset.y;
					spawnPos += transform.forward * projectileOffset.z;
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
