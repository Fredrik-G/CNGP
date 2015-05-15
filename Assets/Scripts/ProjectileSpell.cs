using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Engine;

public class ProjectileSpell : Photon.MonoBehaviour
{
    private List<int> playersHit = new List<int>();
    public string Name = "";
    public int spellTeam = 0;
    public double Scale { get; set; }
    public ActiveSkill ProjectileActiveSkill = new ActiveSkill();
    // Use this for initialization
    void Start()
    {
        playersHit.Clear();

    }

    // Update is called once per frame
    void Update()
    {
        if (Name.CompareTo("Firestream") == 0)
        {
            transform.localScale = Vector3.one * (float)Scale;

            Scale += (10 * Time.deltaTime);
        }
        /*
         * 
         * Hittar ägaren för objektet, om ägaren inte hittas
         * så förstörs objektet
         * 
         * */
        /* var idOfOwner = this.GetComponent<PhotonView>().owner;
         var owner = GameObject.FindGameObjectsWithTag("Player");
         bool ownerFound = false;
         foreach(GameObject o in owner)
         {
             if (o.GetComponent<PhotonView>().owner == idOfOwner)
                 ownerFound = true;
         }
         if (!ownerFound)
             PhotonNetwork.Destroy(this.gameObject);
          */
    }
  /*  [RPC]
    public void DestoryObject(int id)
    {
        if (gameObject.GetComponent<PhotonView>().owner.ID == id)
            PhotonNetwork.Destroy(this.gameObject);
    }*/

    public void Init(ActiveSkill AS)
    {
        ProjectileActiveSkill.Name = AS.Name;
        ProjectileActiveSkill.Info = AS.Info;
        ProjectileActiveSkill.DamageHealingPower = AS.DamageHealingPower;
        ProjectileActiveSkill.ChiCost = AS.ChiCost;
        ProjectileActiveSkill.Radius = AS.Radius;
        ProjectileActiveSkill.SingleTarget = AS.SingleTarget;
        ProjectileActiveSkill.SelfTarget = AS.SelfTarget;
        ProjectileActiveSkill.AllyTarget = AS.AllyTarget;
        ProjectileActiveSkill.DoCollide = AS.DoCollide;
        ProjectileActiveSkill.Cooldown = AS.Cooldown;
        ProjectileActiveSkill.Range = AS.Range;
        ProjectileActiveSkill.ChannelingTime = AS.ChannelingTime;
        ProjectileActiveSkill.CastSpeed = AS.CastSpeed;
    }

    /// <summary>
    /// Korrigerar skillens värden enligt spelarens stats.
    /// </summary>
    /// <returns>The skill values.</returns>
    /// <param name="AS">A.</param>
    public ActiveSkill AdjustActiveSkillValues(ActiveSkill AS, PlayerStats PS)
    {
        ActiveSkill AdjustedActiveSkill = new ActiveSkill();

        AdjustedActiveSkill.Name = AS.Name;
        AdjustedActiveSkill.Info = AS.Info;
        AdjustedActiveSkill.DamageHealingPower = AS.DamageHealingPower * (PS.stats.Damage / 100);
        AdjustedActiveSkill.ChiCost = AS.ChiCost;
        AdjustedActiveSkill.Radius = AS.Radius * (PS.stats.Skillradius / 100);
        AdjustedActiveSkill.SingleTarget = AS.SingleTarget;
        AdjustedActiveSkill.SelfTarget = AS.SelfTarget;
        AdjustedActiveSkill.AllyTarget = AS.AllyTarget;
        AdjustedActiveSkill.DoCollide = AS.DoCollide;
        AdjustedActiveSkill.Cooldown = AS.Cooldown / (PS.stats.Cooldownduration / 100);
        AdjustedActiveSkill.Range = AS.Range * (PS.stats.Skillrange / 100);
        AdjustedActiveSkill.ChannelingTime = AS.ChannelingTime;
        AdjustedActiveSkill.CastSpeed = AS.CastSpeed;

        return AdjustedActiveSkill;
    }
    /*
     * 
     * När man träffar en spelare så ska spelarens TakeDamage anropas!
     * 
     * */

    [RPC]
    void OnTriggerEnter(Collider other)
    {
            if (other.gameObject.tag == "Player")
            {
                if (other.GetComponent<PhotonView>().owner.ID != this.GetComponent<PhotonView>().owner.ID &&
                 other.GetComponent<PlayerStats>().teamID != this.spellTeam)
                {


                foreach (PhotonPlayer player in PhotonNetwork.playerList)
                {
                    if (player.ID == other.GetComponent<PhotonView>().owner.ID)
                    {
                        if(!playersHit.Contains(player.ID))
                        {
                            
                                other.GetComponent<PhotonView>().RPC("TakeDamage", player, this.ProjectileActiveSkill.DamageHealingPower,this.GetComponent<PhotonView>().owner.ID);
                                playersHit.Add(player.ID);
                        }                      
                    }
                }
            }
            else
            {
                Debug.Log("Ples uninstall.");
            }
        }
    }
   
}
