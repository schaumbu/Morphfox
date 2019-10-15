using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    public Vector2 wasd;
    public Vector2 mouse;

    public bool lockMovement;
    public bool lockRotation;

    void Update()
    {
        if (!lockMovement)
        {
            wasd.x = Input.GetAxis("Horizontal");
            wasd.y = Input.GetAxis("Vertical");
            wasd = Vector2.ClampMagnitude(wasd, 1);
        }
        else
        {
            wasd = Vector2.zero;
        }

        if (!lockRotation)
        {
            mouse.x = Input.GetAxis("Mouse X");
            mouse.y = -Input.GetAxis("Mouse Y");
            mouse = Vector2.ClampMagnitude(mouse, 1);
        }
        else
        {
            mouse = Vector2.zero;
        }

    }

    // Setter

    void setlockRotation(bool i)
    {
        lockRotation = i;
    }

    void setlockMovement(bool i)
    {
        lockMovement = i;
    }
}
