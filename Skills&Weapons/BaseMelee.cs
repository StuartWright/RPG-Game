using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMelee : MonoBehaviour
{
    //public Player PlayerRef;
    public Player PlayerRef;
    private int RandomNum;
    //public float critDamage;
    public float Timer = 1;
    public Animator anim;
    private GameObject FirePoint;
    public GameObject AttackBoxGO;
    private AttackBox AttackBoxScript;
    public bool hasswung = false;
    private PlayerController PC;
    private PlayerRotate PR;
    public bool canSwing = true;
    public float CritDamage
    {
        get
        {
            return SkillManager.Instance.PhysicalDamage * 2;
        }
    }
    private void Start()
    {
        PlayerRef = GameObject.Find("Player").GetComponent<Player>();
        anim = PlayerRef.GetComponentInChildren<Animator>();
        FirePoint = GameObject.Find("FirePoint");
        Transform AttackBoxTrans = this.gameObject.transform.GetChild(0);
        AttackBoxGO = AttackBoxTrans.gameObject;
        AttackBoxScript = AttackBoxGO.gameObject.GetComponent<AttackBox>();
        PC = GameObject.Find("PlayerBase").GetComponent<PlayerController>();
        PR = GameObject.Find("PlayerBase").GetComponent<PlayerRotate>();
    }
    void Update()
    {  
        if (Input.GetButtonDown("Fire1") && PlayerRef.CanAttack == true)
        {
            Fire();
        }
    }
   public void CanSwing()
    {

    }
    void Fire()
    {

        if (canSwing)
        {
            anim.SetBool("condition", false);
            anim.SetBool("Swing", true);
            canSwing = false;
            PC.canMove = false;
            PR.CanTurn = false;
            RandomNum = Random.Range(0, 99);
            if (RandomNum <= SkillManager.Instance.CritChance)
            {
                AttackBoxScript.Damage = (int)SkillManager.Instance.PhysicalDamage * 2;
                AttackBoxScript.Crit = true;
            }
            else
            {
                AttackBoxScript.Damage = (int)SkillManager.Instance.PhysicalDamage;
                AttackBoxScript.Crit = false;
            }
                
        }



    }
}
