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
    private bool Mid;
    [SerializeField]
    private bool Ray;

    [SerializeField]
    private float climbRange;

    [SerializeField]
    private float climbHeight;

    [SerializeField]
    private float climbSpeed;
    [SerializeField]
    private float animSpeedMultiplier;

    RaycastHit hitpoint;

    private float gravityvalue;

    Vector3 targetpoint = Vector3.zero;

    AnimatorTransitionInfo test;

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
            if(transform.position.y >= targetpoint.y)
            {
                rig.velocity = new Vector3(rig.velocity.x, 0, rig.velocity.z);
                if (transform.position.z != targetpoint.z || transform.position.x != targetpoint.x)
                {
                    float delta = climbSpeed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetpoint.x, transform.position.y, targetpoint.z), delta);
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

                
                Ray x = new Ray(HeightPoint.position, transform.up * -1);
                Physics.Raycast(x, out hitpoint);
                
                if (!Physics.Raycast(RayPoint.position, rig.transform.forward, 1) && Physics.Raycast(MidPoint.position, rig.transform.forward, climbRange))
                {
                    if (Input.GetKey(KeyCode.E))
                    {
                        targetpoint = hitpoint.point;
                        height = targetpoint.y;
                        animController.SetFloat("ClimbingState", height * 0.1f);
                        animController.SetBool("IsClimbing", true);
                       // float cliplength = animController.GetCurrentAnimatorClipInfo(0)[0].clip.length;
                        float speed = (height / climbHeight) * animSpeedMultiplier;
                        Debug.Log(speed);
                        animController.SetFloat("ClimbingSpeed", speed);
                        PlayerMovement3D.gravity = 0;
                        lockMovement = true;
                        rig.AddForce(rig.transform.up * climbSpeed);
                    }
                }
            
        }
    }
}
