using UnityEngine;
using System.Collections;

public class ProjectileShooter : MonoBehaviour
{

    private GameObject prefab;
    public Transform spawn;
    private int _shotDistance = 20;

	// Use this for initialization
	void Start ()
	{
	    prefab = Resources.Load("projectile") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {

	    if (Input.GetButton("Shoot"))
	    {
	        var projectile = Instantiate(prefab) as GameObject;
	        projectile.transform.position = spawn.position;
	        var rb = projectile.GetComponent<Rigidbody>();
	        rb.velocity = spawn.transform.forward*5;
	    }
	}
}
