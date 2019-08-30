using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement3D : MonoBehaviour
{
    private Vector3 AimDirection = Vector3.zero;
    private Vector3 Target = Vector3.zero;
    private Vector2 mov;


    public Animator animController;
    private Rigidbody rig;
    [SerializeField]
    private int jumpsleft;
    [SerializeField]
    private bool onGround;
    private Vector2 camRotation;
    private Vector3 camStandardPos;
    private bool cooldown;

    private float showVel;
    private Vector3 lastfixedVel;

    [SerializeField]
    Transform footpoint;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float boostspeed;
    [SerializeField]
    public Slider speedduration;
    [SerializeField]
    private float acceleration;
    [SerializeField]
    private float jumpaccelration = 0.4f;
    [SerializeField]
    private float turn;
    [SerializeField]
    private float raylength;
    [SerializeField]
    private float jumpheight;

    public static float gravity = 100;
    [SerializeField]
    private float friction;
    [SerializeField]
    private Transform camPoint;
    [SerializeField]
    private Transform camPos;
    [SerializeField]
    private AnimationCurve plot;
    [SerializeField]
    private AnimationCurve turnOverSpeed;

    // Start is called before the first frame update
    void Start()
    {
        if (!rig)
        {
            rig = GetComponent<Rigidbody>();
        }
        if (!animController)
        {
            animController = GetComponent<Animator>();
        }
        camStandardPos = camPos.localPosition;

    }

    // Update is called once per frame
    void Update()
    {

        Time.timeScale = Input.GetKey(KeyCode.B) ? .01f : 1;
        if (Physics.Raycast(new Ray(footpoint.position, Vector3.down), raylength, LayerMask.GetMask("Ground")))
        {
            onGround = true;
            
        }
        else
        {
            onGround = false;
        }

        if (onGround)
        {
            jumpsleft = 2;
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpsleft > 0)
        {
            if (jumpsleft == 2)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + raylength * 2, transform.position.z);
         
                //rig.position = transform.position.normalized * raylength * 2 + rig.position;
            }
            rig.velocity.Set(rig.velocity.x, 0, rig.velocity.z);
            rig.AddForce(transform.up * jumpheight);
            jumpsleft--;
        }



        mov.x = Input.GetAxis("Horizontal");
        mov.y = Input.GetAxis("Vertical");
        if (Jump3D.lockMovement)
        {
            mov = Vector2.zero;
        }
        else
        {
            mov = Vector2.ClampMagnitude(mov, 1);
        }

        Vector2 maus;
        maus.x = Input.GetAxis("Mouse X");
        maus.y = -Input.GetAxis("Mouse Y");

        camRotation += maus;
        camRotation.y = Mathf.Clamp(camRotation.y, -60, 80);

        camPos.localPosition = camStandardPos;
        RaycastHit hit;

        //Cam Offset
        if (Physics.Raycast(new Ray(camPoint.position, camPos.position - camPoint.position), out hit, (camPos.position - camPoint.position).magnitude))
        {
            camPos.position = hit.point + ((camPos.position - camPoint.position).normalized * 0.01f);
        }


        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

        float lerpfactor = (Time.time - Time.fixedTime) / Time.fixedDeltaTime;
        Vector3 lerpvector = Vector3.Lerp(lastfixedVel, rig.velocity, lerpfactor);


        //Debug.Log(lerpvector - lastfixedVel);
        //Animation
        animController.SetFloat("WalkingState", Mathf.SmoothDamp(animController.GetFloat("WalkingState"), lerpvector.magnitude / boostspeed, ref showVel, 0.1f));
        animController.SetFloat("JumpingState", lerpvector.y);
        animController.SetBool("OnGround", onGround);


    }
    private void LateUpdate()
    {
        camPoint.rotation = Quaternion.AngleAxis(camRotation.x, transform.up);// * Quaternion.AngleAxis(camRotation.y, transform.right);

    }


    private void FixedUpdate()
    {

        lastfixedVel = rig.velocity;

        float acc = acceleration;
        //  rig.rotation = Quaternion.AngleAxis(maus.x, transform.up) * rig.rotation;

        if (!onGround)
        {
            acceleration *= jumpaccelration;
        }
        Vector3 vel = rig.velocity;
        vel.y = 0;

        // Sprint
        if (Input.GetKey(KeyCode.LeftShift) && speedduration.value > 1)
        {
            rig.AddForce(acceleration * (Quaternion.AngleAxis(camRotation.x, Vector3.up) * new Vector3(mov.x, 0, mov.y) - vel / boostspeed));
            speedduration.value--;
        }
        else
        {

            rig.AddForce(acceleration * (Quaternion.AngleAxis(camRotation.x, Vector3.up) * new Vector3(mov.x, 0, mov.y)  - vel / speed));
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                speedduration.value += 0.5f;
            }
        }

        // Gravity
        if (rig.velocity.y < 0)
        {
            rig.AddForce(Vector3.down * gravity * 1.5f); // Gravity
        }
        else
        {
            rig.AddForce(Vector3.down * gravity); // Gravity
        }

        Vector2 flooredVelocity = new Vector2(rig.velocity.x, rig.velocity.z);

        float delta = Vector2.SignedAngle(
                new Vector2(transform.forward.x, transform.forward.z).normalized,
                flooredVelocity.normalized);
        rig.AddTorque(0, -(delta * delta * delta) * turn * turnOverSpeed.Evaluate(flooredVelocity.magnitude), 0);
        plot.AddKey(new Keyframe(Time.time, rig.rotation.eulerAngles.y));


        acceleration = acc;

        
    }
}
