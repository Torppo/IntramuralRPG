using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {
	
	public GameObject followTarget;
	
	public float moveSpeed = 2f;
	public float turnSpeed = 10f;
	public float stopDistance = 1f;
	
	private Animator anim;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		CharacterController cc = this.GetComponent<CharacterController>();
		
		if (followTarget == null){
			anim.SetFloat("speed", 0f);
			return;
		}
		
		Vector3 targetDir =  -1 *(transform.position - followTarget.transform.position);
		float step = turnSpeed * Time.deltaTime;
    	Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
		transform.rotation = Quaternion.LookRotation(newDir);
		
		if ((transform.position - followTarget.transform.position).magnitude > stopDistance){
			Vector3 moveDirection = transform.forward;
			moveDirection *= moveSpeed;
			moveDirection *= Time.deltaTime;
			
			cc.Move(moveDirection);
			
			anim.SetFloat("speed", 1f);
			
			
			
		}
		else {
			anim.SetFloat("speed", 0f);
		}
	}
	
}
