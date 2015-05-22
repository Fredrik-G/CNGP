using System.Collections.Generic;
using Engine;
using UnityEngine;

/// <summary>
/// Class used for passive skill selection.
/// Contains lists of all skills and their sprites and load methods.
/// </summary>
public class PassiveSkillSelection
{
    #region Data

    /// <summary>
    /// Class that contains information about an passive skill and a bool variable for displaying if the passive skill is clicked.
    /// </summary>
    public class PassiveClickableSkill
    {
        public bool IsClicked { get; set; }
        public PassiveSkill PassiveSkill { get; set; }

        public PassiveClickableSkill(PassiveSkill passiveSkill)
        {
            IsClicked = false;
            PassiveSkill = passiveSkill;
        }
    }

    /// <summary>
    /// Enum of all passive skills.
    /// </summary>
    public enum Skills
    {
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine
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
    public List<PassiveClickableSkill> PassiveSkills { get; set; }

    public List<Sprite> AllSprites; 

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

    public PassiveSkillSelection()
    {
        PassiveSkills = new List<PassiveClickableSkill>();
        NormalImages = new List<Sprite>();
        ClickedImages = new List<Sprite>();
        MaxSelectedSkills = 2;
        CurrentNumberOfSelectedSkills = 0;

        AllSprites = new List<Sprite>();
    }

    #endregion

    public void LoadAllSprites()
    {
        var textures = new List<Sprite>
        {
            Resources.Load<Sprite>("LobbyMaterials/Icons/Passives/PassiveSkillAstralProjection"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Passives/PassiveSkillManipulationofSpiritEnergy"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Passives/PassiveSkillStrongPresence"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Passives/PassiveSkillTheFaceStealersSecret"),

            Resources.Load<Sprite>("LobbyMaterials/Icons/Passives/PassiveSkillFrost"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Passives/PassiveSkillTailwind"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Passives/PassiveSkillLeech"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Passives/PassiveSkillAdvancedBending"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Passives/PassiveSkillHarmonicConvergence")

        };

        AllSprites.AddRange(textures);
    }

    #region Load Skills & Images Methods

    public void LoadPassiveSkills()
    {
        PassiveSkills.Add(new PassiveClickableSkill(new PassiveSkill("Astral Projection", 0)));
        PassiveSkills.Add(new PassiveClickableSkill(new PassiveSkill("Manipulation of Spirit Energy", 0)));
        PassiveSkills.Add(new PassiveClickableSkill(new PassiveSkill("Strong Presence", 0)));
        PassiveSkills.Add(new PassiveClickableSkill(new PassiveSkill("The Face Stealers Secret", 0)));

        PassiveSkills.Add(new PassiveClickableSkill(new PassiveSkill("Frost", 0)));
        PassiveSkills.Add(new PassiveClickableSkill(new PassiveSkill("Tailwind", 0)));
        PassiveSkills.Add(new PassiveClickableSkill(new PassiveSkill("Leech", 0)));
        PassiveSkills.Add(new PassiveClickableSkill(new PassiveSkill("Advanced Bending", 0)));
        PassiveSkills.Add(new PassiveClickableSkill(new PassiveSkill("Harmonic Convergence", 0)));
    }

    public void LoadPassiveImages()
    {
        var textures = new List<Sprite>
        {
            Resources.Load<Sprite>("LobbyMaterials/Icons/Passives/PassiveSkillAstralProjection"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Passives/PassiveSkillManipulationofSpiritEnergy"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Passives/PassiveSkillStrongPresence"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Passives/PassiveSkillTheFaceStealersSecret"),

            Resources.Load<Sprite>("LobbyMaterials/Icons/Passives/PassiveSkillFrost"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Passives/PassiveSkillTailwind"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Passives/PassiveSkillLeech"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Passives/PassiveSkillAdvancedBending"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Passives/PassiveSkillHarmonicConvergence")

        };

        NormalImages.AddRange(textures);

        textures = new List<Sprite>
        {
            Resources.Load<Sprite>("LobbyMaterials/Icons/Passives/PassiveSkillAstralProjection_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Passives/PassiveSkillManipulationofSpiritEnergy_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Passives/PassiveSkillStrongPresence_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Passives/PassiveSkillTheFaceStealersSecret_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Passives/PassiveSkillFrost_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Passives/PassiveSkillTailwind_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Passives/PassiveSkillLeech_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Passives/PassiveSkillAdvancedBending_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Icons/Passives/PassiveSkillHarmonicConvergence_Clicked")
        };

        ClickedImages.AddRange(textures);
    }

    #endregion
}

