using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour {
    public Camera CameraRef;

    private void Start()
    {
        CameraRef = FindObjectOfType<Camera>();
    }

    void Update ()
    {
        transform.LookAt(transform.position + CameraRef.transform.rotation * Vector3.forward,
           CameraRef.transform.rotation * Vector3.up);
    }
}
