using UnityEngine;
using System.Collections;

public class ActionBarController : MonoBehaviour {

    public Texture2D actionBar;
    public Texture2D minimap;
    public Texture2D healthBar;
    public Texture2D chiBar;
    public Texture2D ButtonFrame;
    public Rect actionBarPosition;
    public Rect minimapPosition;
    public Rect healthBarPosition;
    public Rect chiPosition;
    public Rect SkillPosition;
    public Rect ButtonPositions;
    public Rect ButtonFramePosition;
    public Rect TooltipPosition;
    public float BarMax;
    private PlayerStats playerStats;
    public GUIStyle Style;

	// Use this for initialization
	void Start () {
        BarMax = 0.292f;
        actionBarPosition.Set(0.3F, 0.88F, 0.4F, 0.12F);
        minimapPosition.Set(0.83F, 0.7F, 0.181F, 0.32F);
        healthBarPosition.Set(0.354f, 0.893f, BarMax, 0.008f);
        chiPosition.Set(0.354f, 0.907f, BarMax, 0.008f);
        SkillPosition.Set(0.409f,0.933f,0.034f,0.06f);
        ButtonFramePosition.Set(0, 0.43f, 0.39f, 0.57f);
        ButtonPositions.Set(0.006f, 0.865f, 0.03f, 0.025f);
        TooltipPosition.Set(0.004f, 0.665f, 0.5f, 0.5f);
        Style.fontSize = 10;
        
      
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
        DrawMinimapFrame();
        DrawButtonFrame();
        
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
    void DrawMinimapFrame()
    {
        GUI.DrawTexture(new Rect(Screen.width * minimapPosition.x, Screen.height * minimapPosition.y, Screen.width * minimapPosition.width, Screen.height * minimapPosition.height), minimap);
    }
    void DrawButtonFrame()
    {
        GUI.DrawTexture(new Rect(Screen.width * ButtonFramePosition.x, Screen.height * ButtonFramePosition.y, Screen.width * ButtonFramePosition.width, Screen.height * ButtonFramePosition.height), ButtonFrame);
        GUI.Button(new Rect(Screen.width * ButtonPositions.x , Screen.height * ButtonPositions.y, Screen.width * ButtonPositions.width, Screen.height * ButtonPositions.height), new GUIContent("1","Power:\nDamage+3\nPhysicalresistance+1\nSkillradius+2\nMaximumChi+1"));
        GUI.Button(new Rect(Screen.width * (ButtonPositions.x +0.031f), Screen.height * ButtonPositions.y, Screen.width * ButtonPositions.width, Screen.height * ButtonPositions.height), new GUIContent("2","Agility:\nMovementspeed+1,5\nCooldownreduction+0.5\nSkillrange+2"));
        GUI.Button(new Rect(Screen.width * (ButtonPositions.x + 0.062f), Screen.height * ButtonPositions.y, Screen.width * ButtonPositions.width, Screen.height * ButtonPositions.height), new GUIContent("3", "Harmony:\nDamage+1\nHealingpower+3\nBuffduration+1\nMagicresistance+2\nChiRegeneration+3"));
        GUI.Button(new Rect(Screen.width * (ButtonPositions.x + 0.093f), Screen.height * ButtonPositions.y, Screen.width * ButtonPositions.width, Screen.height * ButtonPositions.height), new GUIContent("4", "Toughness:\nArmor+2\nPhysicalresistance+2\nHealthRegeneration+4"));
        GUI.Button(new Rect(Screen.width * (ButtonPositions.x + 0.124f), Screen.height * ButtonPositions.y, Screen.width * ButtonPositions.width, Screen.height * ButtonPositions.height), new GUIContent("5", "Endurance:\nMaximumHealthpoints+2\nHealthRegeneration+4\nMagicresistance+1"));
        GUI.Button(new Rect(Screen.width * (ButtonPositions.x + 0.155f), Screen.height * ButtonPositions.y, Screen.width * ButtonPositions.width, Screen.height * ButtonPositions.height), new GUIContent("6", "Wisdom:\nMaximumChi+4\nChiRegeneration+4\nDebuffduration+0.5\nCooldownreduction+1"));
        GUI.Button(new Rect(Screen.width * (ButtonPositions.x + 0.186f), Screen.height * ButtonPositions.y, Screen.width * ButtonPositions.width, Screen.height * ButtonPositions.height), new GUIContent("7", "Versatility:\nBuffduration+2\nDebuffduration+1,5\nDamage+1\nHealingpower+1")); ;

        GUI.Label(new Rect(Screen.width * ButtonPositions.x, Screen.height * (ButtonPositions.y + 0.02f), Screen.width * ButtonPositions.width, Screen.height * ButtonPositions.height), "Pow");
        GUI.Label(new Rect(Screen.width * (ButtonPositions.x + 0.031f), Screen.height * (ButtonPositions.y + 0.02f), Screen.width * ButtonPositions.width, Screen.height * ButtonPositions.height), "Agi");
        GUI.Label(new Rect(Screen.width * (ButtonPositions.x + 0.062f), Screen.height * (ButtonPositions.y + 0.02f), Screen.width * ButtonPositions.width, Screen.height * ButtonPositions.height), "Har");
        GUI.Label(new Rect(Screen.width * (ButtonPositions.x + 0.093f), Screen.height * (ButtonPositions.y + 0.02f), Screen.width * ButtonPositions.width, Screen.height * ButtonPositions.height), "Tou");
        GUI.Label(new Rect(Screen.width * (ButtonPositions.x + 0.124f), Screen.height * (ButtonPositions.y + 0.02f), Screen.width * ButtonPositions.width, Screen.height * ButtonPositions.height), "End");
        GUI.Label(new Rect(Screen.width * (ButtonPositions.x + 0.155f), Screen.height * (ButtonPositions.y + 0.02f), Screen.width * ButtonPositions.width, Screen.height * ButtonPositions.height), "Wis");
        GUI.Label(new Rect(Screen.width * (ButtonPositions.x + 0.186f), Screen.height * (ButtonPositions.y + 0.02f), Screen.width * ButtonPositions.width, Screen.height * ButtonPositions.height), "Ver");

        GUI.Label(new Rect(Screen.width * (ButtonPositions.x + 0.063f), Screen.height * (ButtonPositions.y + 0.071f), Screen.width * ButtonPositions.width, Screen.height * ButtonPositions.height), "Damage: " + playerStats.stats.Damage,Style);
        GUI.Label(new Rect(Screen.width * (ButtonPositions.x + 0.063f), Screen.height * (ButtonPositions.y + 0.082f), Screen.width * ButtonPositions.width, Screen.height * ButtonPositions.height), "Chi: " + playerStats.stats.MaxChi, Style);
        GUI.Label(new Rect(Screen.width * (ButtonPositions.x + 0.063f), Screen.height * (ButtonPositions.y + 0.093f), Screen.width * ButtonPositions.width, Screen.height * ButtonPositions.height), "Chireg: " + playerStats.stats.Chireg, Style);
        GUI.Label(new Rect(Screen.width * (ButtonPositions.x + 0.063f), Screen.height * (ButtonPositions.y + 0.104f), Screen.width * ButtonPositions.width, Screen.height * ButtonPositions.height), "Armor: " + playerStats.stats.Armor, Style);
        GUI.Label(new Rect(Screen.width * (ButtonPositions.x + 0.063f), Screen.height * (ButtonPositions.y + 0.115f), Screen.width * ButtonPositions.width, Screen.height * ButtonPositions.height), "Skillrange: " + playerStats.stats.Skillrange, Style);
        GUI.Label(new Rect(Screen.width * (ButtonPositions.x + 0.143f), Screen.height * (ButtonPositions.y + 0.071f), Screen.width * ButtonPositions.width, Screen.height * ButtonPositions.height), "Health: " + playerStats.stats.MaxHealthpoints, Style);
        GUI.Label(new Rect(Screen.width * (ButtonPositions.x + 0.143f), Screen.height * (ButtonPositions.y + 0.082f), Screen.width * ButtonPositions.width, Screen.height * ButtonPositions.height), "Healthreg: " + playerStats.stats.Healthreg, Style);
        GUI.Label(new Rect(Screen.width * (ButtonPositions.x + 0.143f), Screen.height * (ButtonPositions.y + 0.093f), Screen.width * ButtonPositions.width, Screen.height * ButtonPositions.height), "HealingPower: " + playerStats.stats.Healingpower, Style);
        GUI.Label(new Rect(Screen.width * (ButtonPositions.x + 0.143f), Screen.height * (ButtonPositions.y + 0.104f), Screen.width * ButtonPositions.width, Screen.height * ButtonPositions.height), "Skillradius: " + playerStats.stats.Skillradius, Style);
        GUI.Label(new Rect(Screen.width * (ButtonPositions.x + 0.143f), Screen.height * (ButtonPositions.y + 0.115f), Screen.width * ButtonPositions.width, Screen.height * ButtonPositions.height), "Cooldownred: " + playerStats.stats.Cooldownduration, Style);
        GUI.Label(new Rect(Screen.width * (ButtonPositions.x + 0.223f), Screen.height * (ButtonPositions.y + 0.071f), Screen.width * ButtonPositions.width, Screen.height * ButtonPositions.height), "Movement: " + playerStats.stats.Movementspeed, Style);
        GUI.Label(new Rect(Screen.width * (ButtonPositions.x + 0.223f), Screen.height * (ButtonPositions.y + 0.082f), Screen.width * ButtonPositions.width, Screen.height * ButtonPositions.height), "Physicalres: " + playerStats.stats.Physicalresistance, Style);
        GUI.Label(new Rect(Screen.width * (ButtonPositions.x + 0.223f), Screen.height * (ButtonPositions.y + 0.093f), Screen.width * ButtonPositions.width, Screen.height * ButtonPositions.height), "Magicres: " + playerStats.stats.Magicalresistance, Style);
        GUI.Label(new Rect(Screen.width * (ButtonPositions.x + 0.223f), Screen.height * (ButtonPositions.y + 0.104f), Screen.width * ButtonPositions.width, Screen.height * ButtonPositions.height), "Buffduration: " + playerStats.stats.Buffeffectduration, Style);
        GUI.Label(new Rect(Screen.width * (ButtonPositions.x + 0.223f), Screen.height * (ButtonPositions.y + 0.115f), Screen.width * ButtonPositions.width, Screen.height * ButtonPositions.height), "Debuffduration: " + playerStats.stats.Debuffeffectduration, Style);

        GUI.Label(new Rect(Screen.width * (TooltipPosition.x + 0), Screen.height * (TooltipPosition.y + 0.05f), Screen.width * TooltipPosition.width, Screen.height * TooltipPosition.height), GUI.tooltip);
    }
}
