using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : BaseEnemy
{
    public GameObject EnemyBolt;
    public GameObject FirePoint;
    private bool HasShot;
    private float StopMovingTimer = 3;
    private float WaitingToStopTimer = 4;
    private bool HasStopped = false;
    private bool CanMove = true;
    private bool WalkTimer = false;
    public Vector3 Direction;
    private void Awake()
    {
        Direction = Vector3.back;
    }
    void Update ()
    {
        if(Player != null)
        {
            float PlayerDistance = Vector3.Distance(Player.transform.position, transform.position);
            if (PlayerDistance <= AttackRadius || UnderAttack)
            {
                if (UnderAttack && PlayerDistance >= AttackRadius)
                    Direction = Vector3.forward;
                if (PlayerDistance <= AttackRadius)
                    UnderAttack = false;//UNDER ATTACK ALLOWS THE ENEMY TO TARGET THE PLAYER WHEN THEYVE BEEN ATTACKED. 
                if (CanMove)
                {
                    anim.SetBool("Running", true);
                    Speed = 1.5f;
                    transform.Translate(Direction * Speed * Time.deltaTime);
                    WalkTimer = true;
                }
                transform.LookAt(Player.transform);

                if (!HasShot)
                    Attack();
            }
            else
            {
                anim.SetBool("Running", false);
                Speed = 0;
            }

            if (WalkTimer)
            {
                WaitingToStopTimer -= Time.deltaTime;
                if (WaitingToStopTimer <= 0)
                {
                    int RanNum = Random.Range(0, 3);
                    switch (RanNum)
                    {
                        case 0:
                            Direction = Vector3.back;
                            break;
                        case 1:
                            Direction = Vector3.right;
                            break;
                        case 2:
                            Direction = Vector3.left;
                            break;
                            // case 3:
                            //     Direction = Vector3.forward;
                            //     break;
                    }
                    //CanMove = false;
                    //WalkTimer = false;
                    WaitingToStopTimer = 4;
                    //HasStopped = true;
                }

            }
            if (HasStopped)
            {
                StopMovingTimer -= Time.deltaTime;
                if (StopMovingTimer <= 0)
                {
                    CanMove = true;
                    StopMovingTimer = 3;
                    HasStopped = false;
                }
            }
            if (StartTimer)
            {
                AttackTimer -= Time.deltaTime;
                if (AttackTimer <= 2f)
                {
                    anim.SetBool("Attack", false);
                }
            }

            if (AttackTimer <= 0)
            {
                HasShot = false;
            }
        }
        
    }
    private void Attack()
    {
        HasShot = true;
        anim.SetBool("Attack", true);
        StartTimer = true;
        StartCoroutine(Fire());
        AttackTimer = 2.5f;
    }

    private IEnumerator Fire()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject bolt = Instantiate(EnemyBolt, FirePoint.transform.position, transform.rotation);
        bolt.GetComponent<EnemyBolt>().EnemyRef = this;
    }
}
