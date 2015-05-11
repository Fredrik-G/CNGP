using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for character and skill handling.
/// </summary>
public class CharacterAndSkillsHandler : MonoBehaviour
{
    #region Data

    #region Text Fields

    public Text NameText;
    public Text InfoText;
    public Text AttributesText;

    #endregion

    #region Image Lists

    /// <summary>
    /// Lists of images set via Unity Editor.
    /// </summary>
    public List<Image> Characters = new List<Image>();
    public List<Image> ActiveSkills = new List<Image>();
    public List<Image> PassiveSkills = new List<Image>();

    #endregion

    private readonly CharacterSelection _characterSelection = new CharacterSelection();
    private readonly SkillSelection _skillSelection = new SkillSelection();

    #region Networking Variables

    private LobbyNetworking _lobbyNetworking = new LobbyNetworking();
    public PhotonView PhotonView;
    public List<Text> TeamMembersText = new List<Text>();

    #endregion

    #endregion

    #region Monobehaivor Methods

    public void Start()
    {
        _characterSelection.LoadImages();
        ResetCharacterSelection();
        EnablePassivSkillImages();
        _skillSelection.PreparePassiveSkills();
    }

    public void OnGUI()
    {
        //var players = PhotonNetwork.playerList;

        //var sortedPlayers = players.OrderBy(x => x.ID).ToArray();

        //for (var i = 0; i < sortedPlayers.Length; i++)
        //{
        //    TeamMembersText[i].text = sortedPlayers[i].name;
        //}
    }

    #endregion

    #region Networking

    public void OnPhotonPlayerConnected(PhotonPlayer player)
    {
        Debug.Log("Player Connected " + player.name);
    }

    public void OnPhotonPlayerDisconnected(PhotonPlayer player)
    {
        Debug.Log("Player Disconnected " + player.name);
    }

    public void HandleConfirmClick()
    {
        PhotonView.RPC("ConfirmSelections", PhotonTargets.All);
        //TODO
    }

    [RPC]
    public void ConfirmSelections()
    {
        if (PhotonView.isMine)
        {
            var playerId = PhotonNetwork.player.ID - 1;

            TeamMembersText[playerId].text = "Confirmed";
        }
    }

    #endregion

    #region Enable & Reset

    /// <summary>
    /// Resets the character selection and disables every activeskill-image.
    /// </summary>
    private void ResetCharacterSelection()
    {
        NameText.text = InfoText.text = AttributesText.text = String.Empty;
        for (var i = 0; i < Characters.Count; i++)
        {
            Characters[i].sprite = _characterSelection.NormalSprites[i];
        }

        foreach (var activeSkill in ActiveSkills)
        {
            activeSkill.enabled = false;
        }
    }

    /// <summary>
    /// Resets the skill selection.
    /// </summary>
    private void ResetSkillSelection()
    {
        NameText.text = InfoText.text = AttributesText.text = String.Empty;

        _skillSelection.ClearActiveSkillSelection();
    }

    /// <summary>
    /// Enables every activeskill-image.
    /// </summary>
    private void EnableActiveSkillImages()
    {
        foreach (var activeSkill in ActiveSkills)
        {
            activeSkill.enabled = true;
        }
    }

    /// <summary>
    /// Enables every passiveskill-image.
    /// </summary>
    private void EnablePassivSkillImages()
    {
        foreach (var passiveSkill in PassiveSkills)
        {
            passiveSkill.enabled = true;
        }
    }

    #endregion

    #region Character Mouse Clicks

    /// <summary>
    /// Handles mouse click for the Water image.
    /// </summary>
    public void HandleWaterMouseClick()
    {
        ResetCharacterSelection();

        if (_characterSelection.ToggleWaterCharacter())
        {
            Characters[_characterSelection.WaterbendingId].sprite =
                _characterSelection.NormalSprites[_characterSelection.WaterbendingId];
        }
        else
        {
            Characters[_characterSelection.WaterbendingId].sprite =
                _characterSelection.ClickedSprites[_characterSelection.WaterbendingId];
            InfoText.text = "Water description goes here";
            DisplaySkillImages();
        }
    }

    /// <summary>
    /// Handles mouse click for the Earth image.
    /// </summary>
    public void HandleEarthMouseClick()
    {
        ResetCharacterSelection();

        if (_characterSelection.ToggleEarthCharacter())
        {
            Characters[_characterSelection.EarthbendingId].sprite =
                _characterSelection.NormalSprites[_characterSelection.EarthbendingId];
        }
        else
        {
            Characters[_characterSelection.EarthbendingId].sprite =
                _characterSelection.ClickedSprites[_characterSelection.EarthbendingId];
            InfoText.text = "Earth description goes here";
            DisplaySkillImages();
        }
    }

    /// <summary>
    /// Handles mouse click for the Fire image.
    /// </summary>
    public void HandleFireMouseClick()
    {
        ResetCharacterSelection();

        if (_characterSelection.ToggleFireCharacter())
        {
            Characters[_characterSelection.FirebendingId].sprite =
                _characterSelection.NormalSprites[_characterSelection.FirebendingId];
        }
        else
        {
            Characters[_characterSelection.FirebendingId].sprite =
                _characterSelection.ClickedSprites[_characterSelection.FirebendingId];
            InfoText.text = "Fire description goes here";
            DisplaySkillImages();
        }
    }

    /// <summary>
    /// Handles mouse click for the Air image.
    /// </summary>
    public void HandleAirMouseClick()
    {
        ResetCharacterSelection();

        if (_characterSelection.ToggleAirCharacter())
        {
            Characters[_characterSelection.AirbendingId].sprite =
                _characterSelection.NormalSprites[_characterSelection.AirbendingId];
        }
        else
        {
            Characters[_characterSelection.AirbendingId].sprite =
                _characterSelection.ClickedSprites[_characterSelection.AirbendingId];
            InfoText.text = "Air description goes here";
            DisplaySkillImages();
        }
    }

    #endregion

    #region Active Skill Mouse Clicks

    /// <summary>
    /// Handles a single mouse click for an activeskill-image.
    /// Performs a click on said image and selects/deselects it.
    /// </summary>
    /// <param name="skill">The clicked skill.</param>
    private void HandleActiveSkillClick(ActiveSkillSelection.Skills skill)
    {
        var skillNumber = (int)skill;
        if (_skillSelection.PerformActiveSkillClick(skillNumber))
        {
            DisplayActiveSkillInformation(skill);
        }
        else
        {
            ResetSkillSelection();
            _skillSelection.DeselectActiveSkill(skill, ActiveSkills[skillNumber]);
        }
    }

    /// <summary>
    /// Handles a single mouse click for the first activeskill-image.
    /// </summary>
    public void HandleActiveSkillOneClick()
    {
        HandleActiveSkillClick(ActiveSkillSelection.Skills.One);
    }
    /// <summary>
    /// Handles a single mouse click for the second activeskill-image.
    /// </summary>
    public void HandleActiveSkillTwoClick()
    {
        HandleActiveSkillClick(ActiveSkillSelection.Skills.Two);
    }
    /// <summary>
    /// Handles a single mouse click for the third activeskill-image.
    /// </summary>
    public void HandleActiveSkillThreeClick()
    {
        HandleActiveSkillClick(ActiveSkillSelection.Skills.Three);
    }
    /// <summary>
    /// Handles a single mouse click for the fourth activeskill-image.
    /// </summary>
    public void HandleActiveSkillFourClick()
    {
        HandleActiveSkillClick(ActiveSkillSelection.Skills.Four);
    }

    #endregion

    #region Passive Skill Mouse Clicks

    /// <summary>
    /// Handles a single mouse click for an passiveskill-image.
    /// Performs a click on said image and selects/deselects it.
    /// </summary>
    /// <param name="skill">The clicked skill.</param>
    private void HandlePassiveSkillClick(PassiveSkillSelection.Skills skill)
    {
        var skillNumber = (int)skill;
        if (_skillSelection.PerformPassiveSkillClick(skillNumber))
        {
            DisplayPassiveSkillInformation(skill);
        }
        else
        {
            ResetSkillSelection();
            _skillSelection.DeselectPassiveSkill(skill, PassiveSkills[skillNumber]);
        }
    }

    /// <summary>
    /// Handles a single mouse click for the first passiveskill-image.
    /// </summary>
    public void HandlePassiveSkillOneClick()
    {
        HandlePassiveSkillClick(PassiveSkillSelection.Skills.One);
    }
    /// <summary>
    /// Handles a single mouse click for the second passiveskill-image.
    /// </summary>
    public void HandlePassiveSkillTwoClick()
    {
        HandlePassiveSkillClick(PassiveSkillSelection.Skills.Two);
    }
    /// <summary>
    /// Handles a single mouse click for the third passiveskill-image.
    /// </summary>
    public void HandlePassiveSkillThreeClick()
    {
        HandlePassiveSkillClick(PassiveSkillSelection.Skills.Three);
    }
    /// <summary>
    /// Handles a single mouse click for the fourth passiveskill-image.
    /// </summary>
    public void HandlePassiveSkillFourClick()
    {
        HandlePassiveSkillClick(PassiveSkillSelection.Skills.Four);
    }

    #endregion

    #region Display Methods

    /// <summary>
    /// Displays skill images based on the selected character/element.
    /// </summary>
    private void DisplaySkillImages()
    {
        EnableActiveSkillImages();
        EnablePassivSkillImages();

        switch (_characterSelection.CurrentCharacter)
        {
            case CharacterSelection.Characters.Waterbending:
                _skillSelection.PrepareWaterSkills();
                _skillSelection.DisplayWaterSkillImages(ActiveSkills);
                break;
            case CharacterSelection.Characters.Earthbending:
                _skillSelection.PrepareEarthSkills();
                _skillSelection.DisplayEarthSkillImages(ActiveSkills);
                break;
            case CharacterSelection.Characters.Firebending:
                _skillSelection.PrepareFireSkills();
                _skillSelection.DisplayFireSkillImages(ActiveSkills);
                break;
            case CharacterSelection.Characters.Airbending:
                _skillSelection.PrepareAirSkills();
                _skillSelection.DisplayAirSkillImages(ActiveSkills);
                break;
        }
    }

    /// <summary>
    /// Displays the selected active skill in the text boxes.
    /// </summary>
    /// <param name="skill"></param>
    private void DisplayActiveSkillInformation(ActiveSkillSelection.Skills skill)
    {
        var skillNumber = (int) skill;

        _skillSelection.SelectActiveSkill(skill, ActiveSkills[skillNumber]);

        NameText.text = _skillSelection.ActiveSkillSelection.ActiveSkills[skillNumber].ActiveSkill.Name;
        InfoText.text = _skillSelection.ActiveSkillSelection.ActiveSkills[skillNumber].ActiveSkill.Info;

        var attributeText = "Damage: " +
                            _skillSelection.ActiveSkillSelection.ActiveSkills[skillNumber].ActiveSkill
                                .DamageHealingPower
                            + "\nChi Cost: " +
                            _skillSelection.ActiveSkillSelection.ActiveSkills[skillNumber].ActiveSkill.ChiCost
                            + "\nCooldown: " +
                            _skillSelection.ActiveSkillSelection.ActiveSkills[skillNumber].ActiveSkill.Cooldown
                            + "\nRange: " +
                            _skillSelection.ActiveSkillSelection.ActiveSkills[skillNumber].ActiveSkill.Range;

        AttributesText.text = attributeText;
    }

    /// <summary>
    /// Displays the selected passive skill in the text boxes.
    /// </summary>
    /// <param name="skill"></param>
    private void DisplayPassiveSkillInformation(PassiveSkillSelection.Skills skill)
    {
        var skillNumber = (int) skill;

        _skillSelection.SelectPassiveSkill(skill, PassiveSkills[skillNumber]);

        NameText.text = _skillSelection.PassiveSkillSelection.PassiveSkills[skillNumber].PassiveSkill.Name;
        InfoText.text = "Cooldown: " +
                        _skillSelection.PassiveSkillSelection.PassiveSkills[skillNumber].PassiveSkill.Cooldown;
        AttributesText.text = String.Empty;
    }

    #endregion
}
