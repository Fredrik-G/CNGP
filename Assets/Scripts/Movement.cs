﻿using UnityEngine;

public class Movement : MonoBehaviour
{
    public int MoveSpeed = 8;
    private float _horizontal = 0;
    private float _vertical = 0;
    public bool HaveControl = false;
 
    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// FixedUpdate should be used instead of Update when dealing with Rigidbody.
    /// For example when adding a force to a rigidbody, you have to apply the force every fixed frame
    /// inside FixedUpdate instead of every frame inside Update.
    /// </summary>
    void FixedUpdate()
    {
        if (!HaveControl) return;

        _vertical = Input.GetAxis("Vertical");
        _horizontal = Input.GetAxis("Horizontal");

        var newVelocity = (transform.right * _horizontal * MoveSpeed) + (transform.forward * _vertical * MoveSpeed);
        var myVelocity = GetComponent<Rigidbody>().velocity;
        myVelocity.x = newVelocity.x;
        myVelocity.z = newVelocity.z;

        //Checks if the velocity is different and needs to be updated.
        if (myVelocity != GetComponent<Rigidbody>().velocity)
        {
            if (Network.isServer)
            {
                MovePlayer(myVelocity);
            }
            else
            {
                GetComponent<NetworkView>().RPC("MovePlayer", RPCMode.Server, myVelocity);
            }
        }
    }

    [RPC]
    void MovePlayer(Vector3 playerVelocity)
    {
        GetComponent<Rigidbody>().velocity = playerVelocity;
        GetComponent<NetworkView>().RPC("UpdatePlayer", RPCMode.OthersBuffered, transform.position);
    }
    [RPC]
    void UpdatePlayer(Vector3 playerPos)
    {
        transform.position = playerPos;
    }
}