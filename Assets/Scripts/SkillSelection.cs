using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Engine;
using UnityEditor;
using UnityEngine.UI;

public class SkillSelection : MonoBehaviour
{
    private List<ActiveSkill> _skills = new List<ActiveSkill>();
    public List<Texture2D> Images = new List<Texture2D>();

    public void DisplayImages()
    {
        foreach (var image in Images)
        {
            
        }
    }

    public void AirSkills()
    {
        _skills.Add(new ActiveSkill("Airblast", "A more offensive manouver involving a direct pulse of strong wind from the hand, feet or mouth", 3.5, 0, 1, false, false, true, false, 1.8, 0, 5, 0, 20, 1));
        _skills.Add(new ActiveSkill("Air vortex", "A spinning funnel of air of varying size, the air vortex can be used to trap or disorient opponents", 2, 20, 2, false, false, false, false, 5, 0, 15, 0, 0, 1));
        _skills.Add(new ActiveSkill("EnhancedSpeed", "When used by a skilled airbender, this technique can enable the airbender using it to travel at a speed almost too swift for the naked eye to be able to see properly. A master airbender can use this technique to briefly run across water", 0, 10, 0, false, false, false, false, 4, 0, 4.5, 0, 0, 0));

        var image = Resources.Load("LobbyMaterials/Icons/SpellAirblast") as Texture2D;
        Images.Add(image);
        image = Resources.Load("LobbyMaterials/Icons/SpellAirvortex") as Texture2D;
        Images.Add(image);
        image = Resources.Load("LobbyMaterials/Icons/SpellAirblast") as Texture2D;
        Images.Add(image);
    }

    public void EarthSkills()
    {

    }

    public void FireSkills()
    {

    }

    public void WaterSkills()
    {

    }
}
