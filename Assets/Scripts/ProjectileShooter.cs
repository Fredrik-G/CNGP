using UnityEngine;
using System.Collections;
using System;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using Engine;

public class ProjectileShooter : MonoBehaviour
{
    public Transform spawn;
    public AudioSource audioSource;
    public VoiceController voiceController;
    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        voiceController = GetComponent<VoiceController>();
    }

    /*
     * 
     * KillPlayer kod ---- Dödar spelaren!
     * 
     * Ta bort killplayer från update här inne!
     * 
     * 
     * */
    void KillPlayer()
    {
        NetworkManager nm = GameObject.FindObjectOfType<NetworkManager>();
        nm.backgroundCamera.SetActive(true);
        nm.isDead = true;
        nm.respawnTimer = 3f;
        PhotonNetwork.Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {

        NetworkManager nm = GameObject.FindObjectOfType<NetworkManager>();

        var PlayerStats = gameObject.GetComponent<PlayerStats>();

        if (Input.GetButton("LeftClick"))
        {
            if ((PlayerStats.stats.SkillList[0].CurrentCooldown <= 0))
            {
                SkillCast(0, PlayerStats);
            }
            else
            {
                voiceController.PlayCooldownVoice();
            }
            
        }

        if (Input.GetButton("RightClick"))
        {
            if ((PlayerStats.stats.SkillList[1].CurrentCooldown <= 0))
            {
                SkillCast(1, PlayerStats);
            }
            else
            {
                voiceController.PlayCooldownVoice();
            }
        }

        if (Input.GetButton("ButtonQ"))
        {
            if ((PlayerStats.stats.SkillList[2].CurrentCooldown <= 0))
            {
                SkillCast(2, PlayerStats);
            }
            else
            {
                voiceController.PlayCooldownVoice();
            }
        }

        if (Input.GetButton("ButtonE"))
        {
            if ((PlayerStats.stats.SkillList[3].CurrentCooldown <= 0))
            {
                SkillCast(3, PlayerStats);
            }
            else
            {
                voiceController.PlayCooldownVoice();
            }
        }


        for (int i = 0; i < PlayerStats.stats.SkillList.Count; i++)
        {
            PlayerStats.stats.SkillList[i].CurrentCooldown -= Time.deltaTime;
        }


    }

    //Anpassad för de fyra mainskillsen
    void SkillCast(int skillSlot, PlayerStats playerStats)
    {



        var oldSpawn = spawn.localRotation;
        if (playerStats.stats.SkillList[skillSlot].Name == "Waterbullets")
        {
            spawn.transform.Rotate(Vector3.up, -15);
        }
        NetworkManager nm = GameObject.FindObjectOfType<NetworkManager>();
        for (int i = 0; i < playerStats.stats.SkillList[skillSlot].NumberOfProjectiles; i++)
        {


            var projectile = PhotonNetwork.Instantiate("projectile", spawn.position, Quaternion.Euler(0, 0, 0), 0) as GameObject;
            
            var Controller = projectile.GetComponent<ProjectileSpell>();

            Controller.spellTeam = playerStats.teamID;
            Controller.Init(Controller.AdjustActiveSkillValues(playerStats.stats.SkillList[skillSlot], playerStats));
            playerStats.stats.SkillList[skillSlot].CurrentCooldown = Controller.ProjectileActiveSkill.Cooldown;
            Controller.Name = playerStats.stats.SkillList[skillSlot].Name;
            Controller.Scale = Controller.ProjectileActiveSkill.Radius;

            projectile.transform.localScale = new Vector3((float)Controller.Scale, (float)Controller.Scale, (float)Controller.Scale);

            if (playerStats.stats.SkillList[skillSlot].Name == "Waterbullets")
            {
                spawn.transform.Rotate(Vector3.up, 5);
            }
            var rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = spawn.transform.forward * (Convert.ToInt32((Controller.ProjectileActiveSkill.CastSpeed)));
            StartCoroutine(WaitTimeThenDestory(Controller.ProjectileActiveSkill.Range / Controller.ProjectileActiveSkill.CastSpeed, projectile));

        }

        spawn.localRotation = oldSpawn;
    }

    IEnumerator WaitTimeThenDestory(double duration, GameObject obj)
    {
        yield return new WaitForSeconds((float)duration);
        PhotonNetwork.Destroy(obj);
    }
    //Anpassad för earthquake atm
    void SkillOnMouseCast(int skillSlot, PlayerStats playerStats)
    {

        if (playerStats.stats.CurrentChi >= playerStats.stats.SkillList[skillSlot].ChiCost)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, (float)playerStats.stats.SkillList[skillSlot].Range))
            {
                var cylinderPosition = new Vector3(hit.point.x, 0, hit.point.z);


                var cylinder = PhotonNetwork.Instantiate("cylinder", cylinderPosition, Quaternion.Euler(0, 0, 0), 0) as GameObject;
                NetworkManager nm = GameObject.FindObjectOfType<NetworkManager>();

                var Controller = cylinder.GetComponent<CylinderSpell>();

                Controller.spellTeam = playerStats.teamID;
                Controller.Init(Controller.AdjustActiveSkillValues(playerStats.stats.SkillList[skillSlot], playerStats));
                playerStats.stats.SkillList[skillSlot].CurrentCooldown = Controller.CylinderActiveSkill.Cooldown;

                var rb = cylinder.GetComponent<Rigidbody>();
                rb.velocity = cylinder.transform.up * 2;

                playerStats.stats.CurrentChi -= playerStats.stats.SkillList[skillSlot].ChiCost;
                StartCoroutine(WaitTimeThenDestory(2, cylinder.gameObject));
            }
        }
        else
        {
            voiceController.PlayChiCostVoice();
        }
    }
}
