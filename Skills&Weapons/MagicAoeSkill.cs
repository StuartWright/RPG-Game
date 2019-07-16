using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAoeSkill : MonoBehaviour
{
    public float Size = 2;
    public GameObject FireEffect;
    public int ManaCost = 30;
    private BaseEnemy EnemyRef;
    public List<BaseEnemy> EnemiesHit = new List<BaseEnemy>();
    void Start ()
    {
        transform.localScale = new Vector3(Size, 1, Size);
        StartCoroutine(Timer());
	}


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            EnemyRef = other.GetComponent<BaseEnemy>();

            EnemyRef = other.GetComponent<BaseEnemy>();
            if (!EnemyRef.Attacked)
            {
                EnemiesHit.Add(other.GetComponent<BaseEnemy>());
                EnemyRef.Attacked = true;
                IDamage DamRef = other.GetComponent<IDamage>();
                if (DamRef != null)
                {
                    DamRef.TakeDamage(SkillManager.Instance.AOEDamage);
                    DamRef.SetDamageText(SkillManager.Instance.AOEDamage, false);
                }
                EnemyRef.DamEffect(DamageEffects.FireEffect);
                StartCoroutine(ResetAttacked());
            }

        }
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(7);
         Destroy(gameObject);
    }

    IEnumerator ResetAttacked()
    {
        yield return new WaitForSeconds(0.1f);
        foreach (BaseEnemy enemies in EnemiesHit)
        {
            enemies.Attacked = false;
        }
    }
}
