using UnityEngine;
using System.Collections;

public class FPSCounter : MonoBehaviour
{
	float deltaTime = 0.0f;
	private GUIStyle style = new GUIStyle();
	private Rect rect;
	private string text;
	void Start()
	{
		int w = Screen.width;
		int h = Screen.height;
		rect = new Rect(Screen.width*0.003f, Screen.height*0.035f, 0.03f, 0.025f);
		//0.006f, 0.865f, 0.03f, 0.025f
		style.alignment = TextAnchor.LowerLeft;
		style.fontSize = h * 2 / 100;
		style.normal.textColor = Color.black;
	}
	void Update()
	{
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
	}
	
	void OnGUI()
	{
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		if (fps < 10)
			style.normal.textColor = Color.red;
		else if (fps < 30)
			style.normal.textColor = Color.yellow;
		else
			style.normal.textColor = Color.green;
		text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);//{0:0.0} ms ,mse        //text = fps+" fps";
		GUI.Label(rect, text, style);
	}
}