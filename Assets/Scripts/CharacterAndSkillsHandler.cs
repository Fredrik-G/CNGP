using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CharacterAndSkillsHandler : MonoBehaviour
{
    public Text InfoDescription;
    public List<Image> Characters = new List<Image>();
    public List<Image> ActiveSkills = new List<Image>();
    public List<Image> PassiveSkills = new List<Image>(); 

    private CharacterSelection _characterSelection = new CharacterSelection();
    private SkillSelection _skillSelection = new SkillSelection();

    public void Start()
    {
        InfoDescription.enabled = true;
        _characterSelection.LoadImages();
    }

    private void ResetCharacterSelection()
    {
        InfoDescription.text = String.Empty;
        for (var i = 0; i < Characters.Count; i++)
        {
            Characters[i].sprite = _characterSelection.NormalSprites[i];
        }
    }

    public void HandleWaterMouseClick()
    {
        ResetCharacterSelection();

        if (_characterSelection.ToggleWaterCharacter())
        {
            Characters[_characterSelection.WaterbendingId].sprite = _characterSelection.NormalSprites[_characterSelection.WaterbendingId];
        }
        else
        {
            Characters[_characterSelection.WaterbendingId].sprite = _characterSelection.ClickedSprites[_characterSelection.WaterbendingId];
            InfoDescription.text = "Water description goes here";            
        }        
    }

    public void HandleEarthMouseClick()
    {
        ResetCharacterSelection();

        if (_characterSelection.ToggleEarthCharacter())
        {
            Characters[_characterSelection.EarthbendingId].sprite = _characterSelection.NormalSprites[_characterSelection.EarthbendingId];
        }
        else
        {
            Characters[_characterSelection.EarthbendingId].sprite = _characterSelection.ClickedSprites[_characterSelection.EarthbendingId];
            InfoDescription.text = "Water description goes here";
        } 
    }

    public void HandleFireMouseClick()
    {
        ResetCharacterSelection();

        if (_characterSelection.ToggleFireCharacter())
        {
            Characters[_characterSelection.FirebendingId].sprite = _characterSelection.NormalSprites[_characterSelection.FirebendingId];
        }
        else
        {
            Characters[_characterSelection.FirebendingId].sprite = _characterSelection.ClickedSprites[_characterSelection.FirebendingId];
            InfoDescription.text = "Water description goes here";
        } 
    }

    public void HandleAirMouseClick()
    {
        ResetCharacterSelection();

        if (_characterSelection.ToggleAirCharacter())
        {
            Characters[_characterSelection.AirbendingId].sprite = _characterSelection.NormalSprites[_characterSelection.AirbendingId];
        }
        else
        {
            Characters[_characterSelection.AirbendingId].sprite = _characterSelection.ClickedSprites[_characterSelection.AirbendingId];
            InfoDescription.text = "Water description goes here";
        } 
    }

    public void HandleBackClick()
    {
        //TODO
    }

    public void HandleNextClick()
    {
        switch (_characterSelection.CurrentCharacter)
        {
            case CharacterSelection.Characters.Waterbending:
                _skillSelection.WaterSkills();
                break;
            case CharacterSelection.Characters.Earthbending:
                _skillSelection.EarthSkills();
                break;
            case CharacterSelection.Characters.Firebending:
                _skillSelection.FireSkills();
                break;
            case CharacterSelection.Characters.Airbending:
                _skillSelection.AirSkills();
                break;
        }

        _skillSelection.DisplayImages();
    }

    public void HandleConfirmClick()
    {
        InfoDescription.enabled = false;
        //TODO
    }
}
