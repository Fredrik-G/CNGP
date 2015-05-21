using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MainMenuSounds : MonoBehaviour {

    public AudioSource MainMenuMusic;
    public List<AudioClip> MusicClips;

    private int _currentSong;

	void Start ()
    {
		MusicClips = new List<AudioClip>
	    {
	        Resources.Load("MainScreenMaterials/Music/MainOpeningTheme") as AudioClip,
	        Resources.Load("MainScreenMaterials/Music/SafeReturn") as AudioClip
	    };

        if (!MainMenuMusic.isPlaying)
        {
            var random = Random.Range(0, 3);
            _currentSong = random;
        }
	}
	
	void Update ()
    {
        if (!MainMenuMusic.isPlaying)
	    {
            MainMenuMusic.clip = MusicClips[_currentSong++];
            MainMenuMusic.Play();

            if (_currentSong == 3)
            {
                _currentSong = 0;
            }
	    }
	}

    public void ChangeMusicVolume(Slider slider)
    {
        PlayerPrefs.SetFloat("MusicLevel", slider.value);
        MainMenuMusic.volume = slider.value;
    }
    public void ChangeSoundVolume(Slider slider)
    {
        PlayerPrefs.SetFloat("SoundLevel", slider.value);
    }

    public void ChangeVoiceVolume(Slider slider)
    {
        PlayerPrefs.SetFloat("VoiceLevel", slider.value);
    }
}