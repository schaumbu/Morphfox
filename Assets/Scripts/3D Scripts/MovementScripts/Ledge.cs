using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    Gravity gravity;
    Rigidbody rigidbody;
    Jump jump;
    LedgeColliding ledgeCol;
    GameInput gameInput;

    [SerializeField]
    float pullUpFromHoldingTime;
    [SerializeField]
    Transform test;

    public bool pullUpFromHolding;
    public bool pullUp;
    public bool stepUp;

    private Vector3 startPos;
    private Vector3 edgeClimbPoint;

    private float holdingTime_FristStep;
    private float holdingTime_SndStep;

    private float currentLerpTime_FirstStep = 0;
    private float currentLerpTime_SndStep = 0;

    #region Startfunction
    void Start()
    {
        if (!gameInput)
        {
            gameInput = GetComponent<GameInput>();
        }
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
            if(currentLerpTime_FirstStep < holdingTime_FristStep)
            {
                currentLerpTime_FirstStep += Time.deltaTime;
                float Percentage = currentLerpTime_FirstStep / holdingTime_FristStep;
                transform.position = Vector3.Lerp(startPos, new Vector3(startPos.x, startPos.y + 3.3f, startPos.z), Percentage);
            }
            else if(currentLerpTime_SndStep < holdingTime_SndStep)
            {
                currentLerpTime_SndStep += Time.deltaTime;
                float Percentage = currentLerpTime_SndStep / holdingTime_SndStep;
                transform.position = Vector3.Lerp(new Vector3(startPos.x, startPos.y + 3.3f, startPos.z), edgeClimbPoint, Percentage);
            }
            else
            {
               
                // Das hier machen wenn fertig
                currentLerpTime_FirstStep = 0;
                currentLerpTime_SndStep = 0;
                pullUpFromHolding = false;
                gravity.UnFreezeGravity();
                rigidbody.freezeRotation = false;
                rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                TurnOffHolding();
                gameInput.lockMovement = false;
            }



            /*currentLerpTime_FirstStep += Time.deltaTime;
            if(currentLerpTime_FirstStep >= pullUpFromHoldingTime)
            {
                
            }
            else
            {
                float totalDis = Vector3.Distance(startPos, new Vector3(startPos.x, startPos.y + 4.6f, startPos.z)) + Vector3.Distance(new Vector3(startPos.x, startPos.y + 4.6f, startPos.z), edgeClimbPoint);

                float Percentage = currentLerpTime /;

                if (Percentage < 0.5f)
                {
                    transform.position = Vector3.Lerp(startPos, new Vector3(startPos.x, startPos.y + 4.6f, startPos.z), 2*Percentage);
                }
                else
                {
                    transform.position = Vector3.Lerp(new Vector3(startPos.x, startPos.y + 4.6f, startPos.z), edgeClimbPoint, Percentage);
                }
                
            }*/
            /*
            // Das hier machen wenn fertig
            pullUpFromHolding = false;
            gravity.UnFreezeGravity();
            rigidbody.freezeRotation = false;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            TurnOffHolding();*/


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
        edgeClimbPoint.y += 4.5f;
        //Debug.Log(edgeClimbPoint.y);
        edgeClimbPoint += 2*rigidbody.transform.forward;
        startPos = transform.position;
        holdingTime_FristStep = pullUpFromHoldingTime * 0.622642f;
        holdingTime_SndStep = pullUpFromHoldingTime * (1 - 0.622642f);

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
        rigidbody.position = new Vector3(rigidbody.position.x, col.position.y - 3.3f, rigidbody.position.z);
        // freeze rotatbion around y axis
        rigidbody.freezeRotation = true;
    }

    public void PullUpFromHolding(Transform col)
    {
        pullUpFromHolding = true;
        CalculatePullPoint();
        
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
