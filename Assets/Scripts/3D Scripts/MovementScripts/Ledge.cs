using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    Gravity gravity;
    Rigidbody rigidbody;

    private bool pullUpFromHolding;
    private bool pullUp;
    private bool stepUp;

    private Vector3 edgeClimbPoint;

    #region Startfunction
    void Start()
    {
        if (!gravity)
        {
            gravity = GetComponent<Gravity>();
        }
        if (!rigidbody)
        {
            rigidbody = GetComponent<Rigidbody>();
        }
    }
    #endregion

    void Update()
    {
        if (pullUpFromHolding)
        {

        }

        if (pullUp)
        {

        }

        if (stepUp)
        {

        }
    }

    public void HoldLedge(Transform col)
    {
        // freeze gravity while hanging
        gravity.FreezeGravity();
        // reset velocity
        rigidbody.velocity = Vector3.zero;
        // Ledge has same vertical rotation as player
        transform.eulerAngles = new Vector3(rigidbody.rotation.x, col.transform.rotation.y, rigidbody.rotation.z);
    }

    public void PullUpFromHolding(Transform col)
    {
        // Calculate point
        // put shit into Update function
        // get Animation right
    }

    public void PullUp(Transform col)
    {

    }

    public void StepUp(Transform col)
    {

    }

    public void LetFall(Transform col)
    {
        gravity.UnFreezeGravity();
    }
}
