using UnityEngine;
using System.Collections;
using Engine;

public class MinimapController : MonoBehaviour {

    private Vector3 cameraTarget;
    //private Transform target;
    public Camera camera;
	// Use this for initialization
	void Start () {
        //x = 0.853 y = 0.02 w = 0.134, h 0.235
        camera.rect = new Rect(0.853f, 0.02f, 0.134f, 0.235f);
        camera.transform.position = new Vector3(9.9f,29.9f,19.6f);
	}
	
	// Update is called once per frame
	void LateUpdate () {

        cameraTarget = new Vector3(9.9f, 30f, 19.6f);

        transform.position = Vector3.Lerp(transform.position, cameraTarget, Time.deltaTime * 9999);

        transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
	
	}
    /*public void SetPlayer(Transform target)
    {
        this.target = target;
    }*/
}
