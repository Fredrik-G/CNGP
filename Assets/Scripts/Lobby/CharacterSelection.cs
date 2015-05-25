using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used for Character Selection.
/// Contains lists of character sprites and load methods.
/// </summary>
public class CharacterSelection
{
    #region Data

    public class CharacterInfoDescriptions
    {
        public string Water { get; set; }
        public string Earth { get; set; }
        public string Fire { get; set; }
        public string Air { get; set; }

        public CharacterInfoDescriptions()
        {
            Water = "Water description goes here";
            Earth = "Earth description goes here";
            Fire = "Fire description goes here";
            Air = "Air description goes here";
        }
    }

    public CharacterInfoDescriptions CharacterDescriptions { get; set; }

    /// <summary>
    /// Enum containing all available characters/elements.
    /// </summary>
    public enum Characters
    {
        None,
        Waterbending,
        Earthbending,
        Firebending,
        Airbending,
    }

    /// <summary>
    /// The currently selected character.
    /// </summary>
    public Characters CurrentCharacter { get; set; }

    /// <summary>
    /// List containing "normal" (not clicked) character sprites.
    /// </summary>
    public List<Sprite> NormalSprites { get; set; }

    /// <summary>
    /// List containing clicked character sprites.
    /// </summary>
    public List<Sprite> ClickedSprites { get; set; }

    #endregion

    #region Properties

    public int WaterbendingId
    {
        get { return (int) Characters.Waterbending - 1; }
    }

    public int EarthbendingId
    {
        get { return (int) Characters.Earthbending - 1; }
    }

    public int FirebendingId
    {
        get { return (int) Characters.Firebending - 1; }
    }

    public int AirbendingId
    {
        get { return (int) Characters.Airbending - 1; }
    }

    #endregion

    #region Constructor

    /// <summary>
    /// Default constructor. Sets current character to None.
    /// </summary>
    public CharacterSelection()
    {
        CurrentCharacter = Characters.None;
        NormalSprites = new List<Sprite>();
        ClickedSprites = new List<Sprite>();
        CharacterDescriptions = new CharacterInfoDescriptions();
    }

    #endregion

    #region Load Methods

    /// <summary>
    /// Loads every character sprite.
    /// </summary>
    public void LoadImages()
    {
        var normalSprites = new List<Sprite>
        {
            Resources.Load<Sprite>("LobbyMaterials/Character/Waterbending"),
            Resources.Load<Sprite>("LobbyMaterials/Character/Earthbending"),
            Resources.Load<Sprite>("LobbyMaterials/Character/Firebending"),
            Resources.Load<Sprite>("LobbyMaterials/Character/Airbending")
        };

        NormalSprites.AddRange(normalSprites);

        var clickedSprites = new List<Sprite>
        {
            Resources.Load<Sprite>("LobbyMaterials/Character/Waterbending_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Character/Earthbending_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Character/Firebending_Clicked"),
            Resources.Load<Sprite>("LobbyMaterials/Character/Airbending_Clicked")
        };

        ClickedSprites.AddRange(clickedSprites);
    }

    #endregion

    #region Toggle Active Character Methods

    /// <summary>
    /// Clears the current character if the Water Character is currently selected
    /// else sets the Water Character as the current character.
    /// </summary>
    /// <returns>Returns true if current character is water, else false.</returns>
    public bool ToggleWaterCharacter()
    {
        if (CurrentCharacter == Characters.Waterbending)
        {
            CurrentCharacter = Characters.None;
            return true;
        }

        CurrentCharacter = Characters.Waterbending;
        return false;
    }

    /// <summary>
    /// Clears the current character if the Earth Character is currently selected
    /// else sets the Earth Character as the current character.
    /// </summary>
    /// <returns>Returns true if current character is earth, else false.</returns>
    public bool ToggleEarthCharacter()
    {
        if (CurrentCharacter == Characters.Earthbending)
        {
            CurrentCharacter = Characters.None;
            return true;
        }

        CurrentCharacter = Characters.Earthbending;
        return false;
    }

    /// <summary>
    /// Clears the current character if the Fire Character is currently selected
    /// else sets the Fire Character as the current character.
    /// </summary>
    /// <returns>Returns true if current character is fire, else false.</returns>
    public bool ToggleFireCharacter()
    {
        if (CurrentCharacter == Characters.Firebending)
        {
            CurrentCharacter = Characters.None;
            return true;
        }

        CurrentCharacter = Characters.Firebending;
        return false;
    }

    /// <summary>
    /// Clears the current character if the Air Character is currently selected
    /// else sets the Air Character as the current character.
    /// </summary>
    /// <returns>Returns true if current character is air, else false.</returns>
    public bool ToggleAirCharacter()
    {
        if (CurrentCharacter == Characters.Airbending)
        {
            CurrentCharacter = Characters.None;
            return true;
        }

        CurrentCharacter = Characters.Airbending;
        return false;
    }

    #endregion
}
