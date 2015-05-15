using UnityEngine;
using System.Collections;

public class Sync : Photon.MonoBehaviour {
    Vector3 trueLoc;
    Quaternion trueRot;
    PhotonView pv;
    private Vector3 latestCorrectPos;
    private Vector3 onUpdatePos;
    private float fraction;

	// Use this for initialization
	void Start () {
        pv = GetComponent<PhotonView>();
        if (pv.GetComponent<PhotonView>().isMine)
        {
            enabled = false;
        }
	}

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //we are reicieving data
        if (stream.isReading)
        {
            //receive the next data from the stream and set it to the truLoc varible
         
            //do we own this photonView?????
                this.trueLoc = (Vector3)stream.ReceiveNext(); //the stream send data types of "object" we must typecast the data into a Vector3 format

        }
        //we need to send our data
        else
        {
            //send our posistion in the data stream

                stream.SendNext(transform.FindChild("Ethan").position);

        }
    }
    void Update()
    {

         transform.FindChild("Ethan").position = Vector3.Lerp(transform.FindChild("Ethan").position, trueLoc, Time.deltaTime * 5);
            transform.FindChild("Ethan").rotation = Quaternion.Lerp(transform.FindChild("Ethan").rotation, trueRot, Time.deltaTime * 5);

    }
}
