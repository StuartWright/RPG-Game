using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Manager : MonoBehaviour
{
    private static Level3Manager instance;
    public static Level3Manager Instance
    {
        get { return instance; }
    }
    public GameObject PlayerBase;
    public GameObject[] Enemies = new GameObject[5];
    public GameObject GateToClose, FencesToRemove;
    public Boss BossRef;
    public OpenCloseGate InnerGateRef, OuterGateRef;
    public int EnemyCounter = 0;

    public bool IsRound2;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayerBase = GameObject.Find("PlayerBase");
        PlayerBase.transform.position = new Vector3(110, 0, 45);
        GameObject.Find("Player").transform.position = new Vector3(PlayerBase.transform.position.x, PlayerBase.transform.position.y, PlayerBase.transform.position.z);
    }
   
    public void BossRunAway()
    {
        GateToClose.SetActive(false);
        FencesToRemove.SetActive(true);
    }
    public IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < Enemies.Length; i++)
        {
            yield return new WaitForSeconds(4);
            Enemies[i].SetActive(true);
            if(i <= Enemies.Length)
            StartCoroutine(SpawnEnemies());
        }
        
    }
    public void Round2()
    {
        BossRef.gameObject.SetActive(true);
        OuterGateRef.NeedToOpenGate = true;
        OuterGateRef.NeedToCloseGate = false;
        InnerGateRef.NeedToOpenGate = false;
        InnerGateRef.NeedToCloseGate = true;
        BossRef.EnemyToLookAt = null;
    }
}
