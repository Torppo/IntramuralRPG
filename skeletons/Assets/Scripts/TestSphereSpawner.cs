﻿using UnityEngine;
using System.Collections;

/*
 * Spawns breadcrumbs when X is pressed
 */
public class TestSphereSpawner : MonoBehaviour {

	void Update () {
		if (Input.GetKeyDown(KeyCode.X)){
			InstanceManager.manager.gameObject.GetType();
			InstanceManager.manager.Instantiate("TestSphere", this.transform.position, this.transform.rotation);
		}
	}
}
