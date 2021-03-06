﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    LedgeColliding ledgeCol;
    Rigidbody rigidbody;


    public bool grounded;
    public int jumpsLeft;

    [SerializeField]
    private float raylength;
    [SerializeField]
    public float jumpHeight;
    [SerializeField]
    private Transform footpoint;

    void Start()
    {
        if (!ledgeCol)
        {
            ledgeCol = GetComponent<LedgeColliding>();
        }
        if (!rigidbody)
        {
            rigidbody = GetComponent<Rigidbody>();
        }
    }


    void Update()
    {
        // Check if grounded
        if (Physics.Raycast(new Ray(footpoint.position, Vector3.down), raylength, LayerMask.GetMask("Ground")))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        // Reset counter of jumps left
        if (grounded)
        {
            jumpsLeft = 2;
        }

        // Trigger a jump
        if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0 && !ledgeCol.hanging)
        {
            if (jumpsLeft == 2)
            {
                // Teleports the first jump
                transform.position = new Vector3(transform.position.x, transform.position.y + raylength * 2, transform.position.z);
            }
            // Reset vertical velocity before adding new force
            rigidbody.velocity.Set(rigidbody.velocity.x, 0, rigidbody.velocity.z);
            // Addforce
            rigidbody.AddForce(transform.up * jumpHeight);
            // Minus one jump
            jumpsLeft--;
        }
    }
}
