using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameInput gameInput;

    [SerializeField]
    private float raylength;
    [SerializeField]
    private Transform camPoint;
    [SerializeField]
    private Transform camPos;

    public Vector2 camRotation;
    private Vector3 camStandardPos;

    void Start()
    {
        if (!gameInput)
        {
            gameInput = GetComponent<GameInput>();
        }
        // Save standard position of cam - Important for reseting cam position (e.g. after using offset)
        camStandardPos = camPos.localPosition;
    }

    void Update()
    {
        camRotation += gameInput.mouse;
        camRotation.y = Mathf.Clamp(camRotation.y, -60, 80);

        // Set Camera to standard position
        camPos.localPosition = camStandardPos;

        //Cam Offset
        RaycastHit hit;
        if (Physics.Raycast(new Ray(camPoint.position, camPos.position - camPoint.position), out hit, (camPos.position - camPoint.position).magnitude))
        {
            camPos.position = hit.point + ((camPos.position - camPoint.position).normalized * 0.01f);
        }

    }

    private void LateUpdate()
    {
        camPoint.rotation = Quaternion.AngleAxis(camRotation.x, transform.up);  
    }

}
