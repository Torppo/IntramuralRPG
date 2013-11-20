using UnityEngine;
using System.Collections;

public class SimpleMove : MonoBehaviour {
	
	public float speed = 1.0f;
	
	
	// Update is called once per frame
	void FixedUpdate () {
		rigidbody.velocity = transform.forward * speed;
	}
}
