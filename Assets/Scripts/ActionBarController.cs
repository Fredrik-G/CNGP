using UnityEngine;
using System.Collections;

public class ActionBarController : MonoBehaviour {

    public Texture2D actionBar;
    public Texture2D minimap;
    public Texture2D healthBar;
    public Texture2D chiBar;
    public Rect actionBarPosition;
    public Rect minimapPosition;
    public Rect healthBarPosition;
    public Rect chiPosition;
    public Rect SkillPosition;
    public float BarMax;
    private PlayerStats playerStats;
	// Use this for initialization
	void Start () {
        BarMax = 0.292f;
        actionBarPosition.Set(0.3F, 0.88F, 0.4F, 0.12F);
        minimapPosition.Set(0.3F, 0.88F, 0.4F, 0.12F);
        healthBarPosition.Set(0.354f, 0.893f, BarMax, 0.008f);
        chiPosition.Set(0.354f, 0.907f, BarMax, 0.008f);
        SkillPosition.Set(0.409f,0.933f,0.034f,0.06f);
        playerStats = GetComponentInParent<PlayerStats>();
        
	}
	
	// Update is called once per frame
	void Update () {
        healthBarPosition.Set(0.354f, 0.893f, GetHealthbarSize(), 0.008f);
        chiPosition.Set(0.354f, 0.907f, GetChibarSize(), 0.008f);
	}

    private float GetHealthbarSize()
    {
        
        double Percentage = playerStats.stats.CurrentHealthpoints / playerStats.stats.MaxHealthpoints;
        return BarMax * (float)Percentage;
    }

    private float GetChibarSize()
    {
        double Percentage = playerStats.stats.CurrentChi / playerStats.stats.MaxChi;
        return BarMax * (float)Percentage;
    }

    void OnGUI()
    {
        DrawActionBar();
        DrawMinimap();
        DrawHealthBar();
        DrawChiBar();
        DrawSkills();
        DrawCooldownSkills();
    }
    void DrawActionBar()
    {
        GUI.DrawTexture(new Rect(Screen.width * actionBarPosition.x, Screen.height * actionBarPosition.y, Screen.width * actionBarPosition.width, Screen.height * actionBarPosition.height), actionBar);
    }
    void DrawMinimap()
    {
        GUI.DrawTexture(new Rect(Screen.width * actionBarPosition.x, Screen.height * actionBarPosition.y, Screen.width * actionBarPosition.width, Screen.height * actionBarPosition.height), actionBar);
    }
    void DrawHealthBar()
    {
        GUI.DrawTexture(new Rect(Screen.width * healthBarPosition.x, Screen.height * healthBarPosition.y, Screen.width * healthBarPosition.width, Screen.height * healthBarPosition.height), healthBar);
    }
    void DrawChiBar()
    {
        GUI.DrawTexture(new Rect(Screen.width * chiPosition.x, Screen.height * chiPosition.y, Screen.width * chiPosition.width, Screen.height * chiPosition.height), chiBar);
    }
    void DrawSkills()
    {
        GUI.DrawTexture(new Rect(Screen.width * SkillPosition.x, Screen.height * SkillPosition.y, Screen.width * SkillPosition.width, Screen.height * SkillPosition.height), (Resources.Load("GUI Texture/Spell" + playerStats.stats.SkillList[0].Name)as Texture2D));
        GUI.DrawTexture(new Rect(Screen.width * (SkillPosition.x + 0.0485f), Screen.height * SkillPosition.y, Screen.width * SkillPosition.width, Screen.height * SkillPosition.height), (Resources.Load("GUI Texture/Spell" + playerStats.stats.SkillList[1].Name) as Texture2D));
        GUI.DrawTexture(new Rect(Screen.width * (SkillPosition.x + 0.096f), Screen.height * SkillPosition.y, Screen.width * SkillPosition.width, Screen.height * SkillPosition.height), (Resources.Load("GUI Texture/Spell" + playerStats.stats.SkillList[2].Name) as Texture2D));
        GUI.DrawTexture(new Rect(Screen.width * (SkillPosition.x + 0.1435f), Screen.height * SkillPosition.y, Screen.width * SkillPosition.width, Screen.height * SkillPosition.height), (Resources.Load("GUI Texture/Spell" + playerStats.stats.SkillList[3].Name) as Texture2D));
    }
    void DrawCooldownSkills()
    {
        if (playerStats.stats.SkillList[0].CurrentCooldown > 0)
        {
            GUI.DrawTexture(new Rect(Screen.width * SkillPosition.x, Screen.height * SkillPosition.y, Screen.width * SkillPosition.width, Screen.height * SkillPosition.height), (Resources.Load("GUI Texture/Spell" + playerStats.stats.SkillList[0].Name + "Notready") as Texture2D));
        }
        if (playerStats.stats.SkillList[1].CurrentCooldown > 0)
        {
            GUI.DrawTexture(new Rect(Screen.width * (SkillPosition.x + 0.0485f), Screen.height * SkillPosition.y, Screen.width * SkillPosition.width, Screen.height * SkillPosition.height), (Resources.Load("GUI Texture/Spell" + playerStats.stats.SkillList[1].Name + "Notready") as Texture2D));
        }
        if (playerStats.stats.SkillList[2].CurrentCooldown > 0)
        {
            GUI.DrawTexture(new Rect(Screen.width * (SkillPosition.x + 0.096f), Screen.height * SkillPosition.y, Screen.width * SkillPosition.width, Screen.height * SkillPosition.height), (Resources.Load("GUI Texture/Spell" + playerStats.stats.SkillList[2].Name + "Notready") as Texture2D));
        }
        if (playerStats.stats.SkillList[3].CurrentCooldown > 0)
        {
            GUI.DrawTexture(new Rect(Screen.width * (SkillPosition.x + 0.1435f), Screen.height * SkillPosition.y, Screen.width * SkillPosition.width, Screen.height * SkillPosition.height), (Resources.Load("GUI Texture/Spell" + playerStats.stats.SkillList[3].Name + "Notready") as Texture2D));
        }        
    }
}
