using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VoiceController : MonoBehaviour {

    private AudioSource _audioSource;
    public List<AudioClip> audioClips = new List<AudioClip>();
    private double CooldownTimer;
    private double ChiCostTimer;
    private double MovementTimer;
    private double NormalTimer;
    private double OverallTimer;
    private PlayerStats playerStats;
	// Use this for initialization
	void Start () {
        CooldownTimer = 7;
        ChiCostTimer = 7;
        MovementTimer = 7;
        NormalTimer = 15;
        OverallTimer = 1;
        playerStats = GetComponent<PlayerStats>();
        _audioSource = GetComponent<AudioSource>();
        if(playerStats.elementClass.Equals("Air"))
        {
            for (int i = 1; i < 15; ++i)
            {
                audioClips.Add(Resources.Load("SoundEffects/VoiceAir" + i) as AudioClip);
            }
        }
        else if(playerStats.elementClass.Equals("Earth"))
        {
            for (int i = 1; i < 13; ++i)
            {
                audioClips.Add(Resources.Load("SoundEffects/VoiceEarth" + i) as AudioClip);
            }
        }
        Debug.Log("Ljud: " + audioClips.Count);
	}
	
	// Update is called once per frame
	void Update () {
	    if(CooldownTimer > 0)
        {
            CooldownTimer -= Time.deltaTime;
        }
        if (ChiCostTimer > 0)
        {
            ChiCostTimer -= Time.deltaTime;
        }
        if (NormalTimer > 0)
        {
            NormalTimer -= Time.deltaTime;
        }
        if(MovementTimer > 0)
        {
            MovementTimer -= Time.deltaTime;
        }
        if (OverallTimer > 0)
        {
            OverallTimer -= Time.deltaTime;
        }
        PlayNormalVoice();
	}

    public void PlayCooldownVoice()
    {
        if (CooldownTimer <= 0 && OverallTimer <= 0 && audioClips.Count > 2)
        {
            int RandomVoice = Random.Range(0, 2);
            _audioSource.PlayOneShot(audioClips[RandomVoice]);
            CooldownTimer = 3;
            OverallTimer = 2.5f;
        }
    }
    public void PlayCantgoVoice()
    {
        if (MovementTimer <= 0 && OverallTimer <= 0 && audioClips.Count > 4)
        {
            int RandomVoice = Random.Range(2, 4);
            _audioSource.PlayOneShot(audioClips[RandomVoice]);
            MovementTimer = 6;
            OverallTimer = 3;
        }
    }
    public void PlayChiCostVoice()
    {
        if (ChiCostTimer <= 0 && OverallTimer <= 0 && audioClips.Count > 6)
        {
            int RandomVoice = Random.Range(4, 6);
            _audioSource.PlayOneShot(audioClips[RandomVoice]);
            ChiCostTimer = 3;
            OverallTimer = 2.5f;
        }
    }
    public void PlayNormalVoice()
    {
        if (NormalTimer <= 0 && OverallTimer <= 0)
        {
            int RandomVoice = Random.Range(6, audioClips.Count);
            _audioSource.PlayOneShot(audioClips[RandomVoice]);
            NormalTimer = 15;
            OverallTimer = 4;
        }
    }
}
