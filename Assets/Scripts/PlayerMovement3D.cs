using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3D : MonoBehaviour
{
    private Vector3 AimDirection = Vector3.zero;
    private Vector3 Target = Vector3.zero;
    private Vector2 mov;
    private Vector2 maus;
    private Rigidbody rig;
    private int jumpsleft;
    private bool onGround;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float acceleration;
    [SerializeField]
    private float raylength;
    [SerializeField]
    private float jumpheight;
    [SerializeField]
    private float gravity;
    [SerializeField]
    private float friction;


    [SerializeField]
    private AnimationCurve plot;

    // Start is called before the first frame update
    void Start()
    {
        if (!rig)
        {
            rig = GetComponent<Rigidbody>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = Input.GetKey(KeyCode.LeftShift) ? .1f : 1;
        if(Physics.Raycast(new Ray(transform.position, Vector3.down), raylength, LayerMask.GetMask("Ground")))
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
            if(jumpsleft == 2)
            {
                rig.position = transform.position.normalized * raylength * 2 + rig.position;
            }
            rig.AddForce(transform.up * jumpheight);
            jumpsleft--;
        }


        mov.x = Input.GetAxis("Horizontal");
        mov.y = Input.GetAxis("Vertical");


        maus.x = Input.GetAxis("Mouse X");
        maus.y = Input.GetAxis("Mouse Y");

        if (Input.GetMouseButton(1)){
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
        


    }
    private void LateUpdate()
    {
        plot.AddKey(Time.time, rig.velocity.magnitude);
    }
    private void FixedUpdate()
    {
        float acc = acceleration;
        rig.rotation = Quaternion.AngleAxis(maus.x, transform.up) * rig.rotation;

        /*if (mov.x == 0 && mov.y == 0 && Physics.Raycast(new Ray(transform.position, Vector3.down), raylength, LayerMask.GetMask("Ground")))
        {
            rig.AddForce(-rig.velocity * friction);
        }*/
        if (!onGround)
        {
            acceleration *= 0.01f;
        }
        Vector3 vel = rig.velocity;
        vel.y = 0;
        rig.AddForce(acceleration * ((rig.rotation * Vector3.ClampMagnitude(new Vector3(mov.x, 0, mov.y), 1) * speed) - vel));

        //rig.velocity = Vector3.ClampMagnitude(rig.velocity, speed); // Maximale Geschwindigkeit

        if(rig.velocity.y < 0)
        {
            rig.AddForce(Vector3.down * gravity * 1.5f); // Gravity
        }
        else
        {
            rig.AddForce(Vector3.down * gravity); // Gravity
        }




        acceleration = acc;
    }
}
