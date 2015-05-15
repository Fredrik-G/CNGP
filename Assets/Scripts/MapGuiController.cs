using UnityEngine;
using System.Collections;

public class MapGuiController : MonoBehaviour {

    //public Texture2D TopFrame;
    //public Rect TopFramePosition;
    public Texture2D RedteambarTexture2D;
    public Texture2D BlueteambarTexture2D;
    public Texture2D BarFrameTexture2D;
    public Rect RedTeambarPosition,RedFramePosition;
    public Rect BlueTeambarPosition,BlueFramePosition;
    public Rect PointsPosition;
    private MatchController Match;
    public float TeambarMax;
	// Use this for initialization
	void Start ()
	{
	    TeambarMax = 0.48f;
	    //TopFramePosition.Set(0, -0.01f, 0.35f, 0.11f);
        RedTeambarPosition.Set(0.0f, 0.0f, 0.48f, 0.014f);
	    RedFramePosition = RedTeambarPosition;
        BlueTeambarPosition.Set(1f, 0.0f, -0.48f, 0.014f);
	    BlueFramePosition = BlueTeambarPosition;
        PointsPosition.Set(0.24f, -0.005f, 0.1f, 0.1f);
        Match = GetComponentInParent<MatchController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    RedTeambarPosition.Set(0.0f, 0.0f, GetRedbarSize(), 0.014f);
	    BlueTeambarPosition.Set(1f, 0.0f, GetBluebarSize(), 0.014f);
	}

    void OnGUI()
    {
        DrawTopframe();
        DrawTeambars();
        DrawPoints();
    }

    void DrawTopframe()
    {
        //GUI.DrawTexture(new Rect(Screen.width * TopFramePosition.x, Screen.height * TopFramePosition.y, Screen.width * TopFramePosition.width, Screen.height * TopFramePosition.height), TopFrame);
    }

    private float GetRedbarSize()
    {
        double percentage = Match.PointsRedTeam/100;
        return TeambarMax*(float) percentage;
        
    }

    private float GetBluebarSize()
    {
        double percentage = Match.PointsBlueTeam / 100;
        return -(TeambarMax * (float)percentage);
    }

    void DrawTeambars()
    {
        GUI.DrawTexture(new Rect(Screen.width * RedFramePosition.x, Screen.height * RedFramePosition.y, Screen.width * RedFramePosition.width, Screen.height * RedFramePosition.height), BarFrameTexture2D);
        GUI.DrawTexture(new Rect(Screen.width * RedTeambarPosition.x, Screen.height * RedTeambarPosition.y, Screen.width * RedTeambarPosition.width, Screen.height * RedTeambarPosition.height), RedteambarTexture2D);
        GUI.DrawTexture(new Rect(Screen.width * BlueFramePosition.x, Screen.height * BlueFramePosition.y, Screen.width * BlueFramePosition.width, Screen.height * BlueFramePosition.height), BarFrameTexture2D);
        GUI.DrawTexture(new Rect(Screen.width * BlueTeambarPosition.x, Screen.height * BlueTeambarPosition.y, Screen.width * BlueTeambarPosition.width, Screen.height * BlueTeambarPosition.height), BlueteambarTexture2D);
    }

    void DrawPoints()
    {
        GUI.Label(new Rect(Screen.width * PointsPosition.x, Screen.height * PointsPosition.y, Screen.width * PointsPosition.width, Screen.height * PointsPosition.height), ((int)Match.PointsRedTeam).ToString());
        GUI.Label(new Rect(Screen.width * (PointsPosition.x + 0.52f), Screen.height * PointsPosition.y, Screen.width * PointsPosition.width, Screen.height * PointsPosition.height), ((int)Match.PointsBlueTeam).ToString());
    }
}
