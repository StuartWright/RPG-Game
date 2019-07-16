using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsManager : MonoBehaviour, IDifficulty
{
    float Damage = 5;
    float RangedDamage = 10;
    float MaxHealth = 100;
    float Health = 100;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ApplyEnemyStats()
    {
        MonoBehaviour[] list = GameObject.FindObjectsOfType<MonoBehaviour>();
        foreach (MonoBehaviour mb in list)
        {
            if (mb is IDifficulty && mb != this) 
            {
                mb.GetComponent<BaseEnemy>().Health = Health;
                mb.GetComponent<BaseEnemy>().Damage = Damage;
                mb.GetComponent<BaseEnemy>().MaxHealth = MaxHealth;
                mb.GetComponent<BaseEnemy>().RangedDamage = RangedDamage;
                mb.GetComponent<BaseEnemy>().HealthText.text = Health + " / " + MaxHealth;
                // IDifficulty Difficulty = (IDifficulty)mb;
                //Difficulty.IncreaseDifficulty();
            }
        }
    }

    public void IncreaseDifficulty()
    {
        Damage += 5;
        RangedDamage += 5;
        MaxHealth += 10;
        Health += 10;
    }

}
	
