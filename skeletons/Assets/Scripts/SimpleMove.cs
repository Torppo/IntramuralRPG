using UnityEngine;
using System.Collections;

/*
 * Moves a rigidbody object in a straight line
 */
public class SimpleMove : MonoBehaviour {
	
	public float speed = 1.0f;	//Movement speed
	
	
	// Update is called once per frame
	void FixedUpdate () {
		rigidbody.velocity = transform.forward * speed;
	}
}
