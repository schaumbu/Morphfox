using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeColliding : MonoBehaviour
{
    Ledge ledge;
    Jump jump;

    public bool hanging;

    #region Startfunction
    void Start()
    {
        if (!ledge)
        {
            ledge = GetComponent<Ledge>();
        }
        if (!jump)
        {
            jump = GetComponent<Jump>();
        }
    }
    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Ledge"))
        {
            if (!jump.grounded)
            {
                if (!hanging)
                {
                    hanging = true;
                }
                ledge.HoldLedge(collision.transform);
            } else if (Input.GetKey("E"))
            {
                if (!hanging)
                {
                    hanging = true;
                }
                ledge.HoldLedge(collision.transform);
            }
            
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
