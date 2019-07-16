using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Manager : MonoBehaviour
{

    GameObject PlayerBase;
	void Start ()
    {
        PlayerBase = GameObject.Find("PlayerBase");
        PlayerBase.transform.position = new Vector3(10,0,-12);
        GameObject.Find("Player").transform.position = new Vector3(PlayerBase.transform.position.x, PlayerBase.transform.position.y, PlayerBase.transform.position.z);
        GameObject.Find("EnemyStatManager").GetComponent<EnemyStatsManager>().ApplyEnemyStats();
    }
	
	
}
