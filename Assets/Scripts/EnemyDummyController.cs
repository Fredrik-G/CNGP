using UnityEngine;
using System.Collections;
using Engine;

public class EnemyDummyController : MonoBehaviour {
	private Quaternion targetRotation;
	public float rotationSpeed = 450;
	public float currentwalkSpeed = 5;
	public float maxWalkSpeed = 5;
	public float runSpeed = 8;
	private CharacterController controller;


	// Use this for initialization
	void Start () {

		controller = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {

		ControlWASD ();
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
		motion *= (Input.GetButton ("Run")) ? runSpeed : currentwalkSpeed;
		motion += Vector3.up * -8;
		
		controller.Move (motion * Time.deltaTime);
	}
}
