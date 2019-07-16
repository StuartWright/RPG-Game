using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour, IDamage
{
    public delegate void ShopEvent();
    public event ShopEvent PlayerCanBuy;
    public event ShopEvent PlayerCantBuy;

    private SkillManager SM;
    public UIManager UI;
    public Text PlayerHealth;
    public Text PlayerMana;
    public Text PlayerStamina;
    public Text PlayerExp;
    public Text PlayerLevelText;
    public Image HPBar;
    public Image MPBar;
    public Image StamBar;
    public BaseMelee SwordRef;
    public BaseMagicWeap MagicRef;
    public Inventory InventoryRef;
    public Inventory PotionInventoryRef;
    public GameObject DashRef;
    public GameObject MerchantRef;
    public GameObject InventoryPanel;
    public GameObject SkillMenuPanel;
    public GameObject LevelUpPopup;
    public GameObject LevelUpEffect;
    public GameObject PlayerBase;
    private PlayerController PC;
    private PlayerRotate PR;
    public AttackBox AttackBoxScript;
    public Animator LevelUpPopUpAnim;
    public Vector3 StartPos;
    private GameObject Cam;

    public float Health, Mana, Stamina;
    public float MaxHealth, MaxMana, MaxStamina;
    public float Experiance = 0;
    public float ExperianceToLvl = 20;
    public float EffectTimer;
    public int PlayerLevel = 1;

    private bool SkillMenuOpen = false;   
    public bool CanAttack = true;
    private bool InventoryOpen = false;
    private bool IsShop = false;
    private bool ShopOpen = false;
    private bool LeveledUp = false;

    public GameObject TESTDOORS;
    void Start ()
    {
        SM = SkillManager.Instance;
        UI = UIManager.instance;
        //InventoryObject = GameObject.Find("Inventory");
        //InventoryObject.SetActive(false);
        PlayerExp.text = "Experience: " + Experiance + "/" + ExperianceToLvl;
        PlayerLevelText.text = "" + PlayerLevel;
        GameObject Merchant = GameObject.Find("MerchantMan");
        if(MerchantRef != null)
        MerchantRef = Merchant.transform.GetChild(0).gameObject;
        PC = GameObject.Find("PlayerBase").GetComponent<PlayerController>();
        PR = GameObject.Find("PlayerBase").GetComponent<PlayerRotate>();
        LevelUpPopUpAnim = LevelUpPopup.GetComponent<Animator>();
        PlayerBase = GameObject.Find("PlayerBase");
        Cam = GameObject.Find("CameraArm");
        StartCoroutine(StatRegen());
    }
    
    void Update ()
    {
        //UIManager.instance.UpdatePlayerBars();
        HPBar.fillAmount = Health / MaxHealth;
        MPBar.fillAmount = Mana / MaxMana;
        StamBar.fillAmount = Stamina / MaxStamina;

        if (Input.GetKeyDown("1"))
            PotionInventoryRef.Items[0].Use(PotionInventoryRef.Items[0]);   
        if (Input.GetKeyDown("2"))
            PotionInventoryRef.Items[1].Use(PotionInventoryRef.Items[1]);
        if (Input.GetKeyDown("3"))
            PotionInventoryRef.Items[2].Use(PotionInventoryRef.Items[2]);
        if (Input.GetKeyDown("4"))
            PotionInventoryRef.Items[3].Use(PotionInventoryRef.Items[3]);
        if (Input.GetKeyDown("5"))
            PotionInventoryRef.Items[4].Use(PotionInventoryRef.Items[4]);
        if (Input.GetKeyDown("6"))
            PotionInventoryRef.Items[5].Use(PotionInventoryRef.Items[5]);

        //TESTING WILL REMOVE
        if (Input.GetKeyDown("/"))
            TESTDOORS.SetActive(true);

        if (Input.GetKeyDown("i") && InventoryOpen == false)
        {
            Time.timeScale = 0;
            CanAttack = false;
            InventoryPanel.SetActive(true);
            SkillMenuPanel.SetActive(false);
            SkillMenuOpen = false;
            InventoryOpen = true;
            //UI.UpdateMoney();
        }
        else if(Input.GetKeyDown("i") && InventoryOpen == true)
        {
            Time.timeScale = 1;
            CanAttack = true;
            InventoryPanel.SetActive(false);
            InventoryOpen = false;
        }
        if (Input.GetKeyDown("u") && SkillMenuOpen == false)
        {
            Time.timeScale = 0;
            CanAttack = false;
            SkillMenuPanel.SetActive(true);
            InventoryPanel.SetActive(false);
            InventoryOpen = false;
            SkillMenuOpen = true;
            UI.UpdateStats();
        }
        else if (Input.GetKeyDown("u") && SkillMenuOpen == true)
        {
            Time.timeScale = 1;
            CanAttack = true;
            SkillMenuPanel.SetActive(false);
            SkillMenuOpen = false;
        }
        if(IsShop)
        {
            if (Input.GetKeyDown("e") && !ShopOpen)
            {
                MerchantRef.SetActive(true);
                InventoryPanel.SetActive(true);
                if(PlayerCanBuy != null)
                PlayerCanBuy();
                ShopOpen = true;
            }
            else if (Input.GetKeyDown("e") && ShopOpen)
            {
                MerchantRef.SetActive(false);
                InventoryPanel.SetActive(false);
                if(PlayerCantBuy != null)
                PlayerCantBuy();
                ShopOpen = false;
            }   
        }
        //turn the level up effect off:
        if(LeveledUp)
        {
            EffectTimer -= Time.deltaTime;
            if (EffectTimer <= 0)
            {
                LevelUpEffect.SetActive(false);
                LeveledUp = false;
            }
                
        }
    }

    public void TakeDamage(float DamAmount)
    {
        DamAmount -= (DamAmount * SM.Strength / 100);
        Health -= DamAmount;
        UI.UpdatePlayerBars();
        // PlayerHealth.text = "Health: " + Health;
        if (Health <= 0)
        {
            //HPBar.color = Color.Lerp(Color.red, Color.green, Health / MaxHealth);
            MonoBehaviour[] list = GameObject.FindObjectsOfType<MonoBehaviour>();
            foreach (MonoBehaviour mb in list)
            {
                if (mb is IDifficulty && mb != this)
                {
                    Destroy(mb.gameObject);
                }
            }
            Destroy(Cam);
            Destroy(gameObject);
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void SetDamageText(float DamAmount, bool Crit) { }

    IEnumerator StatRegen()
    {
        
        if(Health != MaxHealth)
        {
            Health += (SM.Strength / 2);
            if (Health > MaxHealth)
                Health = MaxHealth;
        }

        if (Mana != MaxMana)
        {
            Mana += (SM.Intelligance / 2);
            if (Mana > MaxMana)
                Mana = MaxMana;
        }

        if (Stamina != MaxStamina)
        {
            Stamina += (SM.Stamina / 2);
            if (Stamina > MaxStamina)
                Stamina = MaxStamina;
        }
        if(UI != null)
            UI.UpdatePlayerBars();
        yield return new WaitForSeconds(1);
        StartCoroutine(StatRegen());
    }
    
    public void CheckIfLvlUp()
    {
        PlayerExp.text = "Experience: " + Experiance + "/" + ExperianceToLvl;
        if (Experiance >= ExperianceToLvl)
        {
            LevelUpPopup.SetActive(true);
            LevelUpEffect.SetActive(true);
            Health = MaxHealth;
            Mana = MaxMana;
            Stamina = MaxStamina;
            EffectTimer = 5;
            LeveledUp = true;
            StartCoroutine(WaitForAnimation());
            PlayerLevel++;
            Experiance = 0;
            ExperianceToLvl += 10;
            PlayerExp.text = "Experience: " + Experiance + "/" + ExperianceToLvl;
            SM.SkillPoints += 2;
            PlayerLevelText.text = "" + PlayerLevel;
            DiffucltyIncrease();
            ItemManager.instance.minValue++;
            ItemManager.instance.maxValue++;
            UI.UpdatePlayerBars();
        }
    }
    private void DiffucltyIncrease()
    {
        MonoBehaviour[] list = GameObject.FindObjectsOfType<MonoBehaviour>();
        foreach (MonoBehaviour mb in list)
        {
            if (mb is IDifficulty)
            {
                IDifficulty Difficulty = (IDifficulty)mb;
                Difficulty.IncreaseDifficulty();
            }
        }
    }
    private IEnumerator WaitForAnimation()
    {

        AnimatorClipInfo[] ClipInfo = LevelUpPopUpAnim.GetCurrentAnimatorClipInfo(0);
        //yield return new WaitForSeconds(0.1f);

        yield return new WaitForSeconds(ClipInfo[0].clip.length);
        LevelUpPopup.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup" && InventoryRef.IsFull() == false)
        {
            
            Pickup pickup = other.GetComponent<Pickup>();

            Items newItem = ItemManager.instance.createItem(this, pickup);
            if (newItem.IsPotion)
                PotionInventoryRef.AddPotion(newItem);
            else
            InventoryRef.AddItem(newItem);
            // InventoryRef.AddItem(other.gameObject, other.GetComponent<Items>());
            Destroy(other.gameObject);
            
        }
        if (other.tag == "Shop")
            IsShop = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Shop")
        {
            IsShop = false;
            MerchantRef.SetActive(false);
            InventoryPanel.SetActive(false);
            ShopOpen = false;
            PlayerCantBuy();
        }     
    }
    public void Dash()
    {
        //GetComponent<Rigidbody>().AddForce(transform.forward * 400);
        StartPos = PlayerBase.transform.position;
        PlayerBase.transform.Translate(Vector3.forward * SM.DashAmount * Time.deltaTime);
        Vector3 EndPos = PlayerBase.transform.position;
        Vector3 MidPos = (StartPos + EndPos) / 2f;
        Instantiate(DashRef, MidPos, transform.rotation);
    }

    //ANIMATION EVENTS:
    public void EnableAttackBox()
    {
        AttackBoxScript.CanSwing = true;
    }
    public void DisableAttackBox()
    {
        AttackBoxScript.CanSwing = false; //Incase player doesnt hit anything.
    }
    public void AnimationEvent()
    {
        SwordRef.canSwing = true;
        MagicRef.canSwing = true;
    }

    public void StopAnim()
    {
        if (SM.PhysicalWeaponEquipped)
            SwordRef.anim.SetBool("Swing", false);
        if (SM.MagicWeaponEquipped)
            MagicRef.anim.SetBool("Swing", false);
    }
    public void CanMove()
    {
        PC.canMove = true;
        PR.CanTurn = true;
    }
}
