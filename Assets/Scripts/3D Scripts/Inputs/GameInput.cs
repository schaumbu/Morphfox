using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    [SerializeField]
    float sensitivity;

    public Vector2 wasd;
    public Vector2 mouse;

    public bool lockMovement;
    public bool lockRotation;
    public bool afk;

    public float afkCounter;

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
            mouse = Vector2.ClampMagnitude(mouse, sensitivity);
        }
        else
        {
            mouse = Vector2.zero;
        }

        if(wasd == Vector2.zero)
        {
            afkCounter += Time.deltaTime;
            if(afkCounter >= 10)
            {
                afk = true;
            }
        }
        else
        {
            afkCounter = 0;
            afk = false;
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
