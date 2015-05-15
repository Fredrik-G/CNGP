using UnityEngine;
using System.Collections;

public class CabbageManController : MonoBehaviour
{

    public int State;
    private MatchController Match;
    public float Deathtimer = 0;
    protected Animator CabbagemanAnimation;
    private AudioSource _audioSource;
    // Use this for initialization
    void Start()
    {
        PhotonNetwork.sendRate = 30;
        PhotonNetwork.sendRateOnSerialize = 30;
        State = 0;
        Match = GameObject.Find("Terrain").GetComponent<MatchController>();
        CabbagemanAnimation = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        Respawncabbageman();

    }

    // Update is called once per frame
    void Update()
    {
        if (State == 4)
        {
            CabbagemanAnimation.SetBool("Alive", false);
            if (Deathtimer <= 0)
            {
                Match.CabbageManTimer = 3;
                PhotonNetwork.Destroy(this.gameObject);
            }
            else
            {
                Deathtimer = Deathtimer - Time.deltaTime;
            }
        }
        else
        {
            CabbagemanAnimation.SetBool("Alive", true);
        }
        MoveCabbageman();
    }


    void OnTriggerEnter(Collider Other)
    {
        if (Other.gameObject.CompareTag("Waypoint"))
        {
            Nextstate();
        }
        else if (Other.gameObject.CompareTag("Player") && State != 4)
        {
            Killcabbageman();
            if (Other.gameObject.GetComponent<PlayerStats>().teamID == 0)
            {
                Match.IncreasePointsCabbageKill(0);
            }
            else
            {
                Match.IncreasePointsCabbageKill(1);
            }
        }
    }
    void Nextstate()
    {
        if (State >= 3)
        {
            State = 0;
        }
        else
        {
            State++;
        }
    }
    void Killcabbageman()
    {

        State = 4;
        Deathtimer = 5;
        int randomsound = Random.Range(0, 2);
        if (randomsound == 0)
        {
            _audioSource.PlayOneShot(Resources.Load("SoundEffects/Mycabbages1") as AudioClip);
        }
        else
        {
            _audioSource.PlayOneShot(Resources.Load("SoundEffects/Mycabbages2") as AudioClip);
        }
    }
    void Respawncabbageman()
    {

        int spawn = Random.Range(0, 4);
        if (spawn == 0)
        {
            transform.position = GameObject.Find("Waypoint3").transform.position + new Vector3(5, 0, 0);

        }
        else if (spawn == 1)
        {
            transform.position = GameObject.Find("Waypoint0").transform.position + new Vector3(0, 0, 5);

        }
        else if (spawn == 2)
        {
            transform.position = GameObject.Find("Waypoint1").transform.position + new Vector3(-5, 0, 0);

        }
        else if (spawn == 3)
        {
            transform.position = GameObject.Find("Waypoint2").transform.position + new Vector3(0, 0, -5);

        }
        State = spawn;
    }
    void MoveCabbageman()
    {

        if (State == 0)
        {
            var lookPos = GameObject.Find("Waypoint0").transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);
            transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("Waypoint0").transform.position, 1.6f * Time.deltaTime);

        }
        else if (State == 1)
        {
            var lookPos = GameObject.Find("Waypoint1").transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);
            transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("Waypoint1").transform.position, 1.6f * Time.deltaTime);

        }
        else if (State == 2)
        {
            var lookPos = GameObject.Find("Waypoint2").transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);
            transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("Waypoint2").transform.position, 1.6f * Time.deltaTime);

        }
        else if (State == 3)
        {
            var lookPos = GameObject.Find("Waypoint3").transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);
            transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("Waypoint3").transform.position, 1.6f * Time.deltaTime);

        }
    }
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(Deathtimer);
            stream.SendNext(State);
        }
        else
        {
            Deathtimer = (float)stream.ReceiveNext();
            State = (int)stream.ReceiveNext();

        }
    }
}
