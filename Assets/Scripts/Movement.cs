﻿using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{

    int moveSpeed = 8;
    float horiz = 0;
    float vert = 0;
    public bool haveControl = false;

    void FixedUpdate()
    {
        if (haveControl)
        {
            vert = Input.GetAxis("Vertical");
            horiz = Input.GetAxis("Horizontal");
            Vector3 newVelocity = (transform.right * horiz * moveSpeed) + (transform.forward * vert * moveSpeed);
            Vector3 myVelocity = GetComponent<Rigidbody>().velocity;
            myVelocity.x = newVelocity.x;
            myVelocity.z = newVelocity.z;

            if (myVelocity != GetComponent<Rigidbody>().velocity)
            {
                if (Network.isServer)
                {
                    movePlayer(myVelocity);
                }
                else
                {
                    GetComponent<NetworkView>().RPC("movePlayer", RPCMode.Server, myVelocity);
                }
            }
        }
    }

    [RPC]
    void movePlayer(Vector3 playerVelocity)
    {
        GetComponent<Rigidbody>().velocity = playerVelocity;
        GetComponent<NetworkView>().RPC("updatePlayer", RPCMode.OthersBuffered, transform.position);
    }
    [RPC]
    void updatePlayer(Vector3 playerPos)
    {
        transform.position = playerPos;
    }
}