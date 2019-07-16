using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCursor : MonoBehaviour
{
    public float LookHeight;


    void LateUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        Vector3 LookPoint = hit.point;
        if (hit.collider.gameObject.tag == "Ground")
        {
            
            LookHeight = 0;
            LookPoint.y += LookHeight;
            transform.LookAt(LookPoint);
        }
        if (hit.collider.gameObject.tag == "Enemy")
        {
            LookHeight = -2;
            LookPoint.y += LookHeight;
            transform.LookAt(LookPoint);
        }
    }
}