using UnityEngine;
using System.Collections;

public class ChangeColor : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Trigger enter");
        GetComponent<NetworkView>().RPC("ChangeColorToRed", RPCMode.AllBuffered);
    }

    void OnTriggerExit(Collider collider)
    {
        Debug.Log("Trigger exit");
        GetComponent<NetworkView>().RPC("ChangeColorToGreen", RPCMode.AllBuffered);       
    }

    [RPC]
    private void ChangeColorToRed()
    {
        GetComponent<Renderer>().material.SetColor("_Color", Color.red);
    }
    [RPC]
    private void ChangeColorToGreen()
    {
        GetComponent<Renderer>().material.SetColor("_Color", Color.green);
    }
}
