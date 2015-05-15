using UnityEngine;
using System.Collections;
using Engine;

public class PlayerStats : Photon.MonoBehaviour
{
    public Stats stats = new Stats();
    public int teamID;
    public GameObject HealthAndChiBarPrefab;
    public string elementClass = string.Empty;
    // Use this for initialization
    [RPC]
    void SetTeamID(int id)
    {
        elementClass = "Air";
        teamID = id;
    }
    void Start()
    {
        //stats.SkillList.Add(new ActiveSkill("Earthquake", "Creates localized earthquakes or fissures to throw opponents off-balance", 7, 20, 1, false, false, true, false, 1.9, 0, 15, 0, 30, 10));
        stats.SkillList.Add(new ActiveSkill("Earthblock", "Earthbenders can bring up blocks of earth and launch them at their enemies", 5, 0, 0.7, false, false, true, false, 2, 0, 10, 0, 9, 1));
        stats.SkillList.Add(new ActiveSkill("Airblast", "A more offensive manouver involving a direct pulse of strong wind from the hand, feet or mouth", 3.5, 0, 1, false, false, true, false, 1.8, 0, 5, 0, 20, 1));
        stats.SkillList.Add(new ActiveSkill("Firestream", "Basic firebending ability, firebenders can shoot continues streams of fire from there fingertips, fists, palms or legs", 0.1, 0, 0.5, false, false, false, false, 0.025, 0, 4, 0, 15, 1));
        stats.SkillList.Add(new ActiveSkill("Waterbullets", "The waterbullet is a move where a waterbender bends a large amount and shoots in a forcefull blow", 1.5, 0, 0.3, false, false, true, false, 1.9, 0, 9, 0, 12, 5));
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.CurrentChi < stats.MaxChi)
            stats.CurrentChi += ((stats.Chireg / 100) * Time.deltaTime);

        if (stats.CurrentHealthpoints < stats.MaxHealthpoints)
            stats.CurrentHealthpoints += ((stats.Healthreg / 100) * Time.deltaTime);

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
        double dmg = (amount / (stats.Armor / 100));
        stats.CurrentHealthpoints -= dmg;
        //PrintDamage(dmg);
        if (this.stats.CurrentHealthpoints <= 0)
            KillPlayer(ownerID);
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
    [RPC]
    public void AddKills()
    {

    }
}