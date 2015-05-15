using UnityEngine;
using System.Collections;
public class LookAtCamera : MonoBehaviour
{
    public GameObject player;

    void LateUpdate()
    {
        transform.LookAt(player.transform);
    }
}