using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Engine;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection
{
    public enum Characters
    {
        None,
        Waterbending,
        Earthbending,
        Firebending,
        Airbending,
    }

    public Characters CurrentCharacter { get; set; }
    public List<Sprite> NormalSprites { get; set; }
    public List<Sprite> ClickedSprites { get; set; }

    public int WaterbendingId
    {
        get { return (int) Characters.Waterbending - 1; }
    }
    public int EarthbendingId
    {
        get { return (int)Characters.Earthbending - 1; }
    }
    public int FirebendingId
    {
        get { return (int)Characters.Firebending - 1; }
    }
    public int AirbendingId
    {
        get { return (int)Characters.Airbending - 1; }
    }

    public CharacterSelection()
    {
        CurrentCharacter = Characters.None;
        NormalSprites = new List<Sprite>();
        ClickedSprites = new List<Sprite>();
    }

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
}
