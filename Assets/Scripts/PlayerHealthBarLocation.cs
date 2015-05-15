using UnityEngine;
using System.Collections;

public class PlayerHealthBarLocation : MonoBehaviour
{
    public GameObject HealthBar;
    public GameObject ChiBar;
    public GameObject HealthAndChiBar;

    private GameObject Player;
    private PlayerStats playerStats;

    private float HealthBarMaxScale;
    private float HealthBarCurrentScale;
    private float ChiBarMaxScale;
    private float ChiBarCurrentScale;

    private double _healthPointPercentage;
    private double _chiPointPercentage;
    public void SetPlayer(GameObject target)
    {
        Player = target;
        playerStats = target.GetComponent<PlayerStats>();
        HealthBarMaxScale = ChiBarMaxScale = 1;
        ChiBarCurrentScale = HealthBarCurrentScale = 1;
    }
    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Player != null)
        {
            UpdateHealthBar();
            UpdateChiBar();

            HealthAndChiBar.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 2, Player.transform.position.z + 0.5f);
        }
        else
            Debug.Log("Failed");
    }

    void UpdateHealthBar()
    {
        _healthPointPercentage = (playerStats.stats.CurrentHealthpoints / playerStats.stats.MaxHealthpoints);
        HealthBarCurrentScale = (float)(_healthPointPercentage * HealthBarMaxScale);
        HealthBar.transform.localScale = new Vector3(HealthBar.transform.localScale.x, HealthBarCurrentScale, HealthBar.transform.localScale.z);
    }

    void UpdateChiBar()
    {
        _chiPointPercentage = (playerStats.stats.CurrentChi / playerStats.stats.MaxChi);
        ChiBarCurrentScale = (float)(_chiPointPercentage * ChiBarMaxScale);
      
        ChiBar.transform.localScale = new Vector3(ChiBar.transform.localScale.x, ChiBarCurrentScale, ChiBar.transform.localScale.z);
    }
}