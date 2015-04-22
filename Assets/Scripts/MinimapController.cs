using UnityEngine;
using System.Collections;
using Engine;

public class MinimapController : MonoBehaviour {

    private Vector3 cameraTarget;
    public Transform target;
	// Use this for initialization
	void Start () {
        //x = 0.853 y = 0.02 w = 0.134, h 0.235
	}
	
	// Update is called once per frame
	void LateUpdate () {

        cameraTarget = new Vector3(0, 30f, 0);

        transform.position = Vector3.Lerp(transform.position, cameraTarget, Time.deltaTime * 9999);

        transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
	
	}
}
