using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour {

    // GameObject Player;


    public float LookSensivitity;
    public float LookSmoothing;

    public GameObject Player;
    Vector2 MouseLook;
    void Start()
    {
        //  Player = GameObject.FindGameObjectWithTag("Player");
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        // transform.position = Player.transform.position;
        Vector2 MousePosition = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        MousePosition = Vector2.Scale(MousePosition, new Vector2(LookSensivitity * LookSmoothing, LookSmoothing * LookSensivitity));

        MouseLook += MousePosition;

        transform.localRotation = Quaternion.AngleAxis(-MouseLook.y, Vector3.right);
        Player.transform.localRotation = Quaternion.AngleAxis(MouseLook.x, Player.transform.up);
       // Player.transform.localRotation = Quaternion.AngleAxis(MouseLook.x, transform.position);
    }
}
