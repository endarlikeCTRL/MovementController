using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{

    [SerializeField] Transform orientation;

    [Header("Wall Running")]
    [SerializeField] float wallDistance = .5f;
    [SerializeField] float minimumJumpHeight = 1.55f;

    bool wallLeft = false;
    bool wallRight = false;

    bool CanWallRun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight);
    }

    void CheckWall()
    {
        wallLeft = Physics.Raycast(transform.position, -orientation.right, wallDistance);
        wallRight = Physics.Raycast(transform.position, orientation.right, wallDistance);
    }

    private void Update()
    {
        CheckWall();

        if (CanWallRun())
        {
            if (wallLeft)
            {
                Debug.Log("wall running on the left");
            }
            else if (wallRight)
            {
                Debug.Log("wall running on the Right");
            }
        }
    }

}
