using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBolt : MonoBehaviour
{
    public BaseEnemy EnemyRef;
	void Update ()
    {
        transform.Translate(Vector3.forward * 10 * Time.deltaTime);
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" || other.tag == "Enemy")
        {
            IDamage damage = other.GetComponent<IDamage>();
            if (damage != null)
            {
                damage.TakeDamage(EnemyRef.RangedDamage);
                Destroy(gameObject);
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
