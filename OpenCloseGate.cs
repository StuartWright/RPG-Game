using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseGate : MonoBehaviour
{
    public GameObject GateToClose, FencesToRemove;
    public Boss BossRef;

    public bool NeedToOpenGate;
    public bool NeedToCloseGate;
    private void Start()
    {
        BossRef.ShowHealthBar += CloseGate;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            if(NeedToCloseGate)
            CloseGate();
            if (NeedToOpenGate)
                OpenGate();
        }
    }
    public void CloseGate()
    {
        GateToClose.SetActive(true);
        FencesToRemove.SetActive(false);
        BossRef.ShowHealthBar -= CloseGate;
    }

    public void OpenGate()
    {
        GateToClose.SetActive(false);
        FencesToRemove.SetActive(true);
    }
}
