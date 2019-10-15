using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    Rigidbody rigidbody;

    [SerializeField]
    private float gravity;

    private float savedGravity;

    void Start()
    {
        if (!rigidbody)
        {
            rigidbody = GetComponent<Rigidbody>();
        }
        savedGravity = gravity;
    }

    void Update()
    {
        if (rigidbody.velocity.y < 0)
        {
            rigidbody.AddForce(Vector3.down * gravity * 1.5f);
        }
        else
        {
            rigidbody.AddForce(Vector3.down * gravity);
        }
    }

    public void FreezeGravity()
    {
        if(gravity == 0)
        {
            gravity = savedGravity;
        }
        else
        {
            savedGravity = gravity;
            gravity = 0;
        }
    }
}
