using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

	private Vector3 cameraTarget;
	public Transform target;
	
	void Start () 
	{
	}

	void LateUpdate () 
	{
        cameraTarget = new Vector3(target.position.x, transform.position.y, target.position.z - 4);
		
		transform.position = Vector3.Lerp (transform.position, cameraTarget, Time.deltaTime*10000);

        transform.rotation = Quaternion.Euler(new Vector3(70, 0, 0));
	}
}






















