using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour {

    private float Timer = 1.5f;
    public float Size = 1;
    public int StaminaCost = 30;
    private Player PlayerRef;
    private GameObject PlayerBase;
    private BaseEnemy EnemyRef;
    public List<BaseEnemy> EnemiesHit = new List<BaseEnemy>();
	void Start ()
    {
        transform.localScale = new Vector3(1, 1, Size);
        PlayerRef = GameObject.Find("Player").GetComponent<Player>();
        PlayerBase = GameObject.Find("PlayerBase");
    }
	
	// Update is called once per frame
	void Update ()
    {
        Timer -= Time.deltaTime;
        if(Timer <= 0)
        {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Wall")
        {
            PlayerBase.transform.position = new Vector3(PlayerRef.StartPos.x, PlayerRef.StartPos.y, PlayerRef.StartPos.z);
        }

        if (other.tag == "Enemy")
        {
            EnemyRef = other.GetComponent<BaseEnemy>();
            if(!EnemyRef.Attacked)
            {
                EnemiesHit.Add(other.GetComponent<BaseEnemy>());
                EnemyRef.Attacked = true;
                IDamage DamRef = other.GetComponent<IDamage>();
                if (DamRef != null)
                {
                    DamRef.TakeDamage(SkillManager.Instance.DashDamage);
                    DamRef.SetDamageText(SkillManager.Instance.DashDamage, false);
                }
                EnemyRef.DamEffect(DamageEffects.ElectricEffect);
                StartCoroutine(ResetAttacked());
            }
        }
    }

    IEnumerator ResetAttacked()
    {
        yield return new WaitForSeconds(0.1f);
        foreach(BaseEnemy enemies in EnemiesHit)
        {
            enemies.Attacked = false;
        }
    }
}
