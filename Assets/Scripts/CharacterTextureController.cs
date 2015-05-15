using UnityEngine;
using System.Collections;

public class CharacterTextureController : MonoBehaviour
{

    // Use this for initialization
    private PlayerStats playerStats;
    public Material Fire, Earth, Air, Water;
    // Use this for initialization
    void Start()
    {

        var Mesh = GetComponent<SkinnedMeshRenderer>();
        playerStats = GetComponentInParent<PlayerStats>();
        if (playerStats.elementClass.Equals("Fire"))
        {
            Mesh.material = Fire;
        }
        else if (playerStats.elementClass.Equals("Water"))
        {
            Mesh.material = Water;
        }
        else if (playerStats.elementClass.Equals("Air"))
        {
            Mesh.material = Air;
        }
        else if (playerStats.elementClass.Equals("Earth"))
        {
            Mesh.material = Earth;
        }
    }
}
