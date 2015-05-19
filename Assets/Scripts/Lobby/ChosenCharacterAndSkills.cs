using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChosenCharacterAndSkills
{
    public List<Image> ChosenImages;

    public void SetCharacterImage(Sprite characterSprite)
    {
        ChosenImages[0].sprite = characterSprite;
    }
}