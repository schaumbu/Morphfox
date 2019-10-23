using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeColliding : MonoBehaviour
{
    Ledge ledge;
    Jump jump;

    [SerializeField]
    Collider def;

    public bool hanging;

    Collider colStorage;

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

    private void Update()
    {
        if (hanging)
        {
            if (Input.GetKey("s"))
            {
                ledge.LetFall();
            } else if (Input.GetKey("w"))
            {
                hanging = false;
                ledge.PullUpFromHolding(colStorage.transform);
            }
        }
    }

    /*if(hanging && Input.GetKey("s"))
    {
        gravity.UnFreezeGravity();
        hanging = false;
        rigidbody.freezeRotation = false;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }*/

    private void OnTriggerStay(Collider other)
    {
        if(!hanging && jump.grounded && Input.GetKey("e"))
        {
            hanging = true;
            ledge.HoldLedge(other.transform);
            colStorage = other;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.layer == LayerMask.NameToLayer("Ledge") && !jump.grounded)
        {
            hanging = true;
            ledge.HoldLedge(collider.transform);
            colStorage = collider;
            /*if (!jump.grounded)
            {
                if (!hanging)
                {
                    hanging = true;
                }
                ledge.HoldLedge(collider.transform);
            } else if (Input.GetKey("e"))
            {
                if (!hanging)
                {
                    hanging = true;
                }
                ledge.HoldLedge(collider.transform);
            }*/

        }
    }
}
