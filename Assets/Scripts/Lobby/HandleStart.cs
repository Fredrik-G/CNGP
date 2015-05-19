using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class HandleStart : MonoBehaviour
{
    public Sprite ReadySprite;
    public Sprite NotReadySprite;
    public Image Image;
    public Text ReadyText;

    private bool _isReady = false;

    /// <summary>
    /// Color used for not ready.
    /// </summary>
    private Color _startColor;

    /// <summary>
    /// Color used for ready.
    /// </summary>
    private Color _readyColor;

    public void Start()
    {
        _startColor = ReadyText.color;
        _readyColor = new Color(255f, 255f, 255f, 254f);
    }

    public void HandleMouseClick()
    {
		Debug.Log ("ON HandleMouseClick");
        if (PhotonNetwork.isMasterClient) {
			GetComponent<PhotonView>().RPC("StartGame", PhotonTargets.All);
			Debug.Log("IsMasterClient");
		}
        /*Debug.Log("Klick");
        ChangeButtonImage();
        ChangeReadyTextColor();

        _isReady = !_isReady;*/
    }

    public void ChangeReadyTextColor()
    {
        ReadyText.color = _isReady ? _startColor : _readyColor;
    }

    public void ChangeButtonImage()
    {
        Image.sprite = _isReady ? NotReadySprite : ReadySprite;
        
    }
   
    [RPC]
    public void StartGame()
    {    
            Application.LoadLevel("De_dust");
    }
   
}