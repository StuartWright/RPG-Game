using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMagicWeap : MonoBehaviour
{
    public GameObject Bolt;
    public GameObject BoltBounce;
    public Player PlayerRef;
    public int ManaCost = 10;
    private GameObject Player;
    public GameObject FirePoint;
    public Animator anim;
    private PlayerController PC;
    private PlayerRotate PR;
    public bool canSwing = true;
    private void Start()
    {
        Player = GameObject.Find("Player"); 
        PlayerRef = GameObject.Find("Player").GetComponent<Player>();
        anim = PlayerRef.GetComponentInChildren<Animator>();
        PC = GameObject.Find("PlayerBase").GetComponent<PlayerController>();
        PR = GameObject.Find("PlayerBase").GetComponent<PlayerRotate>();
    }

    void Update ()
    {
        if (Input.GetButtonDown("Fire1") && PlayerRef.Mana >= ManaCost && PlayerRef.CanAttack == true)
        {
            Fire();
            
        }
        
    }

    private void Fire()
    {
        if (canSwing)
        {
            canSwing = false;
            PC.canMove = false;
            PR.CanTurn = false;
            anim.SetBool("Swing", true);
            PlayerRef.Mana -= ManaCost;
            if (SkillManager.Instance.MagicBoltLvl >= 2)
            {
                Instantiate(BoltBounce, FirePoint.transform.position, FirePoint.transform.rotation);
            }
            else
            {
                Instantiate(Bolt, FirePoint.transform.position, FirePoint.transform.rotation);
            }
        }
    }
}
