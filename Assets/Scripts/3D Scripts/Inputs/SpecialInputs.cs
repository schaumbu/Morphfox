using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialInputs : MonoBehaviour
{
    void Update()
    {
        Time.timeScale = Input.GetKey(KeyCode.B) ? .01f : 1;

        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
