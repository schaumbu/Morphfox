using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    private Rigidbody rigidbody;
    private CameraMovement cameraMovement;
    private GameInput gameInput;
    private Jump jump;
    private LedgeColliding ledgeCol;

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
    private AnimationCurve turnOverSpeed;

    private Vector3 lastFixedVel;

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
        if (!gameInput)
        {
            gameInput = GetComponent<GameInput>();
        }
        if (!rigidbody)
        {
            rigidbody = GetComponent<Rigidbody>();
        }
        if (!cameraMovement)
        {
            cameraMovement = GetComponent<CameraMovement>();
        }
    }

    void Update()
    {
        float lerpfactor = (Time.time - Time.fixedTime) / Time.fixedDeltaTime;
        Vector3 lerpvector = Vector3.Lerp(lastFixedVel, rigidbody.velocity, lerpfactor);

    }

    void FixedUpdate()
    {
        lastFixedVel = rigidbody.velocity;

        float acc = acceleration;

        if (!jump.grounded)
        {
            acceleration *= jumpaccelration;
        }

        Vector3 vel = rigidbody.velocity;
        vel.y = 0;

        if (!ledgeCol.hanging)
        {
            if (Input.GetKey(KeyCode.LeftShift) && speedduration.value > 1)
            {
                rigidbody.AddForce(acceleration * (Quaternion.AngleAxis(cameraMovement.camRotation.x, Vector3.up) * new Vector3(gameInput.wasd.x, 0, gameInput.wasd.y) - vel / boostspeed));
                speedduration.value--;
            }
            else
            {

                rigidbody.AddForce(acceleration * (Quaternion.AngleAxis(cameraMovement.camRotation.x, Vector3.up) * new Vector3(gameInput.wasd.x, 0, gameInput.wasd.y) - vel / speed));
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    speedduration.value += 0.5f;
                }
            }
        }
        else
        {
            if(gameInput.wasd.x > 0)
            {
                rigidbody.AddForce(acceleration * rigidbody.transform.right - vel / speed);
            }
            else if(gameInput.wasd.x < 0)
            {
                rigidbody.AddForce(acceleration * -rigidbody.transform.right - vel / speed);
            }
            else
            {
                rigidbody.velocity = rigidbody.velocity * 0.9f * Time.deltaTime;
            }
            
        }
        

        Vector2 flooredVelocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.z);

        float delta = Vector2.SignedAngle(
                new Vector2(transform.forward.x, transform.forward.z).normalized,
                flooredVelocity.normalized);
        rigidbody.AddTorque(0, -(delta * delta * delta) * turn * turnOverSpeed.Evaluate(flooredVelocity.magnitude), 0);

        acceleration = acc;
    }
}
