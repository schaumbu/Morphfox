using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    Gravity gravity;
    Rigidbody rigidbody;
    Jump jump;
    LedgeColliding ledgeCol;

    [SerializeField]
    Transform test;

    private bool pullUpFromHolding;
    private bool pullUp;
    private bool stepUp;

    private Vector3 edgeClimbPoint;

    #region Startfunction
    void Start()
    {
        if (!ledgeCol)
        {
            ledgeCol = GetComponent<LedgeColliding>();
        }
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
                //Debug.Log("OK");
                if(rigidbody.velocity.y > 0)
                {
                    rigidbody.velocity = Vector3.zero;
                }
                if (transform.position != edgeClimbPoint)
                {
                    rigidbody.AddForce(edgeClimbPoint + transform.position);
                }
                else
                {
                    pullUpFromHolding = false;
                    gravity.UnFreezeGravity();
                    rigidbody.freezeRotation = false;
                    rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                    TurnOffHolding();
                }
            }
            else
            {
                test.position = edgeClimbPoint;
               // Debug.Log(edgeClimbPoint.x + " " + edgeClimbPoint.y + " " + edgeClimbPoint.z);
                //Debug.Log("idk");
                rigidbody.AddForce(transform.up * 50);
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
        edgeClimbPoint.y += 4.6f;
        Debug.Log(edgeClimbPoint.y);
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
        // Get on same height as Ledge
        rigidbody.position = new Vector3(rigidbody.position.x, col.position.y - 4.5f, rigidbody.position.z);
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

    public void LetFall()
    {
        gravity.UnFreezeGravity();
        rigidbody.freezeRotation = false;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        TurnOffHolding();
    }

    public void TurnOffHolding()
    {
        ledgeCol.hanging = false;
    }
}
