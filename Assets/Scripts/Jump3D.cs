using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump3D : MonoBehaviour
{
    private Rigidbody rig;
    [SerializeField]
    private Transform RayPoint;
    [SerializeField]
    private Transform MidPoint;
    [SerializeField]
    private bool coll;

    [SerializeField]
    private bool Mid;
    [SerializeField]
    private bool Ray;

    [SerializeField]
    private float climbRange;

    [SerializeField]
    private float climbHeight;

    [SerializeField]
    private float climbSpeed;



    

    void Start()
    {
        RayPoint.position = new Vector3(transform.position.x, climbHeight, transform.position.z);
        if (!rig)
        {
            rig = GetComponent<Rigidbody>();
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        coll = true;
    }
    private void OnTriggerExit(Collider other)
    {
        coll = false;
    }

    void FixedUpdate()
    {
        if(!Physics.Raycast(RayPoint.position, rig.transform.forward, 1))
        {
            Ray = true;
        }
        else
        {
            Ray = false;
        }
        if (Physics.Raycast(MidPoint.position, rig.transform.forward, climbRange))
        {
            Mid = true;
        }
        else
        {
            Mid = false;
        }

        if (Input.GetKey(KeyCode.G))
        {
            RayPoint.position = new Vector3(transform.position.x, climbHeight, transform.position.z);
        }

        if (coll)
        {
            if (!Physics.Raycast(RayPoint.position, rig.transform.forward, 1) && Physics.Raycast(MidPoint.position, rig.transform.forward, climbRange))
            {
                if (Input.GetKey(KeyCode.E))
                {
                    rig.AddForce(rig.transform.up * climbSpeed);
                }
            }
        }
        
    }
}
