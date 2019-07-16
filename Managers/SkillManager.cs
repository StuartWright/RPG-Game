using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillManager : MonoBehaviour
{
    private static SkillManager instance;
    public static SkillManager Instance
    {
        get { return instance; }
    }
    public bool MagicWeaponEquipped = false;
    public bool PhysicalWeaponEquipped = false;
    public bool ResetSkill = true;

    public GameObject player;
    public Player PlayerRef;

    public int SkillPoints;
    public int Money;

    public int Strength;
    public int Intelligance;
    public int Stamina;
    public int Agility;
    public int WeapPhysicalDamage;
    public int WeapMagicDamage;
    public float CritChance;

    public BaseBolt MagicBoltRef;
    public BoltBounce MagicBoltBounceRef;
    public int BoltPointsNeeded = 3;
    public int MagicBoltLvl = 1;

    public GameObject AOE;
    public MagicAoeSkill AOERef;
    public int AOELvl = 0;
    private int AOEBaseDamage = 10;
    public int AOEPointsNeeded = 1;
    public float AOEDamage
    {
        get { return (AOEBaseDamage + MagicDamage); }
        //devided by 2 because theres 2 triggers on the enemies and its being activated twice(will try and find a better way for this in the future)
    }

    public Dash DashRef;
    public int DashAmount;
    private int BaseDashDamage = 10;
    public int DashLvl = 0;
    public int DashPointsNeeded = 1;
    public float DashDamage
    {
        get { return (BaseDashDamage + PhysicalDamage); }
        //devided by 2 because theres 2 triggers on the enemies and its being activated twice(will try and find a better way for this in the future)
    }

    public int PetLvl = 0;
    public Pet PetRef;
    public GameObject Pet;
    public int PetPointsNeeded = 1;

    public int RageLvl = 0;
    public Rage RageRef;
    public GameObject Rage;
    public int RagePointsNeeded = 1;
    public float PhysicalDamage
    {
        get { return WeapPhysicalDamage + (Stamina);}
    }
    public float MagicDamage
    {
        get { return WeapMagicDamage + Intelligance;}
    }
   
    void Awake ()
    {
		if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        player = GameObject.Find("Player");
        PlayerRef = player.GetComponent<Player>();
        MagicBoltRef.Speed = 10;
        AOERef.Size = 0;
        DashAmount = 300;
        //DashRef.GetComponent<CapsuleCollider>().height = 5;
        DashRef.Size = 0.7f;
        PetRef.Health = 100;
        PetRef.Damage = 10;
        RageRef.Size = 0;
    }
    
    private void Update()
    {
        if(Input.GetKeyDown("q"))
        {
            
            if (MagicWeaponEquipped && AOELvl > 0 && ResetSkill)
            {
                ResetSkill = false;
                if (PlayerRef.Mana >= AOERef.ManaCost)
                {
                    Instantiate(AOE, player.transform.position, transform.rotation);
                    PlayerRef.Mana -= AOERef.ManaCost;
                }
            }
            if(PhysicalWeaponEquipped && DashLvl > 0 && ResetSkill)
            {
                ResetSkill = false;
                if (PlayerRef.Stamina >= DashRef.StaminaCost)
                {
                    PlayerRef.Dash();
                    PlayerRef.Stamina -= DashRef.StaminaCost;
                }
                
            }
            UIManager.instance.UpdatePlayerBars();
            StartCoroutine(SkillCooldown());
        }
        if (Input.GetKeyDown("r"))
        {
            
            if (MagicWeaponEquipped && PetLvl > 0 && ResetSkill)
            {
                ResetSkill = false;
                if (PlayerRef.Mana >= PetRef.ManaCost)
                {
                    Instantiate(Pet, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 3), transform.rotation);
                    PlayerRef.Mana -= PetRef.ManaCost;
                }
            }
            if (PhysicalWeaponEquipped && RageLvl > 0 && ResetSkill)
            {
                ResetSkill = false;
                if (PlayerRef.Stamina >= RageRef.StaminaCost)
                {
                    Instantiate(Rage, player.transform.position, transform.rotation);
                    PlayerRef.Stamina -= RageRef.StaminaCost;
                }

            }
            UIManager.instance.UpdatePlayerBars();
            StartCoroutine(SkillCooldown());
        }
        if (Input.GetKeyDown("3"))
        {
            
        }
    }

    IEnumerator SkillCooldown()
    {
        yield return new WaitForSeconds(2);
        ResetSkill = true;
    }
    public void UpgradeBolt()
    {
        MagicBoltLvl += 1;
        MagicBoltRef.Speed += 1;
        MagicBoltBounceRef.Speed = MagicBoltRef.Speed;
        SkillPoints -= 3;
        Destroy(UIManager.instance.BoltButton);
        Destroy(UIManager.instance.BoltPointsNeededText);
        UIManager.instance.UpdateStats();
        
    }

    public void UpgradeAOE()
    {
        AOELvl += 1;
        AOERef.Size += 1;
        AOEBaseDamage += 15;
        SkillPoints -= AOEPointsNeeded;
        AOEPointsNeeded += 1;
        UIManager.instance.UpdateStats();
        UIManager.instance.CheckIcons();
    }

    public void UpgradeDash()
    {
        DashLvl += 1;
        DashAmount += 50;
        DashRef.Size += 0.3f;
        BaseDashDamage += 15;
        SkillPoints -= DashPointsNeeded;
        DashPointsNeeded += 1;
        UIManager.instance.UpdateStats();
        UIManager.instance.CheckIcons();
    }

    public void UpgradePet()
    {
        PetLvl += 1;
        PetRef.Health += 20;
        PetRef.Damage += 10;
        SkillPoints -= PetPointsNeeded;
        PetPointsNeeded += 1;
        UIManager.instance.UpdateStats();
        UIManager.instance.CheckIcons();
    }

    public void UpgradeRage()
    {
        RageLvl += 1;
        RageRef.Size += 1;
        SkillPoints -= RagePointsNeeded;
        RagePointsNeeded += 1;
        UIManager.instance.UpdateStats();
        UIManager.instance.CheckIcons();
    }
    public void UpgradeStrength()
    {
        if(SkillPoints >= 1)
        {
            PlayerRef.MaxHealth += 10;
            Strength += 1;
            SkillPoints -= 1;
            UIManager.instance.UpdateStats();
        }
    }
    public void UpgradeIntelligence()
    {
        if (SkillPoints >= 1)
        {
            PlayerRef.MaxMana += 10;
            Intelligance += 1;
            SkillPoints -= 1;
            UIManager.instance.UpdateStats();
        }
    }
    public void UpgradeStamina()
    {
        if (SkillPoints >= 1)
        {
            PlayerRef.MaxStamina += 10;
            Stamina += 1;
            SkillPoints -= 1;
            UIManager.instance.UpdateStats(); 
        }
    }
    public void UpgradeAgility()
    {
        if (SkillPoints >= 1)
        {
            Agility += 1;
            SkillPoints -= 1;
            UIManager.instance.UpdateStats();
        }
    }
    public void UpgradeCritChance()
    {
        if (SkillPoints >= 1)
        {
            CritChance += 5;
            SkillPoints -= 1;
            UIManager.instance.UpdateStats();
        }
    }
}
