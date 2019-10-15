using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    Gravity gravity;
    Rigidbody rigidbody;

    void Start()
    {
        if (!gravity)
        {
            gravity = GetComponent<Gravity>();
        }
        if (!rigidbody)
        {
            rigidbody = GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        
    }

    public void HoldLedge(Transform col)
    {
        // freeze gravity while hanging
        gravity.FreezeGravity();
        // Ledge has same vertical rotation as player
        transform.eulerAngles = new Vector3(rigidbody.rotation.x, col.transform.rotation.y, rigidbody.rotation.z);
    }
}
