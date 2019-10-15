using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    Rigidbody rigidbody;

    [SerializeField]
    private float gravity;

    private float savedGravity;

    #region Startfunction
    void Start()
    {
        if (!rigidbody)
        {
            rigidbody = GetComponent<Rigidbody>();
        }
        savedGravity = gravity;
    }
#endregion

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
        gravity = 0;
    }

    public void UnFreezeGravity()
    {
        gravity = savedGravity;
    }
}
