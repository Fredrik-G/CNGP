using System.Collections.Generic;
using Engine;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used for skill (active and passive) selection.
/// Contains methods for selecting and displaying skills.
/// </summary>
public class SkillSelection
{
    #region Data

    public ActiveSkillSelection ActiveSkillSelection { get; set; }
    public PassiveSkillSelection PassiveSkillSelection { get; set; }

    #endregion

    #region Constructor

    /// <summary>
    /// Default Constructor.
    /// </summary>
    public SkillSelection()
    {
        ActiveSkillSelection = new ActiveSkillSelection();
        PassiveSkillSelection = new PassiveSkillSelection();
    }

    #endregion

    #region Active Skill Methods

    #region Display Methods

    /// <summary>
    /// Displays the water sprites in given list of images.
    /// </summary>
    /// <param name="activeSkills">List of images to set sprite of.</param>
    public void DisplayWaterSkillImages(List<Image> activeSkills)
    {
        for (var i = 0; i < activeSkills.Count; i++)
        {
            activeSkills[i].sprite = ActiveSkillSelection.NormalImages[i];
        }
    }

    /// <summary>
    /// Displays the earth sprites in given list of images.
    /// </summary>
    /// <param name="activeSkills">List of images to set sprite of.</param>
    public void DisplayEarthSkillImages(List<Image> activeSkills)
    {
        for (var i = 0; i < activeSkills.Count; i++)
        {
            activeSkills[i].sprite = ActiveSkillSelection.NormalImages[i];
        }
    }

    /// <summary>
    /// Displays the fire sprites in given list of images.
    /// </summary>
    /// <param name="activeSkills">List of images to set sprite of.</param>
    public void DisplayFireSkillImages(List<Image> activeSkills)
    {
        for (var i = 0; i < activeSkills.Count; i++)
        {
            activeSkills[i].sprite = ActiveSkillSelection.NormalImages[i];
        }
    }

    /// <summary>
    /// Displays the air sprites in given list of images.
    /// </summary>
    /// <param name="activeSkills">List of images to set sprite of.</param>
    public void DisplayAirSkillImages(List<Image> activeSkills)
    {
        for (var i = 0; i < activeSkills.Count; i++)
        {
            activeSkills[i].sprite = ActiveSkillSelection.NormalImages[i];
        }
    }

    #endregion

    #region Click & Selection Methods

    /// <summary>
    /// Performs a click on an active skill if possible.
    /// </summary>
    /// <param name="skillNumber"></param>
    /// <returns>Returns true if the given skill could be clicked on, otherwise false.</returns>
    public bool PerformActiveSkillClick(int skillNumber, LobbySounds soundController)
    {
        if (IsSkillClicked(skillNumber, true))
        {
            ActiveSkillSelection.ActiveSkills[skillNumber].IsClicked = false;
            ActiveSkillSelection.CurrentNumberOfSelectedSkills--;
            soundController.DeselectSkill();
            return false;
        }

        if (CanPerformSkillClick(true))
        {
            ActiveSkillSelection.ActiveSkills[skillNumber].IsClicked = true;
            ActiveSkillSelection.CurrentNumberOfSelectedSkills++;
            soundController.SelectSkill();
            return true;
        }

        return false;
    }

    /// <summary>
    /// Selects the given skill and changes its sprite to display selected state.
    /// </summary>
    /// <param name="skill">The skill to select.</param>
    /// <param name="activeSkill">The ActiveSkill Image to change sprite for.</param>
    public void SelectActiveSkill(ActiveSkillSelection.Skills skill, Image activeSkill)
    {
        var skillNumber = (int)skill;
        activeSkill.sprite = ActiveSkillSelection.ClickedImages[skillNumber];
    }

    /// <summary>
    /// Deselects the given skill and changes its sprite to display deselected state.
    /// </summary>
    /// <param name="skill">The skill to select.</param>
    /// <param name="activeSkill">The ActiveSkill Image to change sprite for.</param>
    public void DeselectActiveSkill(ActiveSkillSelection.Skills skill, Image activeSkill)
    {
        var skillNumber = (int)skill;
        activeSkill.sprite = ActiveSkillSelection.NormalImages[skillNumber];
    }

    /// <summary>
    /// Clears selection for every active skill.
    /// </summary>
    public void ClearActiveSkillSelection()
    {
        foreach (var activeSkill in ActiveSkillSelection.ActiveSkills)
        {
            activeSkill.IsClicked = false;
        }
        ActiveSkillSelection.CurrentNumberOfSelectedSkills = 0;
    }

    #endregion

    #region Prepare Methods

    /// <summary>
    /// Prepares the lists for water skills and images.
    /// </summary>
    public void PrepareWaterSkills()
    {
        ActiveSkillSelection.ActiveSkills.Clear();
        ActiveSkillSelection.NormalImages.Clear();
        ActiveSkillSelection.ClickedImages.Clear();

        ActiveSkillSelection.LoadWaterSkills();
        ActiveSkillSelection.LoadWaterImages();
    }

    /// <summary>
    /// Prepares the lists for earth skills and images.
    /// </summary>
    public void PrepareEarthSkills()
    {
        ActiveSkillSelection.ActiveSkills.Clear();
        ActiveSkillSelection.NormalImages.Clear();
        ActiveSkillSelection.ClickedImages.Clear();

        ActiveSkillSelection.LoadEarthSkills();
        ActiveSkillSelection.LoadEarthImages();
    }


    /// <summary>
    /// Prepares the lists for fire skills and images.
    /// </summary>
    public void PrepareFireSkills()
    {
        ActiveSkillSelection.ActiveSkills.Clear();
        ActiveSkillSelection.NormalImages.Clear();
        ActiveSkillSelection.ClickedImages.Clear();

        ActiveSkillSelection.LoadFireSkills();
        ActiveSkillSelection.LoadFireImages();
    }

    /// <summary>
    /// Prepares the lists for air skills and images.
    /// </summary>
    public void PrepareAirSkills()
    {
        ActiveSkillSelection.ActiveSkills.Clear();
        ActiveSkillSelection.NormalImages.Clear();
        ActiveSkillSelection.ClickedImages.Clear();

        ActiveSkillSelection.LoadAirSkills();
        ActiveSkillSelection.LoadAirImages();
    }

    #endregion

    #endregion

    #region Passive Skill Methods

    #region Click & Selection Methods

    /// <summary>
    /// Performs a click on an active skill if possible.
    /// </summary>
    /// <param name="skillNumber"></param>
    /// <returns>Returns true if the given skill could be clicked on, otherwise false.</returns>
    public bool PerformPassiveSkillClick(int skillNumber, LobbySounds soundController)
    {
        if (IsSkillClicked(skillNumber, false))
        {
            PassiveSkillSelection.PassiveSkills[skillNumber].IsClicked = false;
            PassiveSkillSelection.CurrentNumberOfSelectedSkills--;
            soundController.DeselectSkill();
            return false;
        }

        if (CanPerformSkillClick(false))
        {
            PassiveSkillSelection.PassiveSkills[skillNumber].IsClicked = true;
            PassiveSkillSelection.CurrentNumberOfSelectedSkills++;
            soundController.SelectSkill();
            return true;
        }

        return false;
    }

    /// <summary>
    /// Checks if a skill can be clicked based on 
    /// the current number of selected spells and if said spell is already clicked.
    /// </summary>
    /// <returns>Returns true if a skill can be clicked on, otherwise false.</returns>
    private bool CanPerformSkillClick(bool isActiveSkill)
    {
        if (isActiveSkill)
        {
            return ActiveSkillSelection.CurrentNumberOfSelectedSkills < ActiveSkillSelection.MaxSelectedSkills;
        }

        return PassiveSkillSelection.CurrentNumberOfSelectedSkills < PassiveSkillSelection.MaxSelectedSkills;
    }

    private bool IsSkillClicked(int skillNumber, bool isActiveSkill)
    {
        return isActiveSkill ? ActiveSkillSelection.ActiveSkills[skillNumber].IsClicked : PassiveSkillSelection.PassiveSkills[skillNumber].IsClicked;
    }

    /// <summary>
    /// Selects the given skill and changes its sprite to display selected state.
    /// </summary>
    /// <param name="skill">The skill to select.</param>
    /// <param name="passiveSkill">The ActiveSkill Image to change sprite for.</param>
    public void SelectPassiveSkill(PassiveSkillSelection.Skills skill, Image passiveSkill)
    {
        var skillNumber = (int)skill;
        passiveSkill.sprite = PassiveSkillSelection.ClickedImages[skillNumber];
    }

    /// <summary>
    /// Clears selection for every active skill.
    /// </summary>
    public void DeselectPassiveSkill(PassiveSkillSelection.Skills skill, Image passiveSkill)
    {
        var skillNumber = (int)skill;
        passiveSkill.sprite = PassiveSkillSelection.NormalImages[skillNumber];
    }

    #endregion

    /// <summary>
    /// Prepares the lists for water skills and images.
    /// </summary>
    public void PreparePassiveSkills()
    {
        PassiveSkillSelection.LoadPassiveSkills();
        PassiveSkillSelection.LoadPassiveImages();
    }
    #endregion
}
