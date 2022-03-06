using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    [SerializeField] Transform cameraPosition;

    [Header("Keybinds")]
    [SerializeField] KeyCode crouchKey = KeyCode.LeftShift;

    private float crouchDistance = 0.5f;

    void Update()
    {

        if (Input.GetKey(crouchKey)){ 
        transform.position = cameraPosition.position + new Vector3(0, -crouchDistance, 0);
        } 
        else
        {
            transform.position = cameraPosition.position;
        }
    }
}
