using UnityEngine;
using System.Collections;

public class FixedRotation : MonoBehaviour {

    Quaternion _rotation;
    void Awake()
    {
        _rotation = transform.rotation;

    }
    void LateUpdate()
    {
        transform.rotation = _rotation;
    }
}
