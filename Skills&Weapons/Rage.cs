using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rage : MonoBehaviour
{
    private float Timer = 3;
    public bool HasTarger = false;
    public float Size = 2;
    public int StaminaCost = 60;
    void Start ()
    {
        transform.localScale = new Vector3(Size, 1, Size);
    }
	
	
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
        if(other.tag == "Enemy")
        {
            other.GetComponent<BaseEnemy>().Rage();
            //other.GetComponent<Renderer>().material.color = Color.blue;
        }
    }
}
