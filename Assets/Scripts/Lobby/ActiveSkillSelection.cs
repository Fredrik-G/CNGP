using Engine;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class used for active skill selection.
/// Contains lists of all skills and their sprites and load methods.
/// </summary>
public class ActiveSkillSelection
{
    #region Data

    /// <summary>
    /// Class that contains information about an active skill and a bool variable for displaying if the active skill is clicked.
    /// </summary>
    public class ActiveClickableSkill
    {
        public bool IsClicked { get; set; }
        public ActiveSkill ActiveSkill { get; set; }

        /// <summary>
        /// Constructor that sets "IsClicked" to false uses the given active skill.
        /// </summary>
        /// <param name="activeSkill"></param>
        public ActiveClickableSkill(ActiveSkill activeSkill)
        {
            IsClicked = false;
            ActiveSkill = activeSkill;
        }
    }

    /// <summary>
    /// Enum of all active skills.
    /// </summary>
    public enum Skills
    {
        One,
        Two,
        Three,
        Four
    }

    /// <summary>
    /// The max number of allowed selected skills.
    /// </summary>
    public int MaxSelectedSkills { get; set; }

    /// <summary>
    /// The number of currently selected skills.
    /// </summary>
    public int CurrentNumberOfSelectedSkills { get; set; }

    #region Lists

    /// <summary>
    /// List containing every active spell.
    /// </summary>
    public List<ActiveClickableSkill> ActiveSkills { get; set; }

    /// <summary>
    /// List containing "normal" (not clicked) skill sprites.
    /// </summary>
    public List<Sprite> NormalImages { get; set; }

    /// <summary>
    /// List containing clicked skill sprites.
    /// </summary>
    public List<Sprite> ClickedImages { get; set; }

    #endregion

    #endregion

    #region Constructor

    /// <summary>
    /// Default constructor.
    /// </summary>
    public ActiveSkillSelection()
    {
        ActiveSkills = new List<ActiveClickableSkill>();
        NormalImages = new List<Sprite>();
        ClickedImages = new List<Sprite>();
        MaxSelectedSkills = 4;
        CurrentNumberOfSelectedSkills = 0;
    }

    #endregion

    #region Load Skills Methods

    public void LoadWaterSkills()
    {
        ActiveSkills.Add(
            new ActiveClickableSkill(new ActiveSkill("Ice Ramp",
                "Waterbenders can manipulate ice as a means of short transportation",
                0, 0.4, 0, false, false, false, false, 0.1, 0, 0, 0, 0, 0)));
        ActiveSkills.Add(new ActiveClickableSkill(new ActiveSkill("Ice floor",
            "a waterbender can cover a large area of the ground with ice, trapping their enemies' feet in ice", 10, 20,
            7, false, false, false, false, 2, 0, 2.5, 0, 0, 1)));
        ActiveSkills.Add(new ActiveClickableSkill(new ActiveSkill("Water bullets",
            "The waterbullet is a move where a waterbender bends a large amount and shoots in a forcefull blow", 1.5, 0,
            0.3, false, false, true, false, 1.9, 0, 9, 0, 12, 5)));
        ActiveSkills.Add(new ActiveClickableSkill(new ActiveSkill("Water Shield",
            "Capable waterbenders are able to sustain a large amount of attacks by creating a bubble around themselves and their fellow travelers",
            0, 10, 0, false, false, false, false, 10, 0, 0, 0, 0, 0)));
    }

    public void LoadEarthSkills()
    {
        ActiveSkills.Add(
            new ActiveClickableSkill(new ActiveSkill("Rock Shoes",
                "Rock shoes makes the earthbender more stable and therefore stronger", 0, 25, 0, false, false, false,
                false, 6, 0, 0, 0, 0, 0)));
        ActiveSkills.Add(new ActiveClickableSkill(new ActiveSkill("Earthquake",
            "Creates localized earthquakes or fissures to throw opponents off-balance", 7, 20, 1, false, false, true,
            false, 1.9, 0, 15, 0, 30, 1)));
        ActiveSkills.Add(new ActiveClickableSkill(new ActiveSkill("Earthblock",
            "Earthbenders can bring up blocks of earth and launch them at their enemies", 5, 0, 0.7, false, false, true,
            false, 2, 0, 10, 0, 9, 1)));
        ActiveSkills.Add(new ActiveClickableSkill(new ActiveSkill("Earth Bomb",
            "By sending a rock toward the ground, earthbenders can cause massive damage as well as throw their opponents off their feet",
            30, 20, 3, false, false, false, false, 18, 0, 10, 0, 6, 1)));
    }

    public void LoadFireSkills()
    {
        ActiveSkills.Add(new ActiveClickableSkill(new ActiveSkill("Firestream",
            "Basic firebending ability, firebenders can shoot continues streams of fire from there fingertips, fists, palms or legs",
            0.1, 0, 0.5, false, false, false, false, 0.025, 0, 4, 0, 15, 1)));
        ActiveSkills.Add(new ActiveClickableSkill(new ActiveSkill("Blazing ring",
            "Spinning kicks or sweeping arm movements create rings and arcs to slice larger, more widely spaced, or evasive targets",
            16, 20, 8, false, false, false, false, 4, 0, 0, 0, 0, 0)));
        ActiveSkills.Add(new ActiveClickableSkill(new ActiveSkill("Shield Of Fire",
            "This creates a protective fire shield around the front of, or the whole body of, a firebender that can deflect attacks and explosions",
            0, 15, 0, false, false, false, false, 10, 0, 0, 0, 0, 0)));
        ActiveSkills.Add(new ActiveClickableSkill(new ActiveSkill("Jet Propulsion",
            "Skilled firebending masters are able to conjure huge amounts of flame to propel themselves at high speeds on the ground or through the air",
            0, 4, 0, false, false, false, false, 0, 0, 0, 0, 0, 0)));
    }

    public void LoadAirSkills()
    {
        ActiveSkills.Add(new ActiveClickableSkill(new ActiveSkill("Airblast",
            "A more offensive manouver involving a direct pulse of strong wind from the hand, feet or mouth", 3.5, 0, 1,
            false, false, true, false, 1.8, 0, 5, 0, 20, 1)));
        ActiveSkills.Add(new ActiveClickableSkill(new ActiveSkill("Air vortex",
            "A spinning funnel of air of varying size, the air vortex can be used to trap or disorient opponents", 2, 20,
            2, false, false, false, false, 5, 0, 15, 0, 0, 1)));
        ActiveSkills.Add(new ActiveClickableSkill(new ActiveSkill("Enhanced Speed",
            "When used by a skilled airbender, this technique can enable the airbender using it to travel at a speed almost too swift for the naked eye to be able to see properly. A master airbender can use this technique to briefly run across water",
            0, 10, 0, false, false, false, false, 4, 0, 4.5, 0, 0, 0)));
        ActiveSkills.Add(
            new ActiveClickableSkill(new ActiveSkill("Strong Wind", "Advanced air bending ability", 18, 22, 1, false,
                false, false,
                false, 20, 0, 8, 0, 20, 1)));
    }

    #endregion

    #region Load Images Methods

    public void LoadWaterImages()
    {
        var textures = new List<Sprite>
        {
            Resources.Load<Sprite>("LobbyMaterials/Icons/Water/SpellIceRamp"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Water/SpellIcefloor"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Water/SpellWaterbullets"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Water/SpellWaterShield")
        };

        NormalImages.AddRange(textures);

        textures = new List<Sprite>
        {
            Resources.Load<Sprite>("LobbyMaterials/Icons/Water/SpellIceRamp_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Water/SpellIcefloor_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Water/SpellWaterbullets_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Water/SpellWaterShield_Clicked")
        };

        ClickedImages.AddRange(textures);
    }

    public void LoadEarthImages()
    {
        var textures = new List<Sprite>
        {
            Resources.Load<Sprite>("LobbyMaterials/Icons/Earth/SpellRockShoes"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Earth/SpellEarthblock"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Earth/SpellEarthquake"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Earth/SpellEarthbomb")
        };

        NormalImages.AddRange(textures);

        textures = new List<Sprite>
        {
            Resources.Load<Sprite>("LobbyMaterials/Icons/Earth/SpellRockShoes_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Earth/SpellEarthblock_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Earth/SpellEarthquake_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Earth/SpellEarthbomb_Clicked")
        };

        ClickedImages.AddRange(textures);
    }

    public void LoadFireImages()
    {
        var textures = new List<Sprite>
        {
            Resources.Load<Sprite>("LobbyMaterials/Icons/Fire/SpellFirestream"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Fire/SpellBlazingring"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Fire/SpellShieldoffire"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Fire/SpellJetpropulsion")
        };

        NormalImages.AddRange(textures);

        textures = new List<Sprite>
        {
            Resources.Load<Sprite>("LobbyMaterials/Icons/Fire/SpellFirestream_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Fire/SpellBlazingring_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Fire/SpellShieldoffire_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Fire/SpellJetpropulsion_Clicked")
        };

        ClickedImages.AddRange(textures);
    }

    public void LoadAirImages()
    {
        var textures = new List<Sprite>
        {
            Resources.Load<Sprite>("LobbyMaterials/Icons/Air/SpellAirblast"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Air/SpellAirvortex"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Air/SpellEnhancedspeed"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Air/SpellStrongwind")
        };

        NormalImages.AddRange(textures);

        textures = new List<Sprite>
        {
            Resources.Load<Sprite>("LobbyMaterials/Icons/Air/SpellAirblast_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Air/SpellAirvortex_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Air/SpellEnhancedspeed_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Air/SpellStrongwind_Clicked")
        };

        ClickedImages.AddRange(textures);
    }

    #endregion
}