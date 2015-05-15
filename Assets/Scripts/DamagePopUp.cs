using UnityEngine;
using System.Collections;

public class DamagePopUp : MonoBehaviour {

    private Vector3 _position;
    private Vector3 _screenPointPosition;
    private Camera _cameraHold;
    private string _text;
    private GUIStyle style;
	// Use this for initialization
	void Start () {
        style = new GUIStyle();
        style.fontSize = 5;
        style.fontStyle = FontStyle.Normal;
        style.normal.textColor = Color.black;
        _cameraHold = Camera.main;
        _screenPointPosition = _cameraHold.WorldToScreenPoint(_position);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public static void ShowMessage(string text, Vector3 position)
    {
        var newInstance = new GameObject("DamagePopUp");
        var damagePopUp = newInstance.AddComponent<DamagePopUp>();
        damagePopUp._position = position;
        damagePopUp._text = text;

    }
    void ONGui()
    {
        GUI.Label(new Rect(_screenPointPosition.x, _screenPointPosition.y, 100, 20), _text, style);
        Destroy(this.gameObject, 1);
    }
}
