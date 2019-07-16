using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum AttackState
{
    Attack,
    Calm
}

public class Pet : MonoBehaviour, IDamage
{
    public delegate void DeathEvent();
    public event DeathEvent OnDeath;

    public int Speed;
    public int Damage = 20;
    public int NoDamage = 0;
    public int ManaCost = 70;
    int i = 0;

    public float AttackRadius;
    public float LifeTimer = 60;
    private float Range = 3f;
    private float PushBackForce = 100;
    private float ThingDistance;
    public float Health = 100;
    private float EnemyDistance;
    private float MaxHealth = 100;
    public float AttackTimer = 2;

    public GameObject Player;
    public GameObject ThingToFollow;
    public Collider[] hit;
    public Text HealthText;
    public Image HPBar;
    private Animator anim;
    public AttackBox AttackBoxRef;
    private Rigidbody rb;
    private BaseEnemy currentTarget;

    public bool IsEnemy = false;
    public bool StartTimer = false;
    public bool StillTouching = false;
    private bool canAttack = false;
    public bool HasSwung = false;
    void Start ()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        LifeTimer -= Time.deltaTime;

        if (LifeTimer <= 0)
        {
            Death();
        }

        if (!IsEnemy)
        {
            ThingToFollow = Player;
            //AttackState = AttackState.Calm;
            if (ThingToFollow == Player)
            {
                DrawSphere();
            }
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].tag == "Enemy")
                {
                    ThingToFollow = hit[i].gameObject;

                    currentTarget = ThingToFollow.GetComponent<BaseEnemy>();

                    currentTarget.OnDeath += OnTargetDied;

                    // AttackState = AttackState.Attack;
                    IsEnemy = true;
                    break;
                }
            }
                
        }

        if (ThingToFollow == null)
        {
            IsEnemy = false;
            ThingToFollow = Player;
        }

        float ThingDistance = Vector3.Distance(ThingToFollow.transform.position, transform.position);
        if (ThingDistance >= 1.6f)
        {
            anim.SetBool("Running", true);
            Speed = 4;
            //transform.Translate(Vector3.forward * Speed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, ThingToFollow.transform.position, Speed * Time.deltaTime);
            transform.LookAt(ThingToFollow.transform);
            anim.SetBool("Attack", false);
            canAttack = true;
        }
        else if (ThingDistance <= 1.6f)
        {
            anim.SetBool("Running", false);
            Speed = 0;
            if(ThingToFollow != Player)
            {
                if (canAttack)
                {
                    Swing();
                    canAttack = false;
                }
            }
            
        }
        
    }
    private void OnTargetDied(BaseEnemy sender)
    {
        IsEnemy = false;
        currentTarget.OnDeath -= OnTargetDied;
    }
    public void Swing()
    {
        GetDamageAmount();
        anim.SetBool("Attack", true);
    }
    public void EnableCanAttack()
    {
        AttackBoxRef.CanSwing = true;
    }
    public void ResetCanAttack()
    {
        canAttack = true;
    }
    protected void GetDamageAmount()
    {
        AttackBoxRef.Damage = Damage;
    }

    public void DrawSphere()
    {
        hit = Physics.OverlapSphere(transform.position, 10);
    }

    public void TakeDamage(float DamAmount)
    {
        
        Health -= DamAmount;
        HPBar.fillAmount = Health / MaxHealth;
        HealthText.text = Health + " / " + MaxHealth;
        if (Health <= 0)
        {
            Death();
        }
    }
    private void Death()
    {
        anim.SetBool("Death", true);
        rb.useGravity = false;
        HPBar.gameObject.SetActive(false);
        this.GetComponent<Pet>().enabled = false;
        this.GetComponent<CapsuleCollider>().enabled = false;
        this.GetComponent<BoxCollider>().enabled = false;
        Destroy(this.GetComponent<Rigidbody>());
        AttackBoxRef.gameObject.SetActive(false);
        if (OnDeath != null)
            OnDeath();
        //Destroy(gameObject);
    }
    public void SetDamageText(float DamAmount, bool Crit) { }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (StartTimer == false)
            {
                other.GetComponent<BaseEnemy>().EnemyToLookAt = this.gameObject;
                other.GetComponent<BaseEnemy>().Pet = this.gameObject;
                OnDeath += other.GetComponent<BaseEnemy>().OnPetDied;
            }
        }
           
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Enemy")
        {
            OnDeath -= other.GetComponent<BaseEnemy>().OnPetDied;
        }
    }
    /*
    private IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(1.5f);
        //Attack();
        StartCoroutine(AttackTimer());
    }
    */
}
