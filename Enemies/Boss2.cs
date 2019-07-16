using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : Boss
{

    protected new void Start()
    {
        ShowHealthBar += EnemyC;
        base.Start();
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
            Speed = 5;
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

}
