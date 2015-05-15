using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicController : MonoBehaviour
{

    public List<AudioClip> musicClips = new List<AudioClip>();
    public AudioSource audio;
    // Use this for initialization
    void Start()
    {

        musicClips.Add(Resources.Load("Music/AgniKai") as AudioClip);
        musicClips.Add(Resources.Load("Music/Ending") as AudioClip);
        musicClips.Add(Resources.Load("Music/FinalBlow") as AudioClip);
        musicClips.Add(Resources.Load("Music/LionTurtle") as AudioClip);

        audio = GetComponent<AudioSource>();
        if (!audio.playOnAwake)
        {
            audio.clip = musicClips[Random.Range(0, musicClips.Count)];
            audio.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!audio.isPlaying)
        {
            audio.clip = musicClips[Random.Range(0, musicClips.Count)];
            audio.Play();
        }
    }
}
