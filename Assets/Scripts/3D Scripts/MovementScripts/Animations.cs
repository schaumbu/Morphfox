using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{

    public Animator animController;
    LedgeColliding ledgeCol;
    GameInput input;
    Ledge ledge;
    Movement movement;
    Jump jump;
    Rigidbody rig;

    public Animation idle;

    private float showVel;
    private bool dance;

    // TODO MAXIMO.COM
    // Bessere Jumpanimation
    // Animation beim Aufkommen mit hoher Geschwindigkeit
    // Step up animation
    // Silly dancing animation

    void Start()
    {
        if (!input)
        {
            input = GetComponent<GameInput>();
        }
        if (!ledge)
        {
            ledge = GetComponent<Ledge>();
        }
        if (!ledgeCol)
        {
            ledgeCol = GetComponent<LedgeColliding>();
        }
        if (!rig)
        {
            rig = GetComponent<Rigidbody>();
        }
        if (!jump)
        {
            jump = GetComponent<Jump>();
        }
        if (!movement)
        {
            movement = GetComponent<Movement>();
        }
        if (!animController)
        {
            animController = GetComponent<Animator>();
        }
    }

    void Update()
    {
        float lerpfactor = (Time.time - Time.fixedTime) / Time.fixedDeltaTime;
        Vector3 lerpvector = Vector3.Lerp(movement.lastFixedVel, rig.velocity, lerpfactor);


        if(Input.GetKey("h"))
        {
            if (!dance)
            {
                dance = true;
            }
            else
            {
                dance = false;
            }
        }

        if (input.afk && !dance)
        {
            animController.SetBool("Idle", true);
        }
        else
        {
                animController.SetBool("Idle", false);
        }

        animController.SetBool("Dance", dance);
        animController.SetFloat("HangingState", Mathf.MoveTowards(animController.GetFloat("HangingState"),input.wasd.x, Time.deltaTime * movement.speed));
        animController.SetBool("PullUpState", ledge.pullUpFromHolding);
        animController.SetFloat("WalkingState", Mathf.SmoothDamp(animController.GetFloat("WalkingState"), lerpvector.magnitude / movement.boostspeed, ref showVel, 0.1f));
        animController.SetFloat("JumpingState", lerpvector.y);
        animController.SetBool("OnGround", jump.grounded);
        animController.SetBool("Hanging", ledgeCol.hanging);
    }
}
