using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public enum DamageEffects
 {
    FireEffect,
    ElectricEffect
 }

public class BaseEnemy : MonoBehaviour, IDifficulty, IDamage
{
    public delegate void DeathEvent(BaseEnemy sender);
    public event DeathEvent OnDeath;

    public float AttackRadius = 15;
    public float Health = 100;
    public float MaxHealth = 100;   
    public float Damage = 5;
    public float RangedDamage = 10;
    protected float Range = 1.8f;
    public float Speed;
    public float MaxSpeed;
    public float AttackTimer = 3;

    public int ExpToGive = 10;

    public GameObject EnemyToLookAt;
    public GameObject Player;
    public GameObject Pet;
    public GameObject Enemy;
    public GameObject FireEffect;
    public GameObject ElectricEffect; 
    public GameObject RageTarget;
    public GameObject AttackPoint;
    private GameObject EnemyCanvas;

    public Animator anim;
    private Animator Textanim;
    public EnemyAttackBox AttackBoxRef;
    protected Player PlayerScript;
    protected Rigidbody rb;
    public Text HealthText;
    private Text DamText;
    public Collider[] hit;
    public Image HPBar;

    protected bool StartTimer = false;
    protected bool StillTouching = false;
    public bool IsDead = false;
    public bool canAttack = false;
    public bool HasSwung = false;
    public bool UnderAttack = false;
    private bool NextToPlayer = false;
    public bool IsBossEnemy;
    public bool Attacked = false;

    public DamageEffects Effects;
    public static readonly int AttackAnimId = Animator.StringToHash("Attack");
    private static readonly int MovingAnimId = Animator.StringToHash("Moving");

    public void Start()
    {
        
        Enemy = this.gameObject;
        Player = GameObject.Find("Player");
        PlayerScript = Player.GetComponent<Player>();
        EnemyCanvas = transform.Find("EnemyC").gameObject;
        if(!this.GetComponent<Boss>())
        HPBar = transform.Find("EnemyC").Find("HealthBar").GetComponent<Image>();
        HealthText.text = Health + " / " + MaxHealth;
        anim = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();
        GameObject DamageTextTrans = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;
        Textanim = DamageTextTrans.GetComponent<Animator>();
        DamText = DamageTextTrans.GetComponent<Text>();
        DamText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (EnemyToLookAt == null || EnemyToLookAt == this.gameObject)
        {
            EnemyToLookAt = Player;
        }


        float PlayerDistance = Vector3.Distance(EnemyToLookAt.transform.position, transform.position);
        if ((PlayerDistance <= AttackRadius && PlayerDistance >= Range) || UnderAttack)
        {
            NextToPlayer = false;
            if (PlayerDistance <= AttackRadius)
                UnderAttack = false;//UNDER ATTACK ALLOWS THE ENEMY TO TARGET THE PLAYER WHEN THEYVE BEEN ATTACKED. 
            anim.SetBool(MovingAnimId, true);
            Speed = MaxSpeed;
            transform.position = Vector3.MoveTowards(transform.position, EnemyToLookAt.transform.position, Speed * Time.deltaTime);
            transform.LookAt(EnemyToLookAt.transform);
            anim.SetBool(AttackAnimId, false);
            canAttack = true;
        }
        else if (PlayerDistance <= AttackRadius && PlayerDistance <= Range)
        {
            NextToPlayer = true;
            anim.SetBool(MovingAnimId, false);
            transform.LookAt(EnemyToLookAt.transform);
            Speed = 0;
            if (canAttack)
            {
                Swing();
                canAttack = false;
            }

        }
        else
        {
            anim.SetBool(MovingAnimId, false);
            //canAttack = true;
        }



    }
    public void Swing()
    {
        GetDamageAmount();
        anim.SetBool(AttackAnimId, true);   
    }
    public void EnableCanAttack()
    {
        AttackBoxRef.CanSwing = true;
    }
   public void ResetCanAttack()
    {
        canAttack = true;
    }
    public void ResetHitReact()
    {
        anim.SetBool("Hit", false);
    }
    public void TakeDamage(float DamAmount)
    {
        //anim.SetBool("Hit", true);
        if (GetComponent<Boss>())
            GetComponent<Boss>().AtHalfHealth();
        if(NextToPlayer == false)
        UnderAttack = true;
        Health -= DamAmount;
        HPBar.fillAmount = Health / MaxHealth;
        HealthText.text = (int)Health + " / " + MaxHealth;
        if (Health <= 0)
        {
            PlayerScript.Experiance += ExpToGive;
            PlayerScript.CheckIfLvlUp();
            DropLoot();
            anim.SetBool("Attack", false);
            anim.SetBool("Death", true);
            Death();
        }
    }

    protected void Death()
    {
        if (IsBossEnemy)
        {
            Level3Manager.Instance.EnemyCounter++;
            if (Level3Manager.Instance.EnemyCounter >= 5)
                Level3Manager.Instance.Round2();
        }
            
        rb.useGravity = false;
        //HPBar.gameObject.SetActive(false);
        EnemyCanvas.SetActive(false);
        this.GetComponent<BaseEnemy>().enabled = false;
        this.GetComponent<CapsuleCollider>().enabled = false;
        this.GetComponent<BoxCollider>().enabled = false;
        Destroy(this.GetComponent<Rigidbody>());
        FireEffect.SetActive(false);
        ElectricEffect.SetActive(false);
        IsDead = true;

        if (OnDeath != null)
            OnDeath(this);
        if(GetComponent<Boss>())
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void SetDamageText(float DamAmount, bool Crit)
    {
        //StopCoroutine(WaitForAnimation());

        //Textanim.StartPlayback();

        DamText.gameObject.SetActive(true);
        if (Crit)
            DamText.color = Color.yellow;
        else
            DamText.color = Color.red;
        DamText.text = "" + (int)DamAmount; 
        StartCoroutine(WaitForAnimation());
    }
    private IEnumerator WaitForAnimation()
    {
        if (Textanim.isActiveAndEnabled)
        {
            AnimatorClipInfo[] ClipInfo = Textanim.GetCurrentAnimatorClipInfo(0);

             if (ClipInfo.Length > 0)
             yield return new WaitForSeconds(ClipInfo[0].clip.length);
        }
        
        DamText.gameObject.SetActive(false);
    }
    
    protected void OnBecameInvisible()
    {
        if(IsDead)
        {
            Destroy(gameObject);
        }
    }
    public void OnPetDied()
    {
        EnemyToLookAt = Player;
        Pet.GetComponent<Pet>().OnDeath -= OnPetDied;
    }
    public void DropLoot()
    {
        int randomNum = Random.Range(0, 100);
        Vector3 Pos = new Vector3(transform.position.x, 1, transform.position.z);
        if (randomNum <= 50)
        Instantiate(ItemManager.instance.RandomItem(), Pos + new Vector3(0,0,1), Quaternion.Euler(new Vector3(-90,10,0)));
    }


    protected void GetDamageAmount()
    {
        AttackBoxRef.Damage = Damage;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetType() != typeof(BoxCollider))
        {
            if (other.tag == ("Player") || other.tag == ("Pet"))
            {
                if (StartTimer == false)
                {
                    //canAttack = true;
                    //AttackBoxRef.HasSwung = true;
                }
            }
        }
        if(EnemyToLookAt != Player)
        {
            if(other.tag == "Enemy")
            {
                if (StartTimer == false)
                {
                    //AttackBoxRef.HasSwung = true;
                }
            }
        }
        if (GetComponent<Boss>() && other.tag == "StopPoint")
        {
            Level3Manager.Instance.IsRound2 = true;
            GetComponent<Boss>().EnemyToLookAt = null;
            GetComponent<Boss>().Round2 = true;
            Destroy(other.gameObject);
            gameObject.SetActive(false);
        }
        }

    protected void OnTriggerExit(Collider other)
    {
        if (other.tag == ("Player") || other.tag == ("Pet"))
        {
            anim.SetBool("Attack", false);
        }
    }

    public DamageEffects DamEffect(DamageEffects DamEff)
    {
        
        switch(DamEff)
        {
            case DamageEffects.FireEffect:
                FireEffect.SetActive(true);
                StartCoroutine(EffectTimer());
                break;
            case DamageEffects.ElectricEffect:
                ElectricEffect.SetActive(true);
                StartCoroutine(EffectTimer());
                break;
        }

        return DamEff;
    }
    protected IEnumerator EffectTimer()
    {
        yield return new WaitForSeconds(5);
        FireEffect.SetActive(false);
        ElectricEffect.SetActive(false);
    }
    /*
    private IEnumerator AttackTimer()
    {
        anim.SetBool("Attack", true);
        yield return new WaitForSeconds(1);
        //Attack();
        anim.SetBool("Attack", false);
        StartCoroutine(AttackTimer());
    }
    */

    public void IncreaseDifficulty()
    {
        Damage += 2;
        RangedDamage += 3;
        Speed += 0.3f;
        MaxHealth += 10;
        Health += 10;
        HealthText.text = Health + " / " + MaxHealth;
    }

    public void Rage()
    {
        hit = Physics.OverlapSphere(transform.position, 10);
        for(int i = 0; i < hit.Length; i++)
        {
            if(hit[i].tag == "Enemy")
            {
                if (hit[i].gameObject != this.gameObject)
                {
                    //RageTarget = hit[i].gameObject;
                  
                        EnemyToLookAt = hit[i].gameObject;
                    hit[i].GetComponent<BaseEnemy>().OnDeath += EnemyDead;
                    if(AttackBoxRef != null)
                    AttackBoxRef.IsEnemy = true;
                    break;
                } 
            } 
        }  
    }
    public void EnemyDead(BaseEnemy sender)
    {
        EnemyToLookAt = Player;
        sender.OnDeath -= EnemyDead;
    }
    
}


