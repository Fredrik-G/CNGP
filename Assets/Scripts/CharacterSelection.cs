using System;
using Engine;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public Text ElementDescription;

    public void Start()
    {
        ElementDescription.text = String.Empty;
    }

    public void HandleWaterMouseClick()
    {
        ElementDescription.text = "Water description goes here";
    }
    public void HandleEarthMouseClick()
    {
        ElementDescription.text = "Earth description goes here";
    }
    public void HandleFireMouseClick()
    {
        ElementDescription.text = "Fire description goes here";
    }
    public void HandleAirMouseClick()
    {
        ElementDescription.text = "Air description goes here";
    }

    public void HandleComfirm()
    {
        ElementDescription.enabled = false;
    }

}
