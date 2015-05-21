using System;
using Engine;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MainMenuEventHandler : MonoBehaviour
{
    #region Data

    public InputField InputField;
    public string RoomName = string.Empty;
    private readonly NetworkManager _networkManager = new NetworkManager();

    private Vector2 scrollPos = Vector2.zero;
    public Vector2 WidthAndHeight = new Vector2(300, 10);

    private readonly Rect _roomInfoRect = new Rect(Screen.width/2 - Screen.width/6, Screen.height/2 + Screen.height/20,
        Screen.width/3, Screen.height/2);

    private readonly OptionsMenu _optionsMenu = new OptionsMenu();

    public List<GameObject> ResolutionList;
    public List<GameObject> GraphicsQualityList;
    public List<GameObject> SliderList;

    public GameObject StartLobby;
    public GameObject JoinLobby;
    public GameObject Options;

    public GameObject BackgroundButton;

    public GameObject WindowMode;
    public GameObject SelectedResolution;
    public GameObject SelectedGraphics;
    public GameObject ResolutionDropdownArrow;
    public GameObject GraphicDropdownArrow;

    public GameObject MusicVolume;
    public GameObject SoundVolume;
    public GameObject VoiceVolume;
    
    public GameObject Back;

    private enum Menus
    {
        Default,
        StartNewLobby,
        JoinLobby,
        Options
    }

    private Menus _currentMenu = Menus.Default;

    #endregion

    #region Monobehaivor Methods

    public void Start()
    {
        //   Image.enabled = false;
        DisableInputField();
        _networkManager.Start();
    }

    public void OnGUI()
    {
        switch (_currentMenu)
        {
            case Menus.Default:
                return;
            case Menus.StartNewLobby:
                StartNewLobbyGui();
                break;
            case Menus.JoinLobby:
                JoinLobbyGui();
                break;
            case Menus.Options:
                //Options.OptionsGui();
                break;
        }
    }

    #endregion

    private void StartNewLobbyGui()
    {
        if (InputField.isFocused && !String.IsNullOrEmpty(InputField.text) && Input.GetKey(KeyCode.Return))
        {
            Debug.Log("Starting new lobby with name " + InputField.text);
            _networkManager.CreateNewLobby(InputField.text);
        }
    }

    private void JoinLobbyGui()
    {
        DisplayAllRooms();
    }

    #region Mouse Hover

    public void HandleStartNewLobbyHover()
    {
        GameObject.FindGameObjectWithTag("StartNewLobbyImage").GetComponent<Image>().enabled = true;
    }

    public void HandleJoinLobbyHover()
    {
        GameObject.FindGameObjectWithTag("JoinLobbyImage").GetComponent<Image>().enabled = true;
    }

    public void HandleOptionsHover()
    {
        GameObject.FindGameObjectWithTag("OptionsImage").GetComponent<Image>().enabled = true;
    }

    public void HandleMouseLeave()
    {
        GameObject.FindGameObjectWithTag("StartNewLobbyImage").GetComponent<Image>().enabled = false;
        GameObject.FindGameObjectWithTag("JoinLobbyImage").GetComponent<Image>().enabled = false;
        GameObject.FindGameObjectWithTag("OptionsImage").GetComponent<Image>().enabled = false;
    }

    #endregion

    #region Mouse Clicks

    public void HandleStartNewLobbyClick()
    {
        if (!String.IsNullOrEmpty(InputField.text))
        {
            Debug.Log("Starting new lobby with name " + InputField.text);
            _networkManager.CreateNewLobby(InputField.text);
        }
        else
        {
            ResetGui();
            EnableInputField();
            _currentMenu = Menus.StartNewLobby;
        }
    }

    public void HandleJoinLobbyClick()
    {
        ResetGui();
        _currentMenu = Menus.JoinLobby;
    }

    public void HandleOptionsClick()
    {
        ResetGui();
        _currentMenu = Menus.Options;

        DisplayOptionsMenu();
    }

    public void HandleBackgroundButtonClick()
    {
        DisplayOptionsMenu();
    }

    public void HandleBackClick()
    {
        _currentMenu = Menus.Default;
        DisplayDefaultMenu();
    }

    public void HandleSelectedResolutionClick()
    {
        SelectedResolution.SetActive(false);
        SelectedGraphics.SetActive(false);

        DisableAllGraphics();
        EnableAllResolutions();
        //DisableAllSliders();
    }

    public void HandleSelectedGraphicsClick()
    {
        SelectedGraphics.SetActive(false);

        EnableAllGraphics();
       // DisableAllSliders();
    }

    public void HandleSelectResolutionClick(RectTransform rectButton)
    {
        _optionsMenu.SetResolution(rectButton.GetComponentInChildren<Text>().text);
    }

    public void HandleToggleWindowModeClick()
    {
        _optionsMenu.ToggleWindowMode();
    }

    #endregion

    #region Display Methods

    private void DisplayOptionsMenu()
    {
        StartLobby.SetActive(false);
        JoinLobby.SetActive(false);
        Options.SetActive(false);

        BackgroundButton.SetActive(true);
        ResolutionDropdownArrow.SetActive(true);
        GraphicDropdownArrow.SetActive(true);
        SelectedResolution.SetActive(true);
        SelectedGraphics.SetActive(true);
        WindowMode.SetActive(true);

        VoiceVolume.SetActive(true);
        MusicVolume.SetActive(true);
        SoundVolume.SetActive(true);

        Back.SetActive(true);

        AssignCurrentGraphicSettings();
        AssignCurrentVolumeSliders();

        EnableAllSliders();
        DisableAllResolutions();
        DisableAllGraphics();
    }

    private void DisplayDefaultMenu()
    {
        DisableAllResolutions();
        DisableAllGraphics();
        DisableAllSliders();

        StartLobby.SetActive(true);
        JoinLobby.SetActive(true);
        Options.SetActive(true);

        ResolutionDropdownArrow.SetActive(false);
        GraphicDropdownArrow.SetActive(false);
        BackgroundButton.SetActive(false);
        SelectedResolution.SetActive(false);
        SelectedGraphics.SetActive(false);
        WindowMode.SetActive(false);
        
        MusicVolume.SetActive(false);
        SoundVolume.SetActive(true);
        VoiceVolume.SetActive(false);

        Back.SetActive(false);
    }

    private void DisplayAllRooms()
    {
        GUI.Box(_roomInfoRect, "Join Lobby");
        GUILayout.BeginArea(_roomInfoRect);

        GUILayout.Space(15);
        if (PhotonNetwork.GetRoomList().Length == 0)
        {
            GUILayout.Label("Currently no games are available.");
            GUILayout.Label("Rooms will be listed here when they become available.");
        }
        else
        {
            GUILayout.Label(PhotonNetwork.GetRoomList().Length + " rooms available:");

            scrollPos = GUILayout.BeginScrollView(scrollPos);
            foreach (var roomInfo in PhotonNetwork.GetRoomList())
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(roomInfo.name + " " + roomInfo.playerCount + "/" + roomInfo.maxPlayers);
                if (GUILayout.Button("Join", GUILayout.Width(150)))
                {
                    PhotonNetwork.JoinRoom(roomInfo.name);
                }

                GUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();
        }
        GUILayout.EndArea();
    }

    #endregion

    private void AssignCurrentGraphicSettings()
    {
        SelectedResolution.GetComponentInChildren<Text>().text = _optionsMenu.GetCurrentResolution();
        SelectedGraphics.GetComponentInChildren<Text>().text = _optionsMenu.GetCurrentGraphicsQuality();
        WindowMode.GetComponentInChildren<Toggle>().isOn = _optionsMenu.IsWindowMode();
    }

    private void AssignCurrentVolumeSliders()
    {
        MusicVolume.GetComponentInChildren<Slider>().value = PlayerPrefs.GetFloat("MusicLevel");
        SoundVolume.GetComponentInChildren<Slider>().value = PlayerPrefs.GetFloat("SoundLevel");
        VoiceVolume.GetComponentInChildren<Slider>().value = PlayerPrefs.GetFloat("VoiceLevel");
    }

    #region Enable/Disable InputField

    private void EnableInputField()
    {
        InputField.enabled = true;
        InputField.GetComponent<Image>().enabled = true;
        InputField.GetComponent<InputField>().enabled = true;

        InputField.GetComponentInChildren<Text>().enabled = true;
    }

    private void DisableInputField()
    {
        InputField.enabled = false;
        InputField.GetComponent<Image>().enabled = false;
        InputField.GetComponent<InputField>().enabled = false;

        InputField.GetComponentInChildren<Text>().enabled = false;
    }

    #endregion

    private void ResetGui()
    {
        DisableInputField();
        InputField.text = String.Empty;
    }

    #region Enable/Disable Buttons and Sliders

    private static void EnableButton(GameObject button)
    {
        button.GetComponent<Image>().enabled = true;
        button.GetComponent<Button>().enabled = true;
        button.GetComponentInChildren<Text>().enabled = true;
    }

    private static void DisableButton(GameObject button)
    {         
        button.GetComponent<Image>().enabled = false;
        button.GetComponent<Button>().enabled = false;
        button.GetComponentInChildren<Text>().enabled = false;
    }

    private static void EnableSlider(GameObject slider)
    {
        slider.GetComponentInChildren<Image>().enabled = true;
        var sliderGameObjects = slider.GetComponentsInChildren<Transform>();

        foreach (var sliderGameObject in sliderGameObjects)
        {
            sliderGameObject.GetComponentInChildren<Image>().enabled = true;
        }
    }

    private static void DisableSlider(GameObject slider)
    {
        slider.GetComponentInChildren<Image>().enabled = false;
        var sliderGameObjects = slider.GetComponentsInChildren<Transform>();

        foreach (var sliderGameObject in sliderGameObjects)
        {
            sliderGameObject.GetComponentInChildren<Image>().enabled = false;
        }
    }

    private void EnableAllResolutions()
    {
        foreach (var resolutionButton in ResolutionList)
        {
            EnableButton(resolutionButton);
        }
    }

    private void DisableAllResolutions()
    {
        foreach (var resolutionButton in ResolutionList)
        {
            resolutionButton.SetActive(false);
           // DisableButton(resolutionButton);
        }
    }

    private void EnableAllGraphics()
    {
        foreach (var graphicButton in GraphicsQualityList)
        {
            EnableButton(graphicButton);
        }
    }

    private void DisableAllGraphics()
    {
        foreach (var graphicButton in GraphicsQualityList)
        {
            graphicButton.SetActive(false);
           // DisableButton(graphicButton);
        }
    }

    private void EnableAllSliders()
    {
        foreach (var volumeSlider in SliderList)
        {
            EnableSlider(volumeSlider);
        }
    }

    private void DisableAllSliders()
    {
        foreach (var volumeSlider in SliderList)
        {
            DisableSlider(volumeSlider);
        }
    }

    #endregion
}
