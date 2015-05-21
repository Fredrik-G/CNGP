using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LobbySounds : MonoBehaviour
{
    public enum LobbySoundEffects
    {
        Confirm,
        SelectSkill,
        DeselectSkill,
        Start,
        MouseClick
    }

    public AudioSource LobbyMusic;
    public AudioSource LobbySound;
    public AudioSource LobbyVoice;

    public List<AudioClip> MusicClips;
    public List<AudioClip> WaterVoiceClips;
    public List<AudioClip> EarthVoiceClips;
    public List<AudioClip> FireVoiceClips;
    public List<AudioClip> AirVoiceClips;
    public List<AudioClip> SoundEffects;

    private int _currentSong;

    // Use this for initialization
    void Start()
    {
        var a = PlayerPrefs.GetFloat("MusicLevel");

        MusicClips = new List<AudioClip>
	    {
	        Resources.Load("LobbyMaterials/Music/AvatarState") as AudioClip,
	        Resources.Load("LobbyMaterials/Music/LastAgniKai") as AudioClip,
	        Resources.Load("LobbyMaterials/Music/Reincarnation") as AudioClip
	    };

        SoundEffects = new List<AudioClip>
	    {
	        (Resources.Load("LobbyMaterials/Sounds/confirm") as AudioClip),
	        (Resources.Load("LobbyMaterials/Sounds/selectskill") as AudioClip),
	        (Resources.Load("LobbyMaterials/Sounds/deselectskill") as AudioClip),
	        (Resources.Load("LobbyMaterials/Sounds/start") as AudioClip),
	        (Resources.Load("LobbyMaterials/Sounds/mouseclick") as AudioClip)
	    };

        WaterVoiceClips = new List<AudioClip>
	    {
	        (Resources.Load("LobbyMaterials/Voice/VoiceWater3") as AudioClip),
	        (Resources.Load("LobbyMaterials/Voice/VoiceWater5") as AudioClip),
	        (Resources.Load("LobbyMaterials/Voice/VoiceWater7") as AudioClip),
	        (Resources.Load("LobbyMaterials/Voice/VoiceWater10") as AudioClip),
	        (Resources.Load("LobbyMaterials/Voice/VoiceWater11") as AudioClip)
	    };


        EarthVoiceClips = new List<AudioClip>
	    {
	        (Resources.Load("LobbyMaterials/Voice/VoiceEarth7") as AudioClip),
	        (Resources.Load("LobbyMaterials/Voice/VoiceEarth8") as AudioClip),
	        (Resources.Load("LobbyMaterials/Voice/VoiceEarth9") as AudioClip),
	        (Resources.Load("LobbyMaterials/Voice/VoiceEarth10") as AudioClip)
	    };

        FireVoiceClips = new List<AudioClip>
	    {
	        (Resources.Load("LobbyMaterials/Voice/VoiceFire5") as AudioClip),
	        (Resources.Load("LobbyMaterials/Voice/VoiceFire6") as AudioClip),
	        (Resources.Load("LobbyMaterials/Voice/VoiceFire7") as AudioClip),
	        (Resources.Load("LobbyMaterials/Voice/VoiceFire8") as AudioClip),
	        (Resources.Load("LobbyMaterials/Voice/VoiceFire12") as AudioClip)
	    };

        AirVoiceClips = new List<AudioClip>
	    {
	        (Resources.Load("LobbyMaterials/Voice/VoiceAir1") as AudioClip),
	        (Resources.Load("LobbyMaterials/Voice/VoiceAir8") as AudioClip),
	        (Resources.Load("LobbyMaterials/Voice/VoiceAir11") as AudioClip),
	        (Resources.Load("LobbyMaterials/Voice/VoiceAir12") as AudioClip)
	    };

        if (!LobbyMusic.isPlaying)
        {
            var random = Random.Range(0, 3);
            _currentSong = random;
        }

        SetVolume();
    }

    // Update is called once per frame
    public void Update()
    {
        if (!LobbyMusic.isPlaying)
        {
            LobbyMusic.clip = MusicClips[_currentSong++];
            LobbyMusic.Play();

            if (_currentSong == 3)
            {
                _currentSong = 0;
            }
        }
    }

    private void SetVolume()
    {
        LobbyMusic.volume = PlayerPrefs.GetFloat("MusicLevel");
        LobbySound.volume = PlayerPrefs.GetFloat("SoundLevel");
        LobbyVoice.volume = PlayerPrefs.GetFloat("VoiceLevel");
    }

    public void Confirm()
    {
        LobbySound.clip = SoundEffects[(int)LobbySoundEffects.Confirm];
        LobbySound.Play();
    }

    public void SelectSkill()
    {
        LobbySound.clip = SoundEffects[(int)LobbySoundEffects.SelectSkill];
        LobbySound.Play();
    }
    public void DeselectSkill()
    {
        LobbySound.clip = SoundEffects[(int)LobbySoundEffects.DeselectSkill];
        LobbySound.Play();
    }

    public void StartGame()
    {
        LobbySound.clip = SoundEffects[(int)LobbySoundEffects.Start];
        LobbySound.Play();
    }
    public void SelectCharacter(CharacterSelection.Characters character)
    {
        var random = Random.Range(0, 4);
        switch (character)
        {
            case CharacterSelection.Characters.Waterbending:
                LobbyVoice.clip = WaterVoiceClips[random];
                break;
            case CharacterSelection.Characters.Earthbending:
                LobbyVoice.clip = EarthVoiceClips[random];
                break;
            case CharacterSelection.Characters.Firebending:
                LobbyVoice.clip = FireVoiceClips[random];
                break;
            case CharacterSelection.Characters.Airbending:
                LobbyVoice.clip = AirVoiceClips[random];
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        LobbyVoice.Play();
    }
}
