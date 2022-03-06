using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] Transform orientation;

    [Header("Detection")]
    [SerializeField] float wallDistance = .5f;
    [SerializeField] float minimumJumpHeight = 1.55f;

    [Header("Wall Running")]
    [SerializeField] private float wallRunGRavity;
    [SerializeField] private float wallRunJumpForce;

    bool wallLeft = false;
    bool wallRight = false;
    bool wallFront = false;
    bool wallBack = false;

    RaycastHit leftWallHit;
    RaycastHit rightWallHit;
    RaycastHit frontWallHit;

    private Rigidbody rb;

    bool CanWallRun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void CheckWall()
    {

        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallDistance);
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallDistance);
        wallFront = Physics.Raycast(transform.position, orientation.forward, out frontWallHit, wallDistance);

    }

    private void Update()
    {

        CheckWall();

        if (CanWallRun())
        {
            if (wallLeft)
            {
                StartWallRun();
                Debug.Log("wall running on the left");
            }
            else if (wallRight)
            {
                StartWallRun();
                Debug.Log("wall running on the Right");
            }
            else if (wallFront)
            {
                StartWallRun();
                Debug.Log("wall running on the front");
            }
            else
            {
                StopWallRun();
            }
        }
        else
        {
            StopWallRun();
        }

    }

    void StartWallRun()
    {

        rb.useGravity = false;

        rb.AddForce(Vector3.down * wallRunGRavity, ForceMode.Force);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (wallLeft)
            {
                Vector3 wallRunJumpDirection = transform.up + leftWallHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce * 100, ForceMode.Force);
            }
            else if (wallRight)
            {
                Vector3 wallRunJumpDirection = transform.up + rightWallHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce * 100, ForceMode.Force);
            }
        }

    }

    void StopWallRun()
    {
        rb.useGravity = true;
    }

}
