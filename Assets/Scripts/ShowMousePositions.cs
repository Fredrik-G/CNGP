using UnityEngine;
using System.Collections;

public class ShowMousePositions : MonoBehaviour {


    public  Vector3 ray = new Vector3();
    public Vector3 inputPosition = new Vector3();
    private GameObject player;
    /// <summary>
    /// Variabler för att hålla vilket håll ethan är påväg
    /// </summary>
    public float v; 
    public float h; 

    private float rotationValue; // Håller information om var ethan tittar, uttryckt i grader
 


    public Animator anim = new Animator();
    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }
    void start() 
    {        

        anim = GetComponent<Animator>();
    }

	void Update ()
    {

            rotationValue = player.transform.eulerAngles.y;
            v = Input.GetAxis("Vertical");
            h = Input.GetAxis("Horizontal");


            if (rotationValue < 45.0f || rotationValue > 315) //första kvadranten...
            {
                rotationValue = 1.0f;
            }
            if (rotationValue > 45 && rotationValue < 135)
            {
                rotationValue = 2.0f;
            }
            if (rotationValue > 135 && rotationValue < 225)
            {
                rotationValue = 3.0f;
            }
            if (rotationValue > 225 && rotationValue < 315)
            {
                rotationValue = 4.0f;
            }

            anim.SetFloat("Quadrant", rotationValue);
            anim.SetFloat("v", v);
            anim.SetFloat("h", h);

       
	}
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(v);
            stream.SendNext(h);
            stream.SendNext(rotationValue);
        }
        else
        {
            v = (float)stream.ReceiveNext();
            h =  (float)stream.ReceiveNext();
            rotationValue = (float)stream.ReceiveNext();
            anim.SetFloat("Quadrant",rotationValue);
            anim.SetFloat("v", v);
            anim.SetFloat("h", h);
        }
    }
}
