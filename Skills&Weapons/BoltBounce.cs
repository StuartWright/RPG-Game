using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltBounce : MonoBehaviour
{
    public float Speed;
    public float radius;
    public GameObject Enemy = null;
    public int BoltLives = 4;
    public List<GameObject> EnemiesHit = new List<GameObject>();
    public Collider[] Hit;
    public bool HasHit;

    void Update()
    {
        if (Enemy == null)
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
            transform.LookAt(Enemy.transform.position + new Vector3(0, 2, 0));
        }

        
    }
     public void EnemyDead(BaseEnemy sender)
    {
        Destroy(this.gameObject);
    }
    public void OnTriggerEnter(Collider other)
    {   
            if (other.tag == "Enemy")
            {
            IDamage DamRef = other.GetComponent<IDamage>();
            if (DamRef != null)
            {
                if(!HasHit)
                {
                    DamRef.TakeDamage(SkillManager.Instance.MagicDamage);
                    DamRef.SetDamageText(SkillManager.Instance.MagicDamage, false);
                    BoltLives--;
                    HasHit = true;
                }  
            }
            Hit = Physics.OverlapSphere(transform.position, radius);

                if (BoltLives <= 0 || other.GetComponent<BaseEnemy>().IsDead)
                Destroy(gameObject);

                
            if(Enemy != null)
            {
                foreach (GameObject Target in EnemiesHit)
                {
                    if (Target.name == Enemy.name && Target != null)
                        Destroy(gameObject);
                }
            }
            
            if (!EnemiesHit.Contains(other.gameObject))
                    EnemiesHit.Add(other.gameObject);
            for (int i = 0; i < Hit.Length; i++)
            {
                if (Hit[i].tag == "Enemy" && !EnemiesHit.Contains(Hit[i].gameObject))
                {
                    Enemy = Hit[i].gameObject;
                    HasHit = false;
                    break;
                }
            }
        }
        if (other.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
