using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Boss : BaseEnemy
{
    public delegate void ShowHealth();
    public event ShowHealth ShowHealthBar;
    
    protected bool HasShot;
    protected bool DoOnce = false;
    public bool Round2 = false;

    public GameObject EnemyBolt;
    public GameObject FirePoint;
    public GameObject HealthCanvas;
    public GameObject RunToPoint;

    protected float MagicTimer;
    protected float HalfHealth;
    protected new void Start()
    { 
        ShowHealthBar += EnemyC;
        
        HPBar = transform.Find("EnemyC").Find("HealthBarBackground").Find("HealthBar").GetComponent<Image>();
        HalfHealth = Health / 2;
        base.Start();
    }

    protected void ShowBossHealthBar()
    {
        if (ShowHealthBar != null)
            ShowHealthBar();
    }
    void Update()
    {
        
        if (EnemyToLookAt == null || EnemyToLookAt == this.gameObject)
        {
            EnemyToLookAt = Player;
        }


        float PlayerDistance = Vector3.Distance(EnemyToLookAt.transform.position, transform.position);
        if (AtHalfHealth())
        {
            EnemyToLookAt = RunToPoint;
            Speed = 8;
            transform.position = Vector3.MoveTowards(transform.position, RunToPoint.transform.position, Speed * Time.deltaTime);
            anim.SetBool("Moving", true);
        }
        else if (PlayerDistance <= AttackRadius && PlayerDistance >= 1.8)
        {
            ShowBossHealthBar();

            anim.SetBool("Moving", true);
            Speed = 3;
            transform.position = Vector3.MoveTowards(transform.position, EnemyToLookAt.transform.position, Speed * Time.deltaTime);
            transform.LookAt(EnemyToLookAt.transform);

            if (!HasShot)
                MagicAttack();
            canAttack = true;
        }
        else if (PlayerDistance <= AttackRadius && PlayerDistance <= 1.8)
        {
            anim.SetBool("Moving", false);
            transform.LookAt(EnemyToLookAt.transform);
            Speed = 0;
            if (canAttack)
            {
                Swing();
                canAttack = false;
            }

        }
        else
            anim.SetBool("Moving", false);

        if (StartTimer)
        {
            MagicTimer -= Time.deltaTime;
            if (MagicTimer <= 2f)
            {
                anim.SetBool("MagicAttack", false);
                
            }
        }

        if (MagicTimer <= 0)
        {
            HasShot = false;
            StartTimer = false;
        }
    }
    protected void EnemyC() { HealthCanvas.SetActive(true); ShowHealthBar -= EnemyC; }

    protected void MagicAttack()
    {
        HasShot = true;
        anim.SetBool("MagicAttack", true);
        StartTimer = true;
        StartCoroutine(Fire());
        MagicTimer = 2.5f;
    }

    private IEnumerator Fire()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject bolt = Instantiate(EnemyBolt, FirePoint.transform.position, transform.rotation);
        bolt.GetComponent<EnemyBolt>().EnemyRef = this;
    }
    
    public bool AtHalfHealth()
    {
        if (!Round2)
        {
            if (Health <= HalfHealth)
            {
                if (!DoOnce)
                {
                    StartCoroutine(Level3Manager.Instance.SpawnEnemies());
                    Level3Manager.Instance.BossRunAway();
                    DoOnce = true;
                }

                return true;
            }
        }
        return false;
    }
    
}
