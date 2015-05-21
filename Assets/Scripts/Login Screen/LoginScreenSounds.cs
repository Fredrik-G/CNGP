using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoginScreenSounds : MonoBehaviour {

    public AudioSource LoginScreenMusic;
    public List<AudioClip> MusicClips;

    private int _currentSong;

	void Start () 
    {
        MusicClips = new List<AudioClip>
	    {
	        Resources.Load("LoginScreenMaterials/Music/ThemeopeningSegel") as AudioClip
	    };

        //if (!LoginScreenMusic.isPlaying)
        //{
        //    var random = Random.Range(0, 3);
        //    _currentSong = random;
        //}
        SetVolume();
        LoginScreenMusic.clip = MusicClips[0];
        LoginScreenMusic.Play();
	}
	
	void Update ()
    {
        if (!LoginScreenMusic.isPlaying)
        {
            LoginScreenMusic.clip = MusicClips[0];
            LoginScreenMusic.Play();
        }
	}

    private void SetVolume()
    {
        LoginScreenMusic.volume = PlayerPrefs.GetFloat("MusicLevel");
    }
}
