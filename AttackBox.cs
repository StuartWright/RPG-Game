using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
    public float Damage;
    public bool Crit = false;
    public bool CanSwing = false;
    public bool CanDamage = true;
    private PlayerController PC;
    private PlayerRotate PR;
    private BaseMelee SwordRef;
    private Player playerref;
    private void Start()
    {
        PC = GameObject.Find("PlayerBase").GetComponent<PlayerController>();
        PR = GetComponentInParent<PlayerRotate>();
        SwordRef = GetComponentInParent<BaseMelee>();
        playerref = GameObject.Find("Player").GetComponent<Player>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Pet(Clone)")
            CanDamage = false;
        if (CanDamage)
        {
            if (CanSwing)
            {

                IDamage damageInterface = other.GetComponent<IDamage>();
                if (damageInterface != null)
                {
                    damageInterface.TakeDamage(Damage);
                    damageInterface.SetDamageText(Damage, Crit);
                    //playerref.StopAnim();
                    //playerref.CanMove();
                    //playerref.AnimationEvent();
                    //PC.canMove = true;
                    //SwordRef.anim.SetBool("Swing", false);
                    //PR.CanTurn = true;
                   // SwordRef.canSwing = true;
                    CanSwing = false;
                }

            }
        }
        else
            CanDamage = true; // INCASE THE PLAYER HITS THE PET
        
    }
    
}


