using UnityEngine;
using System.Collections;

public class HealthChiBarController : MonoBehaviour {

    public GameObject HealthBar;
    public GameObject ChiBar;
    public Transform target;

    public float HealthBarMaxScale;
    public float HealthBarCurrentScale;
    public float ChiBarMaxScale;
    public float ChiBarCurrentScale;

    private double _healthPointPercentage;
    private double _chiPointPercentage;

    private Vector3 HealthAndChiBarTarget;

    private PlayerStats playerStats;
	// Use this for initialization
	void Start () {

        HealthBar.transform.localScale = new Vector3(0.1f, 1, 0.1f);
        ChiBar.transform.localScale = new Vector3(0.1f, 1, 0.1f);
        HealthBarMaxScale = HealthBar.transform.localScale.y;
        HealthBarCurrentScale = HealthBarMaxScale;

        ChiBarMaxScale = ChiBar.transform.localScale.y;
        ChiBarCurrentScale = ChiBarMaxScale;

        playerStats = GetComponentInParent<PlayerStats>();
        
        HealthBar.transform.rotation = new Quaternion(0,0,0,0);
        Instantiate(HealthBar,HealthBar.transform.position,new Quaternion(0,0,0,0));
        Instantiate(ChiBar,ChiBar.transform.position,new Quaternion(0,0,0,0));

	}
	
	// Update is called once per frame
	void Update () {
        UpdateHealthBar();
        UpdateChiBar();

        HealthAndChiBarTarget = new Vector3(target.position.x, transform.position.y, target.position.z + 0.5f);
        HealthBar.transform.position = Vector3.Lerp(transform.position, HealthAndChiBarTarget, Time.deltaTime * 9999);
        ChiBar.transform.position = Vector3.Lerp(transform.position, HealthAndChiBarTarget, Time.deltaTime * 9999);
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
    void LateUpdate()
    {
        HealthAndChiBarTarget = new Vector3(target.position.x, transform.position.y, target.position.z + 0.5f);
        HealthBar.transform.position = Vector3.Lerp(transform.position, HealthAndChiBarTarget, Time.deltaTime * 9999);
    }
}
