using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChosenCharacterAndSkills
{
    /// <summary>
    /// Sets the character/element image for a given player and given element.
    /// </summary>
    /// <param name="characterGameObject">The characters game object.</param>
    /// <param name="elementClass">The element class to change to</param>
    /// <param name="normalSprites">The list of all element sprites.</param>
    public void SetCharacterImage(GameObject characterGameObject, string elementClass, List<Sprite> normalSprites)
    {
        var image = characterGameObject.transform.Find("Chosen Character").GetComponent<Image>();

        Sprite sprite;
        
        switch (elementClass)
        {
            case "Water":
                sprite = normalSprites[(int)CharacterSelection.Characters.Waterbending - 1];
                break;
            case "Earth":
                sprite = normalSprites[(int)CharacterSelection.Characters.Earthbending - 1];
                break;
            case "Fire":
                sprite = normalSprites[(int)CharacterSelection.Characters.Firebending - 1];
                break;
            case "Air":
                sprite = normalSprites[(int)CharacterSelection.Characters.Airbending - 1];
                break;
            default:
                sprite = null;
                break;
        }
        image.sprite = sprite;
        image.enabled = true;
    }

    /// <summary>
    /// Sets an active skill image for a given player and given active skill.
    /// </summary>
    /// <param name="teamMemberObject">The players game object.</param>
    /// <param name="activeSkill">The active skill to change to.</param>
    /// <param name="currentNumberOfSelectedSkills">The current number of selected passives</param>
    /// <param name="elementClass">The players selected element.</param>
    /// <param name="allSprites">The list containing all active skill sprites.</param>
    public void SetActiveSkillImage(GameObject teamMemberObject, int activeSkill, int currentNumberOfSelectedSkills, string elementClass, List<Sprite> allSprites)
    {
        const int numberOfCharacterSkills = (int)ActiveSkillSelection.Skills.Six + 1;

        var image = teamMemberObject.transform.Find("Chosen Activeskill " + currentNumberOfSelectedSkills).GetComponent<Image>();

        var activeSkillIndex = 0;
        switch (elementClass)
        {
            case "Water":
                activeSkillIndex = numberOfCharacterSkills * 0 + activeSkill;
                break;
            case "Earth":
                activeSkillIndex = numberOfCharacterSkills * 1 + activeSkill;
                break;
            case "Fire":
                activeSkillIndex = numberOfCharacterSkills * 2 + activeSkill;
                break;
            case "Air":
                activeSkillIndex = numberOfCharacterSkills * 3 + activeSkill;
                break;
        }

        image.sprite = allSprites[activeSkillIndex];
        image.enabled = true;
    }

    /// <summary>
    /// Sets an passive image for a given player and given passive.
    /// </summary>
    /// <param name="teamMemberObject">The players game object.</param>
    /// <param name="passive">The passive to change to.</param>
    /// <param name="currentNumberOfSelectedPassives">The current number of selected passives.</param>
    /// <param name="allSprites">The list containing all passive sprites.</param>
    public void SetPassiveImage(GameObject teamMemberObject, int passive, int currentNumberOfSelectedPassives, List<Sprite> allSprites)
    {
        var image = teamMemberObject.transform.Find("Chosen Passive " + currentNumberOfSelectedPassives).GetComponent<Image>();

        var passiveIndex = passive;
        image.sprite = allSprites[passiveIndex];
        image.enabled = true;
    }
}