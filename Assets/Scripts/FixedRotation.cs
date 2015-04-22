using UnityEngine;
using System.Collections;

public class FixedRotation : MonoBehaviour
{

    private Quaternion _quaternion;

    void Start()
    {
        _quaternion = transform.rotation;
    }

    void Update()
    {
        transform.rotation = _quaternion;
    }
}