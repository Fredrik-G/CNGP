﻿using UnityEngine;
using System.Collections;
using Engine;

public class EnemyDummyController : MonoBehaviour {
	private Quaternion targetRotation;
	public float rotationSpeed = 450;
	public float maxWalkSpeed = 5;
	public float currentWalkSpeed = 2;
	public float runSpeed = 8;
	private CharacterController controller;
	
	
	// Use this for initialization
	void Start () {
		
		controller = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {

	    if (!GetComponent<EnemyStats>().Stunned)
	    {
	        ControlWASD();
	    }
	}
	
	void ControlWASD()
	{
		Vector3 input = new Vector3 (Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		
		if (input != Vector3.zero) {
			targetRotation = Quaternion.LookRotation (input);
			transform.eulerAngles = Vector2.up * Mathf.MoveTowardsAngle (transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);
		}

		Vector3 motion = input;
		motion *= (Mathf.Abs (input.x) == 1 && Mathf.Abs (input.z) == 1) ? .7f : 1;
		motion *= (float)(currentWalkSpeed * (GetComponent<EnemyStats> ().stats.Movementspeed / 100) * GetComponent<EnemyStats> ().stats.MovementSpeedFactor);
		motion += Vector3.up * -8;

	    if (!GetComponent<EnemyStats>().Rooted)
	    {
	        controller.Move(motion*Time.deltaTime);
	    }
	}


}
