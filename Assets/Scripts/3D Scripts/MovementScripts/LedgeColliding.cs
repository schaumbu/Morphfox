﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeColliding : MonoBehaviour
{
    Ledge ledge;

    public bool hanging;


    void Start()
    {
        if (!ledge)
        {
            ledge = GetComponent<Ledge>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Ledge"))
        {
            if (!hanging)
            {
                hanging = true;
            }
            ledge.HoldLedge(collision.transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (hanging)
        {
            hanging = false;
        }
    }
}