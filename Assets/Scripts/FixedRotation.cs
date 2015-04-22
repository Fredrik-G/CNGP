using UnityEngine;
using System.Collections;

public class FixedRotation : MonoBehaviour {
    private Quaternion _Quat;
	// Use this for initialization
	void Start () {
        _Quat = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = _Quat;
	}
}
