using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    [SerializeField]
    float sensitivity;

    Animations anim;
    Jump jump;

    public Vector2 wasd;
    public Vector2 mouse;

    public bool lockMovement;
    public bool lockRotation;

    public float afkCounter = 0.5f;

    public bool test = false;

    private void Start()
    {
        if (!anim)
        {
            anim = GetComponent<Animations>();
        }
        if(!jump)
        {
            jump = GetComponent<Jump>(); 
        }
    }

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

        if (wasd != Vector2.zero || Input.GetKeyDown(KeyCode.Space) == true)
        {
            anim.dance = false;
        }

        if (wasd == Vector2.zero && !anim.dance && !GetGrounded())
        {
            Debug.Log(afkCounter);
            Debug.Log(test);
            afkCounter += Time.deltaTime;
            if (afkCounter >= 3.708f && afkCounter < 5 && test)
            {
                anim.animController.SetTrigger("StopIdle");
                test = false;
                //anim.ResetTriggerIdle();
            }
            else if (afkCounter >= 10)
            {
                anim.animController.ResetTrigger("StopIdle");
                test = true;
                anim.animController.Play("IdleAnim");
                afkCounter = 0;

                //anim.TriggerIdle();
            }
        }
        else
        {
            afkCounter = 0;
            // anim.ResetTriggerIdle();
            anim.animController.SetTrigger("StopIdle");
        }

    }

    private bool GetGrounded()
    {
        return jump.grounded;
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
