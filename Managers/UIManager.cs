using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    public Text StrText;
    public Text IntText;
    public Text StamText;
    public Text AgiText;
    public Text CritChanceText;
    public Text PhysicalDamageText;
    public Text MagicDamageText;
    public Text MagicBoltText;
    public Text AOEText;
    public Text DashText;
    public Text PetText;
    public Text RageText;
    public Text SkillPointsText;
    public Text MoneyText;

    public Text DashPointsNeededText;
    public Text AOEPointsNeededText;
    public Text PetPointsNeededText;
    public Text RagePointsNeededText;
    public Text BoltPointsNeededText;

    public GameObject MagicIcons;
    public GameObject PhysicalIcons;

    public GameObject StatButtons;
    public GameObject AOEButton;
    public GameObject DashButton;
    public GameObject PetButton;
    public GameObject RageButton;
    public GameObject BoltButton;

    private SkillManager SM;
    public Player PlayerRef;

    public GameObject[] SkillIcons = new GameObject[6];
    // Use this for initialization
    void Awake ()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        PlayerRef = GameObject.Find("Player").GetComponent<Player>();
        SM = SkillManager.Instance;
        UpdatePlayerBars();
    }
    public void UpdateMoney()
    {
        MoneyText.text = "Gold: " + SM.Money;
    }
    
    public void UpdateStats()
    {
        StrText.text = "Strength: " + SM.Strength;
        IntText.text = "Intelligance: " + SM.Intelligance;
        StamText.text = "Stamina: " + SM.Stamina;
        AgiText.text = "Agility: " + SM.Agility;
        PhysicalDamageText.text = "Physical Damage: " + (int)SM.PhysicalDamage;
        MagicDamageText.text = "Magic Damage: " + (int)SM.MagicDamage;
        CritChanceText.text = "Critical Chance: " + SM.CritChance + "%";
        SkillPointsText.text = "Skill Points: " + SM.SkillPoints;
        MagicBoltText.text = "Magic Bolt Level: " + SM.MagicBoltLvl;
        AOEText.text = "Fire Ring Level: " + SM.AOELvl;
        DashText.text = "Dash Level: " + SM.DashLvl;
        PetText.text = "Pet Level: " + SM.PetLvl;
        RageText.text = "Rage Level: " + SM.RageLvl;
        DashPointsNeededText.text = "Skill Points required: " + SM.DashPointsNeeded;
        AOEPointsNeededText.text = "Skill Points required: " + SM.AOEPointsNeeded;
        PetPointsNeededText.text = "Skill Points required: " + SM.PetPointsNeeded;
        RagePointsNeededText.text = "Skill Points required: " + SM.RagePointsNeeded;
        if(BoltPointsNeededText != null)
        BoltPointsNeededText.text = "Skill Points required: " + SM.BoltPointsNeeded;
        CheckUpgradeButtons();
    }

    public void UpdatePlayerBars()
    {
        if(PlayerRef!= null)
        {
            PlayerRef.PlayerHealth.text = (int)PlayerRef.Health + " / " + (int)PlayerRef.MaxHealth;
            PlayerRef.PlayerMana.text = (int)PlayerRef.Mana + " / " + (int)PlayerRef.MaxMana;
            PlayerRef.PlayerStamina.text = (int)PlayerRef.Stamina + " / " + (int)PlayerRef.MaxStamina;
        }
    }
    private void CheckUpgradeButtons()
    {
        if (SM.SkillPoints >= 1)
            StatButtons.SetActive(true);
        else
            StatButtons.SetActive(false);

        if (SM.SkillPoints >= SM.AOEPointsNeeded) {AOEButton.SetActive(true); AOEPointsNeededText.gameObject.SetActive(false); }
        else { AOEButton.SetActive(false); AOEPointsNeededText.gameObject.SetActive(true); }
        
        if (SM.SkillPoints >= SM.DashPointsNeeded) {DashButton.SetActive(true); DashPointsNeededText.gameObject.SetActive(false); }
        else { DashButton.SetActive(false); DashPointsNeededText.gameObject.SetActive(true); }
            
        if (SM.SkillPoints >= SM.PetPointsNeeded) { PetButton.SetActive(true); PetPointsNeededText.gameObject.SetActive(false); }
        else { PetButton.SetActive(false); PetPointsNeededText.gameObject.SetActive(true); }
            
        if (SM.SkillPoints >= SM.RagePointsNeeded) { RageButton.SetActive(true); RagePointsNeededText.gameObject.SetActive(false); }    
        else{ RageButton.SetActive(false); RagePointsNeededText.gameObject.SetActive(true); }
        if(BoltPointsNeededText != null && BoltButton != null)
        {
            if (SM.SkillPoints >= SM.BoltPointsNeeded) { BoltButton.SetActive(true); BoltPointsNeededText.gameObject.SetActive(false); }
            else { BoltButton.SetActive(false); BoltPointsNeededText.gameObject.SetActive(true); }
        }
    }
    public void CheckIcons()
    {
        if (SM.PhysicalWeaponEquipped == false && SM.MagicWeaponEquipped == false)
        {
            MagicIcons.SetActive(false);
            PhysicalIcons.SetActive(false);
        }
        if (SM.MagicWeaponEquipped)
        {
            MagicIcons.SetActive(true);
            PhysicalIcons.SetActive(false);
        }
        if (SM.PhysicalWeaponEquipped)
        {
            MagicIcons.SetActive(false);
            PhysicalIcons.SetActive(true);
        }
        CheckMagicIcons();
    }

    private void CheckMagicIcons()
    {
        if(SM.AOELvl > 0)
            SkillIcons[0].SetActive(true);

        if (SM.PetLvl > 0)
            SkillIcons[1].SetActive(true);

        if (SM.DashLvl > 0)
            SkillIcons[3].SetActive(true);

        if (SM.RageLvl > 0)
            SkillIcons[4].SetActive(true);
    }
   
}
