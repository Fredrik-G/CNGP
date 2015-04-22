using UnityEngine;
using System.Collections;
using Engine;

public class PlayerStats : MonoBehaviour {

	public Stats stats = new Stats();


	// Use this for initialization
	void Start () {
		//stats.SkillList.Add (new ActiveSkill ("Earthquake", "Creates localized earthquakes or fissures to throw opponents off-balance", 7, 20, 1, false, false, true, false, 1.9, 0, 15, 0, 30, 10));
		//stats.SkillList[0].BuffEffectList.Add(new BuffEffect("stuns the enemies", new Effect(0.5,0,0,0,0,0)));
		//stats.SkillList.Add(new ActiveSkill ("Earthblock", "Earthbenders can bring up blocks of earth and launch them at their enemies", 5, 0, 0.7, false, false, true, false, 2,0, 10, 0, 9, 1));
		stats.SkillList.Add (new ActiveSkill ("Ice floor", "a waterbender can cover a large area of the ground with ice, trapping their enemies' feet in ice", 10, 20, 2.5, false,false,false,false,2,0,2.5,0,7,1));
		stats.SkillList [0].BuffEffectList.Add (new BuffEffect ("stuns the enemies", new Effect (1, 0, 0, 0, 0, 0)));
		stats.SkillList.Add(new ActiveSkill ("Airblast", "A more offensive manouver involving a direct pulse of strong wind from the hand, feet or mouth", 3.5, 0, 1, false, false, true, false, 1.8, 0, 5, 0, 20, 1));
		stats.SkillList.Add(new ActiveSkill ("Firestream", "Basic firebending ability, firebenders can shoot continues streams of fire from there fingertips, fists, palms or legs", 0.2, 0, 0.5, false, false, false, false, 0.05, 0, 4, 0, 15,1));
		stats.SkillList.Add (new ActiveSkill ("Waterbullets", "The waterbullet is a move where a waterbender bends a large amount and shoots in a forcefull blow", 1.5, 0, 0.3, false, false, true, false, 1.9, 0, 9, 0, 12, 5));

	}
	
	// Update is called once per frame
	void Update () {
		if (stats.CurrentChi < stats.MaxChi) {
			stats.CurrentChi += ((stats.Chireg / 100) * Time.deltaTime);
		}
	}


    private void TakeDamage(double amount)
    {
        stats.CurrentHealthpoints -= (amount / (stats.Armor / 100));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("projectile"))
        {
            var projectile = other.gameObject.GetComponent<ProjectileSpell>();

            if (projectile != null)
            {
                TakeDamage(projectile.ProjectileActiveSkill.DamageHealingPower);
            }
        }

        if (other.gameObject.CompareTag("cylinder"))
        {
            var cylinder = other.gameObject.GetComponent<CylinderSpell>();

            if (cylinder != null)
            {
                TakeDamage(cylinder.CylinderActiveSkill.DamageHealingPower);
            }
        }
    }
}
