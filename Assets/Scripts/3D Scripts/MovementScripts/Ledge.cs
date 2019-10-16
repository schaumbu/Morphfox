using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    Gravity gravity;
    Rigidbody rigidbody;
    Jump jump;

    private bool pullUpFromHolding;
    private bool pullUp;
    private bool stepUp;

    private Vector3 edgeClimbPoint;

    #region Startfunction
    void Start()
    {
        if (!jump)
        {
            jump = GetComponent<Jump>();
        }
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
            // abfragen ob pullpoint schon berechnet wurde
            if(transform.position.y >= edgeClimbPoint.y)
            {
                if(transform.position != edgeClimbPoint)
                {
                    rigidbody.AddForce(edgeClimbPoint - transform.position);
                }
                else
                {
                    pullUpFromHolding = false;
                    gravity.FreezeGravity();
                    rigidbody.freezeRotation = false;
                }
            }
            else
            {
                rigidbody.AddForce(transform.up);
            }
        }

        if (pullUp)
        {

        }

        if (stepUp)
        {

        }
    }

    public void CalculatePullPoint()
    {
        edgeClimbPoint = rigidbody.transform.position;
        edgeClimbPoint.y += jump.jumpHeight;
        edgeClimbPoint += rigidbody.transform.forward;
    }

    public void HoldLedge(Transform col)
    {
        // freeze gravity while hanging
        gravity.FreezeGravity();
        // reset velocity
        rigidbody.velocity = Vector3.zero;
        // Ledge has same vertical rotation as player
        transform.eulerAngles = new Vector3(rigidbody.rotation.x, col.transform.rotation.y, rigidbody.rotation.z);
        // freeze rotatbion around y axis
        rigidbody.freezeRotation = true;
    }

    public void PullUpFromHolding(Transform col)
    {
        CalculatePullPoint();
        pullUpFromHolding = true;
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
