using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBolt : MonoBehaviour
{
    public float Speed;

    
    void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            IDamage DamRef = other.GetComponent<IDamage>();
            if(DamRef != null)
            {
                DamRef.TakeDamage(SkillManager.Instance.MagicDamage);
                DamRef.SetDamageText(SkillManager.Instance.MagicDamage, false);
            }
            
            Destroy(gameObject);
            
        }
        if(other.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

    protected void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

