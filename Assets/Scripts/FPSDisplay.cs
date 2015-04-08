using UnityEngine;
using System.Collections;
using Engine;

public class FPSDisplay : MonoBehaviour
{
    float deltaTime = 0.0f;

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        var guiStyle = UIFormat.FormatGuiStyle(TextAnchor.UpperLeft, UIFormat.FontSize.Medium, Color.green);
        var rect = new Rect(0, 0, Screen.width, Screen.height * 2 / 100);

        var fps = 1.0f / deltaTime;
        var text = string.Format("{0:0.}fps", fps);
        GUI.Label(rect, text, guiStyle);


    }
}