using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	
	public float moveSpeed = 10f;
	public float turnSpeed = 100f;
	
	private Animator anim;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	void Update () {
		float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
		Move(h,v);
	}
	
	void Move(float h, float v){
		
		CharacterController cc = this.GetComponent<CharacterController>();
		Vector3 moveDirection;
		moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
		transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime);
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= moveSpeed;
		moveDirection *= Time.deltaTime;
		
		cc.Move(moveDirection);
		
		
		Animate(v, Input.GetKeyDown(KeyCode.Space));
	}
	
	void Animate (float v, bool striking){
		anim.SetFloat("speed", v);
		
		anim.SetBool("strike", striking);
	}
}
