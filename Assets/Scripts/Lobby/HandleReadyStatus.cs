using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HandleReadyStatus : MonoBehaviour
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
        Debug.Log("Klick");
        ChangeButtonImage();
        ChangeReadyTextColor();

        _isReady = !_isReady;
    }

    private void ChangeReadyTextColor()
    {
        ReadyText.color = _isReady ? _startColor : _readyColor;
    }

    private void ChangeButtonImage()
    {
        Image.sprite = _isReady ? NotReadySprite : ReadySprite;
    }
}
