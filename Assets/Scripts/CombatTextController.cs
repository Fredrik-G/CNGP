using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CombatTextController : MonoBehaviour {
    private double Timeleft;
    private double damage;
    private Text combattext;
	// Use this for initialization
	void Start () {
        Timeleft = 0.75f;
               
               //combattext.color = Color.green;
               transform.rotation = new Quaternion(0, 0, 0, 0);
           
	}
    void TheStart(double Damage)
    {
        damage = Damage;
    }
	
	// Update is called once per frame
	void Update () {

        transform.position = (transform.position + new Vector3(0, 2.5f, 0) * Time.deltaTime);
        transform.localScale = (transform.localScale - new Vector3(0.8f, 1.6f) * Time.deltaTime);
        Timeleft -= Time.deltaTime;
        if(Timeleft < 0)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
	}

    public void SetDamage(double amount)
    {
        combattext = GetComponentInChildren<Text>();
        damage = amount;
        combattext.text = damage.ToString();

    }
}
