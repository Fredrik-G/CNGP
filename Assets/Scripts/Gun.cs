﻿using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	public Transform spawn;

	public void Shoot() {
       
		Ray ray = new Ray (spawn.position, spawn.forward);
		RaycastHit hit;

		float shotDistance = 20;

		if (Physics.Raycast (ray, out hit)) {
			shotDistance = hit.distance;
		}

		Debug.DrawRay  (ray.origin, ray.direction * shotDistance, Color.red, 1);
	}
}
