using UnityEngine;
using System.Collections;
using Engine;

public class PlayerHealthBarLocation : MonoBehaviour {

    private Transform target;
    private Vector3 HealthAndChiBarTarget;

	// Use this for initialization
    void Start(){
        target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        HealthAndChiBarTarget = new Vector3(target.position.x, transform.position.y, target.position.z+0.5f);
        transform.position = Vector3.Lerp(transform.position, HealthAndChiBarTarget, Time.deltaTime * 9999);
       
	}
}
