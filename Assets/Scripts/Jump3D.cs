using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump3D : MonoBehaviour
{

    private Rigidbody rig;

    public Animator animController;

    public static bool lockMovement;

    [SerializeField]
    private float height;
    [SerializeField]
    private Transform RayPoint;
    [SerializeField]
    private Transform MidPoint;
    [SerializeField]
    private Transform HeightPoint;
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

    RaycastHit hitpoint;

    private float gravityvalue;


    void Start()
    {
        RayPoint.position = new Vector3(transform.position.x, climbHeight, transform.position.z);
        Debug.Log(climbRange);
        HeightPoint.position = new Vector3(transform.position.x, climbHeight, climbRange);
        if (!rig)
        {
            rig = GetComponent<Rigidbody>();
        }
        if (!animController)
        {
            animController = GetComponent<Animator>();
        }
        gravityvalue = PlayerMovement3D.gravity;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(RayPoint.position, transform.forward * climbRange);
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

        // Debug.DrawRay(MidPoint.position, transform.forward, Color.green, climbRange);
        // Debug.DrawRay(RayPoint.position, transform.forward, Color.green, climbRange);
        if (!Physics.Raycast(RayPoint.position, rig.transform.forward, 1))
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

        if (animController.GetBool("IsClimbing"))
        {
            if(transform.position.y >= hitpoint.point.y)
            {
                if(transform.position.z != hitpoint.point.z || transform.position.x != hitpoint.point.x)
                {
                    float delta = climbSpeed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(hitpoint.point.x, transform.position.y, hitpoint.point.z), delta);
                }
                else
                {
                    
                    animController.SetBool("IsClimbing", false);
                    PlayerMovement3D.gravity = gravityvalue;
                    lockMovement = false;
                }
            }
            else
            {
                rig.AddForce(rig.transform.up * climbSpeed);
            }
        }
        else
        {
            if (coll)
            {
                
                Ray x = new Ray(HeightPoint.position, transform.up * -1);
                Physics.Raycast(x, out hitpoint);
                height = hitpoint.point.y;
                if (!Physics.Raycast(RayPoint.position, rig.transform.forward, 1) && Physics.Raycast(MidPoint.position, rig.transform.forward, climbRange))
                {
                    if (Input.GetKey(KeyCode.E))
                    {
                        animController.SetBool("IsClimbing", true);
                        PlayerMovement3D.gravity = 0;
                        lockMovement = true;
                        rig.AddForce(rig.transform.up * climbSpeed);
                    }
                }
            }
        }
    }
}
