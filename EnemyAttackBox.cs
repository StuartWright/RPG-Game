using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBox : MonoBehaviour
{
    public float Damage;
    public bool Crit = false;
    public bool CanSwing = false;
    public bool CanDamage = true;
    public bool IsEnemy = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Enemy" || IsEnemy)
        {
            if (CanDamage)
            {
                if (CanSwing)
                {

                    IDamage damageInterface = other.GetComponent<IDamage>();
                    if (damageInterface != null)
                    {
                        damageInterface.TakeDamage(Damage);
                        damageInterface.SetDamageText(Damage, Crit);
                        CanSwing = false;
                    }

                }
            }
            else
                CanDamage = true; // INCASE THE PLAYER HITS THE PET
        }
        

    }

}
