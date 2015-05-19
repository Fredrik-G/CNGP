using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

public class PlayerStats : Photon.MonoBehaviour
{
    public Stats stats = new Stats();
    public string Class = "Fire";

    private bool _canBeStunned = true;
    private bool _canBeSlowed = true;
    public bool Stunned = false;
    public List<BuffEffect> EffectList = new List<BuffEffect>();

    public string elementClass = string.Empty;
    public int teamID;
    // Use this for initialization

    void Start()
    {
        // (Name, Info, DamageHealingPower, ChiCost, Radius, SingleTarget, SelfTarget, AllyTarget, DoCollide, Cooldown, CurrentCooldown, Range, ChannelingTime, CastSpeed, NumberOfProjectiles

        // SKÖLD SKILLS
        //stats.SkillList.Add(new ActiveSkill("WaterShield", "Capable waterbenders are able to sustain a large amount of attacks by creating a bubble around themselves and their fellow travelers", 0, 10, 0, false, false, false,false, 10, 0, 0, 0, 0, 0));
        //stats.SkillList[stats.SkillList.Count - 1].BuffEffectList.Add(new BuffEffect("Increase magic resistance by 100", new Effect("WaterShield mr increase", "Magic resistance increase", 3, 3, 100)));

        //stats.SkillList.Add(new ActiveSkill("AirShield", "The most common defensive tactic, though less powerful than the air barrier, it involves circling enemies, suddenly changing direction when attacked and evading by physical movement rather than bending", 5 , 10, 5, false, false, false, false, 10, 0, 0, 0,0, 0));
        //stats.SkillList[stats.SkillList.Count - 1].BuffEffectList.Add(new BuffEffect("Increase physical resistance by 50", new Effect("AirShield pr increase", "Physical resistance increase", 3, 3, 50)));

        //stats.SkillList.Add(new ActiveSkill("EarthArmor", "Earthbenders can bring rocks, dust, pebbles, or crystals around them and mold them to fit their body and create something similar to armor", 0, 10, 0, false, false, false, false, 10, 0, 0, 0, 0, 0));
        //stats.SkillList[stats.SkillList.Count - 1].BuffEffectList.Add(new BuffEffect("Stun immunity", new Effect("EarthArmor stun immunity", "Stun immunity", 3, 3, 0)));
        //stats.SkillList[stats.SkillList.Count - 1].BuffEffectList.Add(new BuffEffect("Slow immunity", new Effect("EarthArmor slow immunity", "Slow immunity", 3, 3, 0)));

        //stats.SkillList.Add(new ActiveSkill("ShieldOfFire", "This creates a protective fire shield around the front of, or the whole body of, a firebender that can deflect attacks and explosions", 0, 15, 0, false, false, false, false, 10, 0, 0, 0, 0, 0));
        //stats.SkillList[stats.SkillList.Count - 1].BuffEffectList.Add(new BuffEffect("Increase armor by 20%", new Effect("ShieldOfFire armor increase", "Armor increase", 3, 3, 20)));


        // SKILLS MED HÖG SKADA/MYCKET EFFEKT MEN ÄVEN HÖG CHI COST OCH COOLDOWN
        //stats.SkillList.Add(new ActiveSkill("Bloodbending", "Bloodbending is a rather sinister application of the principle that water is present in every living organism, thus making them bendable objects themselves", 18, 30, 0.5, false, false, false, false, 20, 0, 15, 0, 0, 0));
        //stats.SkillList[stats.SkillList.Count - 1].BuffEffectList.Add(new BuffEffect("Roots the enemies", new Effect("Bloodbending root", "Root", 3, 3, 0)));

        //stats.SkillList.Add(new ActiveSkill("EarthBomb", "By sending a rock toward the ground, earthbenders can cause massive damage as well as throw their opponents off their feet", 30, 20, 3, false, false, false, false, 18, 0, 10, 0, 6, 1));
        //stats.SkillList[stats.SkillList.Count - 1].BuffEffectList.Add(new BuffEffect("Stuns the enemies", new Effect("EarthBomb stun", "Stun", 1, 1, 0)));

        stats.SkillList.Add(new ActiveSkill("StrongWind", "Advanced air bending ability", 18, 22, 1, false, false, false, false, 20, 0, 8, 0, 20, 1));
        stats.SkillList[stats.SkillList.Count - 1].BuffEffectList.Add(new BuffEffect("Slows the enemies", new Effect("StrongWind tapering slow", "Tapering slow", 1, 1, 50)));


        // SKILLS SOM AKTIVERAS PÅ EN SJÄLV, OCH GER BUFFAR
        //stats.SkillList.Add(new ActiveSkill("IceRamp", "Waterbenders can manipulate ice as a means of short transportation", 0, 4, 0, false, false, false, false, 0.1, 0, 0, 0, 0, 0));
        //stats.SkillList[stats.SkillList.Count - 1].BuffEffectList.Add(new BuffEffect("Increase movement speed by 10%", new Effect("IceRamp ms increase", "Movement speed increase", 0.1, 0.1, 10)));
        //stats.SkillList[stats.SkillList.Count - 1].BuffEffectList.Add(new BuffEffect("Increase health regeneration by 100%", new Effect("IceRamp hr increase", "Health regeneration increase", 0.1, 0.1, 100)));
        //stats.SkillList[stats.SkillList.Count - 1].BuffEffectList.Add(new BuffEffect("Increase healing power by 20%", new Effect("IceRamp hpow increase", "Healing power increase", 0.1, 0.1, 20)));

        //stats.SkillList.Add(new ActiveSkill("RockShoes", "Rock shoes makes the earthbender more stable and therefore stronger", 0, 25, 0, false, false, false, false, 6, 0, 0, 0, 0, 0));
        //stats.SkillList[stats.SkillList.Count - 1].BuffEffectList.Add(new BuffEffect("Decrease movement speed by 10%", new Effect("RockShoes ms decrease", "Movement speed decrease", 6, 6, 10)));
        //stats.SkillList[stats.SkillList.Count - 1].BuffEffectList.Add(new BuffEffect("Increase damage by 15%", new Effect("RockShoes dmg increase", "Damage increase", 6,6,15)));
        //stats.SkillList[stats.SkillList.Count - 1].BuffEffectList.Add(new BuffEffect("Increase armor by 15%", new Effect("Rockshoes armor increase", "Armor increase", 6, 6, 15)));

        //stats.SkillList.Add(new ActiveSkill("EnhancedSpeed", "When used by a skilled airbender, this technique can enable the airbender using it to travel at a speed almost too swift for the naked eye to be able to see properly. A master airbender can use this technique to briefly run across water", 0, 0, 0, false, false, false, false, 4, 0, 4.5, 0, 0, 0));
        //stats.SkillList[stats.SkillList.Count - 1].BuffEffectList.Add(new BuffEffect("Increase damage by 20%", new Effect("EnhancedSeed dmg increase", "Damage increase", 2, 2, 20)));

        //stats.SkillList.Add(new ActiveSkill("JetPropulsion", "Skilled firebending masters are able to conjure huge amounts of flame to propel themselves at high speeds on the ground or through the air", 0, 4, 0, false, false, false, false, 0, 0, 0, 0, 0, 0));
        //stats.SkillList[stats.SkillList.Count - 1].BuffEffectList.Add(new BuffEffect("Increase movement speed by 15%", new Effect("JetPropulsion ms increase", "Movement speed increase", 0.1, 0.1, 15)));
        //stats.SkillList[stats.SkillList.Count - 1].BuffEffectList.Add(new BuffEffect("Increase damage by 10%", new Effect("JetPropulsion dmg increase", "Damage speed increase", 0.1, 0.1, 10)));
        //stats.SkillList[stats.SkillList.Count - 1].BuffEffectList.Add(new BuffEffect("Increase skillrange by 10%", new Effect("JetPropulsion skr increase", "Skillrange increase", 0.1, 0.1, 10)));



        // STARKA SKILL SOM TAR RELATIVT HÖG DAMAGE OCH GER DEBUFFEFFEKTER TILL MOTSTÅNDAREN
        stats.SkillList.Add(new ActiveSkill("Earthquake", "Creates localized earthquakes or fissures to throw opponents off-balance", 7, 20, 1, false, false, true, false, 1.9, 0, 15, 0, 30, 1));
        stats.SkillList[stats.SkillList.Count - 1].BuffEffectList.Add(new BuffEffect("stuns the enemies", new Effect("Earthquake stun", "Stun", 4, 4, 0)));

        //stats.SkillList.Add (new ActiveSkill ("IceFloor", "a waterbender can cover a large area of the ground with ice, trapping their enemies' feet in ice", 10, 20, 7, false,false,false,false,2,0,2.5,0,0,1));
        //stats.SkillList [stats.SkillList.Count - 1].BuffEffectList.Add (new BuffEffect ("stuns the enemies", new Effect ("IceFloor stun", "Stun", 1, 1, 0)));

        //stats.SkillList.Add (new ActiveSkill ("BlazingRing", "Spinning kicks or sweeping arm movements create rings and arcs to slice larger, more widely spaced, or evasive targets", 16, 20, 8, false, false, false, false, 4, 0, 0, 0, 0, 0));

        //stats.SkillList.Add (new ActiveSkill ("AirVortex", "A spinning funnel of air of varying size, the air vortex can be used to trap or disorient opponents", 2, 20, 2, false, false, false, false, 5, 0, 15, 0, 0, 1));
        //stats.SkillList [stats.SkillList.Count - 1].BuffEffectList.Add (new BuffEffect ("slows the enemies", new Effect ("AirVortex slow","Slow",0,0,70)));


        //MAIN SKILLS
        //stats.SkillList.Add(new ActiveSkill("Earthblock", "Earthbenders can bring up blocks of earth and launch them at their enemies", 5, 0, 0.7, false, false, true, false, 2, 0, 10, 0, 9, 1));
        //stats.SkillList.Add(new ActiveSkill("Airblast", "A more offensive manouver involving a direct pulse of strong wind from the hand, feet or mouth", 3.5, 0, 1, false, false, true, false, 1.8, 0, 5, 0, 20, 1));
        stats.SkillList.Add(new ActiveSkill("Firestream", "Basic firebending ability, firebenders can shoot continues streams of fire from there fingertips, fists, palms or legs", 0.1, 0, 0.5, false, false, false, false, 0.025, 0, 4, 0, 15, 1));
        stats.SkillList.Add(new ActiveSkill("Waterbullets", "The waterbullet is a move where a waterbender bends a large amount and shoots in a forcefull blow", 1.5, 0, 0.3, false, false, true, false, 1.9, 0, 9, 0, 12, 5));

    }

    // Update is called once per frame
    void Update()
    {
        if (stats.CurrentChi < stats.MaxChi)
        {
            stats.CurrentChi += ((stats.Chireg / 100) * Time.deltaTime);

        }
        if (stats.CurrentHealthpoints < stats.MaxHealthpoints)
        {
            stats.CurrentHealthpoints += (((stats.Healthreg/100) * stats.HealthRegFactor * Time.deltaTime));
        }

        //Debug.Log("Stunned? " + _canBeStunned + ", Slowed? " + _canBeSlowed);
        //Debug.Log("Damage faktor: " + stats.DamageFactor + ", Armor faktor: " + stats.ArmorFactor);
    }

    void FixedUpdate()
    {
        SetFactorValues();
    }

    private void SetFactorValues()
    {
        stats.MovementSpeedFactor = 1;
        stats.HealthRegFactor = 1;
        stats.HealingPowerFactor = 1;
        stats.DamageFactor = 1;
        stats.ArmorFactor = 1;
        stats.SkillrangeFactor = 1;
        stats.MagicalresistanceFactor = 1;
        stats.PhysicalresistanceFactor = 1;
        _canBeStunned = true;
        _canBeSlowed = true;
        Stunned = false;
        GetComponent<TopDownController>().enabled = true;
        for (int i = 0; i < EffectList.Count; i++)
        {

            if (EffectList[i].Effect.Type.Equals("Slow") && _canBeSlowed)
            {
                stats.MovementSpeedFactor = stats.MovementSpeedFactor * (1 - (EffectList[i].Effect.Amount / 100));
            }

            if (EffectList[i].Effect.Type.Equals("Stun") && _canBeStunned)
            {
                Stunned = true;
            }

            if (EffectList[i].Effect.Type.Equals("Movement speed increase"))
            {
                stats.MovementSpeedFactor *= (1 + (EffectList[i].Effect.Amount / 100));
            }

            if (EffectList[i].Effect.Type.Equals("Health regeneration increase"))
            {
                stats.HealthRegFactor *= (1 + (EffectList[i].Effect.Amount / 100));
            }

            if (EffectList[i].Effect.Type.Equals("Healing power increase"))
            {
                stats.HealingPowerFactor *= (1 + (EffectList[i].Effect.Amount / 100));
            }

            if (EffectList[i].Effect.Type.Equals("Movement speed decrease"))
            {
                stats.MovementSpeedFactor *= (1 - (EffectList[i].Effect.Amount / 100));
            }

            if (EffectList[i].Effect.Type.Equals("Damage increase"))
            {
                stats.DamageFactor *= (1 + (EffectList[i].Effect.Amount / 100));
            }

            if (EffectList[i].Effect.Type.Equals("Armor increase"))
            {
                stats.ArmorFactor *= (1 + (EffectList[i].Effect.Amount / 100));
            }

            if (EffectList[i].Effect.Type.Equals("Skillrange increase"))
            {
                stats.SkillrangeFactor *= (1 + (EffectList[i].Effect.Amount / 100));
            }

            if (EffectList[i].Effect.Type.Equals("Magic resistance increase"))
            {
                stats.MagicalresistanceFactor *= (1 + (EffectList[i].Effect.Amount / 100));
            }

            if (EffectList[i].Effect.Type.Equals("Physical resistance increase"))
            {
                stats.PhysicalresistanceFactor *= (1 + (EffectList[i].Effect.Amount / 100));
            }

            if (EffectList[i].Effect.Type.Equals("Stun immunity"))
            {
                _canBeStunned = false;
            }

            if (EffectList[i].Effect.Type.Equals("Slow immunity"))
            {
                _canBeSlowed = false;
            }


            //if för alla effekttyper..

            EffectList[i].Effect.Timeleft -= Time.deltaTime;


            if (EffectList[i].Effect.Timeleft <= 0)
            {
                EffectList.RemoveAt(i);
            }

        }
    }
    private void PrintDamage(double dmg)
    {
            int x = Random.Range(-3, 4);
            dmg /= (stats.Armor / 100);

            //CombatText = Instantiate(CombatText, transform.position + new Vector3(x / 3f, 2, 0), new Quaternion(0, 50, 0, 0)) as GameObject;
            var CombatText = PhotonNetwork.Instantiate("CombatText", transform.position + new Vector3(x / 3f, 2, 0), new Quaternion(0, 50, 0, 0), 0) as GameObject;
            var controller = CombatText.GetComponent<CombatTextController>();
            controller.SetDamage(dmg);
            Debug.Log(this.GetComponent<PhotonView>().owner.ID + " dmg");
    }
    [RPC]
    public void TakeDamage(double amount, int ownerID)
    {
		Debug.Log ("On takeDamage");
        double dmg = (amount / (stats.Armor / 100));
        stats.CurrentHealthpoints -= dmg;
		Debug.Log (stats.CurrentHealthpoints);
        if (this.stats.CurrentHealthpoints <= 0)
            KillPlayer(ownerID);
    }
    [RPC]
    void SetTeamID(int id)
    {
        elementClass = "Air";
        teamID = id;
    }
    [RPC]
    public void KillPlayer(int idThatKilledMe)
    {
        if (GetComponent<PhotonView>().isMine)
        {
            GameObject.Find("Terrain").GetComponent<tempPlayerStatsStorage>().SetPlayerStats(stats);
            PhotonNetwork.Destroy(this.gameObject);
            var healthBars = GameObject.FindGameObjectsWithTag("Healthbar");
            foreach (GameObject h in healthBars)
            {
                if (h.GetComponent<PhotonView>().owner.ID == this.gameObject.GetComponent<PhotonView>().owner.ID)
                {
                    PhotonNetwork.Destroy(h);
                }

            }

            NetworkManager nm = GameObject.FindObjectOfType<NetworkManager>();

            nm.backgroundCamera.SetActive(true);
            nm.isDead = true;
            nm.respawnTimer = 3f;

            //int numberOfKills = (int)PhotonNetwork.player[idThatKilledMe].customProperties["Kills"];
            var playerList = PhotonNetwork.playerList;
            foreach(PhotonPlayer player in playerList)
            {
                if(player.ID == idThatKilledMe)
                {
                    if (player.customProperties["Kills"] != null)
                    {
                        if (!nm.killsAdded)
                        {
                            MatchController mc = GameObject.Find("Terrain").GetComponent<MatchController>();
                            if(GameObject.Find("_NetworkManager").GetComponent<NetworkManager>().GetTeamID() == 1)
                            {
                                //mc.IncreaseRedTeamPointsOnKill();
                                mc.GetComponent<PhotonView>().RPC("IncreaseRedTeamPointsOnKill", PhotonTargets.MasterClient);
                            }
                            else
                            {
                                //mc.IncreaseBlueTeamPointsOnKill();

                                mc.GetComponent<PhotonView>().RPC("IncreaseBlueTeamPointsOnKill", PhotonTargets.MasterClient);
                            }
                            int numberOfKills = (int)player.customProperties["Kills"];
                            numberOfKills++;
                            ExitGames.Client.Photon.Hashtable hs = new ExitGames.Client.Photon.Hashtable();
                            hs.Add("Kills", numberOfKills);
                            player.SetCustomProperties(hs);
                            nm.killsAdded = true;
                            break;
                        }
                        
                    }
                    
                }
            }
        }
    }
}
