using Engine;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

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
        Four,
        Five,
        Six
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

    public List<Sprite> AllSprites { get; set; }

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

        AllSprites = new List<Sprite>();
    }

    #endregion

    public void LoadAllSprites()
    {
        var textures = new List<Sprite>
        {
            Resources.Load<Sprite>("LobbyMaterials/Icons/Water/SpellIceRamp"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Water/SpellIcefloor"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Water/SpellWaterbullets"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Water/SpellWaterShield"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Water/SpellBloodbending"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Water/SpellHealing"),

            Resources.Load<Sprite>("LobbyMaterials/Icons/Earth/SpellRockShoes"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Earth/SpellEarthblock"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Earth/SpellEarthquake"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Earth/SpellEarthbomb"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Earth/SpellEartharmor"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Earth/SpellEarthShelter"),

            Resources.Load<Sprite>("LobbyMaterials/Icons/Fire/SpellFirestream"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Fire/SpellBlazingring"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Fire/SpellShieldoffire"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Fire/SpellJetpropulsion"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Fire/SpellLightninggeneration"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Fire/SpellEnergyReading"),

            Resources.Load<Sprite>("LobbyMaterials/Icons/Air/SpellAirblast"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Air/SpellAirvortex"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Air/SpellEnhancedspeed"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Air/SpellStrongwind"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Air/SpellAirManipulation"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Air/SpellAirshield")
        };

        AllSprites.AddRange(textures);
    }

    #region Load Skills Methods

    public void LoadWaterSkills()
    {
        ActiveSkills.Add(
            new ActiveClickableSkill(new ActiveSkill("Ice Ramp",
                "Waterbenders can manipulate ice as a means of short transportation",
                0, 0.4, 0, false, 0.1, 0, 0, 0, 0, 0)));
        ActiveSkills.Add(new ActiveClickableSkill(new ActiveSkill("Ice Floor",
            "a waterbender can cover a large area of the ground with ice, trapping their enemies' feet in ice", 10, 20,
            7, false, 2, 0, 2.5, 0, 0, 1)));
        ActiveSkills.Add(new ActiveClickableSkill(new ActiveSkill("Water Bullets",
            "The waterbullet is a move where a waterbender bends a large amount and shoots in a forcefull blow", 1.5, 0,
            0.3, true, 1.9, 0, 9, 0, 12, 5)));
        ActiveSkills.Add(new ActiveClickableSkill(new ActiveSkill("Water Shield",
            "Capable waterbenders are able to sustain a large amount of attacks by creating a bubble around themselves and their fellow travelers",
            0, 10, 0, false, 10, 0, 0, 0, 0, 0)));

        ActiveSkills.Add(
            new ActiveClickableSkill(new ActiveSkill("Bloodbending",
                "Bloodbending is a rather sinister application of the principle that water is present in every living organism, thus making them bendable objects themselves",
                6, 30, 0.5, false, 20, 0, 15, 0, 0, 0)));
        ActiveSkills.Add(
            new ActiveClickableSkill(new ActiveSkill("Healing",
                "Waterbenders can sometimes use a unique sub-skill: the ability to heal injuries by redirecting energy paths, or chi, throughout the body, using water as a catalyst",
                20, 15, 5, false, 10, 0, 0, 0, 3, 0)));
    }

    public void LoadEarthSkills()
    {
        ActiveSkills.Add(
            new ActiveClickableSkill(new ActiveSkill("Rock Shoes",
                "Rock shoes makes the earthbender more stable and therefore stronger", 0, 25, 0, false, 6, 0, 0, 0, 0, 0)));
        ActiveSkills.Add(new ActiveClickableSkill(new ActiveSkill("Earthquake",
            "Creates localized earthquakes or fissures to throw opponents off-balance", 7, 20, 1, true, 1.9, 0, 15, 0, 30, 1)));
        ActiveSkills.Add(new ActiveClickableSkill(new ActiveSkill("Earthblock",
            "Earthbenders can bring up blocks of earth and launch them at their enemies", 5, 0, 0.7, true, 2, 0, 10, 0, 9, 1)));
        ActiveSkills.Add(new ActiveClickableSkill(new ActiveSkill("Earth Bomb",
            "By sending a rock toward the ground, earthbenders can cause massive damage as well as throw their opponents off their feet",
            30, 20, 3, false, 18, 0, 10, 0, 6, 1)));

        ActiveSkills.Add(
            new ActiveClickableSkill(new ActiveSkill("Earth Armor",
                "Earthbenders can bring rocks, dust, pebbles, or crystals around them and mold them to fit their body and create something similar to armor",
                0, 10, 0, false, 10, 0, 0, 0, 0, 0)));
        ActiveSkills.Add(
            new ActiveClickableSkill(new ActiveSkill("Earth Shelter",
                "This skill can be used by earthbenders to create a shelter or dome which can provide an instant shelter in the wilderness",
                0, 15, 3, false, 10, 0, 0, 0, 3, 0)));
    }

    public void LoadFireSkills()
    {
        ActiveSkills.Add(new ActiveClickableSkill(new ActiveSkill("Firestream",
            "Basic firebending ability, firebenders can shoot continues streams of fire from there fingertips, fists, palms or legs",
            0.1, 0, 0.5, false, 0.025, 0, 4, 0, 15, 1)));
        ActiveSkills.Add(new ActiveClickableSkill(new ActiveSkill("Blazing Ring",
            "Spinning kicks or sweeping arm movements create rings and arcs to slice larger, more widely spaced, or evasive targets",
            16, 20, 8, false, 4, 0, 0, 0, 0, 0)));
        ActiveSkills.Add(new ActiveClickableSkill(new ActiveSkill("Shield Of Fire",
            "This creates a protective fire shield around the front of, or the whole body of, a firebender that can deflect attacks and explosions",
            0, 15, 0, false, 10, 0, 0, 0, 0, 0)));
        ActiveSkills.Add(new ActiveClickableSkill(new ActiveSkill("Jet Propulsion",
            "Skilled firebending masters are able to conjure huge amounts of flame to propel themselves at high speeds on the ground or through the air",
            0, 4, 0, false, 0, 0, 0, 0, 0, 0)));

        ActiveSkills.Add(
            new ActiveClickableSkill(new ActiveSkill("Lightning Generation",
                "Lightning generation is the ability to generate and direct lightning. It requires peace of mind and a complete absence of emotion",
                25, 25, 0.1, false, 15, 0, 10, 0, 40, 1)));
        ActiveSkills.Add(
            new ActiveClickableSkill(new ActiveSkill("Energy Reading",
                "In a similar way to healing, firebenders are capable of using fire to sense chi paths and interpret spiritual energy. ",
                0, 15, 3, false, 10, 0, 0, 0, 0, 0)));
    }

    public void LoadAirSkills()
    {
        ActiveSkills.Add(new ActiveClickableSkill(new ActiveSkill("Airblast",
            "A more offensive manouver involving a direct pulse of strong wind from the hand, feet or mouth", 3.5, 0, 1, true, 1.8, 0, 5, 0, 20, 1)));
        ActiveSkills.Add(new ActiveClickableSkill(new ActiveSkill("Air Vortex",
            "A spinning funnel of air of varying size, the air vortex can be used to trap or disorient opponents", 2, 20, 2, false, 5, 0, 15, 0, 0, 1)));
        ActiveSkills.Add(new ActiveClickableSkill(new ActiveSkill("Enhanced Speed",
            "When used by a skilled airbender, this technique can enable the airbender using it to travel at a speed almost too swift for the naked eye to be able to see properly. A master airbender can use this technique to briefly run across water",
            0, 10, 0, false, 4, 0, 4.5, 0, 0, 0)));

        ActiveSkills.Add(
            new ActiveClickableSkill(new ActiveSkill("Strong Wind", "Advanced air bending ability", 18, 22, 1, false,
                20, 0, 8, 0, 20, 1)));
        ActiveSkills.Add(
            new ActiveClickableSkill(new ActiveSkill("Air Manipulation",
                "By using circular, evavsive movements, airbenders build up massive momentum; this buildup of energy is released as massive power",
                0, 15, 3, false, 10, 0, 15, 0, 0, 0)));
        ActiveSkills.Add(
            new ActiveClickableSkill(new ActiveSkill("Air Shield",
                "The most common defensive tactic, though less powerful than the air barrier, it involves circling enemies, suddenly changing direction when attacked and evading by physical movement rather than bending",
                5, 10, 5, false, 10, 0, 0, 0, 0, 0)));

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
            Resources.Load<Sprite>("LobbyMaterials/Icons/Water/SpellWaterShield"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Water/SpellBloodbending"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Water/SpellHealing")
        };

        NormalImages.AddRange(textures);

        textures = new List<Sprite>
        {
            Resources.Load<Sprite>("LobbyMaterials/Icons/Water/SpellIceRamp_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Water/SpellIcefloor_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Water/SpellWaterbullets_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Water/SpellWaterShield_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Water/SpellBloodbending_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Water/SpellHealing_Clicked")
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
            Resources.Load<Sprite>("LobbyMaterials/Icons/Earth/SpellEarthbomb"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Earth/SpellEartharmor"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Earth/SpellEarthShelter")
        };

        NormalImages.AddRange(textures);

        textures = new List<Sprite>
        {
            Resources.Load<Sprite>("LobbyMaterials/Icons/Earth/SpellRockShoes_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Earth/SpellEarthblock_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Earth/SpellEarthquake_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Earth/SpellEarthbomb_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Earth/SpellEartharmor_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Earth/SpellEarthShelter_Clicked")
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
            Resources.Load<Sprite>("LobbyMaterials/Icons/Fire/SpellJetpropulsion"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Fire/SpellLightninggeneration"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Fire/SpellEnergyReading")
        };

        NormalImages.AddRange(textures);

        textures = new List<Sprite>
        {
            Resources.Load<Sprite>("LobbyMaterials/Icons/Fire/SpellFirestream_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Fire/SpellBlazingring_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Fire/SpellShieldoffire_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Fire/SpellJetpropulsion_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Fire/SpellLightninggeneration_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Fire/SpellEnergyReading_Clicked")
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
            Resources.Load<Sprite>("LobbyMaterials/Icons/Air/SpellStrongwind"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Air/SpellAirManipulation"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Air/SpellAirshield")
        };

        NormalImages.AddRange(textures);

        textures = new List<Sprite>
        {
            Resources.Load<Sprite>("LobbyMaterials/Icons/Air/SpellAirblast_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Air/SpellAirvortex_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Air/SpellEnhancedspeed_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Air/SpellStrongwind_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Air/SpellAirManipulation_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Air/SpellAirshield_Clicked")
        };

        ClickedImages.AddRange(textures);
    }

    #endregion
}