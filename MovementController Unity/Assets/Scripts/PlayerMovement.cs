using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    float playerHeight = 2f;

    [SerializeField] Transform orientation;

    

    [Header("Movement")]
    public float moveSpeed = 5f;
    [SerializeField] float airMultiplier = 0.35f;
    float movementMultiplier = 10f;

    [Header("Camera")]
    [SerializeField] private Camera cam;
    [SerializeField] private float fov;
    [SerializeField] private float crouchfovTime;
    [SerializeField] private float crouchfov;
    [SerializeField] private float sprintfovTime;
    [SerializeField] private float sprintfov;
    [SerializeField] private float zoomfovTime;
    [SerializeField] private float zoomfov;
    [SerializeField] private float jumpfovTime;
    [SerializeField] private float jumpfov;

    [Header("Sprinting")]
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float sprintSpeed = 10f;
    [SerializeField] float acceleration = 10f;
    [SerializeField] float crouchSpeed = 0.45f;

    [Header("Jumping")]
    public float jumpForce = 5f;
    private bool canDoubleJump = false;


    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftControl;
    [SerializeField] KeyCode crouchKey = KeyCode.LeftShift;
    [SerializeField] KeyCode zoomKey = KeyCode.C;

    [Header("Drag")]
    [SerializeField] float groundDrag = 6f;
    [SerializeField] float airDrag = 2f;


    float horizontalMovement;
    float verticalMovement;

    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundDistance = 0.2f;
    bool isGrounded;

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    Rigidbody rb;

    RaycastHit slopeHit;

    private bool OnSlope()
    {

        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;

    }

    private void Start()
    {

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

    }

    private void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        MyInput();
        ControlDrag();
        ControlSpeed();
        ControlCrouch();
        ControlZoom();

        if (Input.GetKey(sprintKey) && !isGrounded)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, sprintfov, sprintfovTime * Time.deltaTime);
        }
        else
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, sprintfovTime * Time.deltaTime);
        }

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            canDoubleJump = true;
            Jump();
        }
        else
        {
            if(Input.GetKeyDown(jumpKey) && canDoubleJump)
            {
                Jump();
                canDoubleJump = false;
            }
        }

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);

    }

    void MyInput()
    {

        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;

    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ControlCrouch()
    {

        if (Input.GetKey(crouchKey) && isGrounded)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, crouchSpeed, acceleration * Time.deltaTime);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, crouchfov, crouchfovTime * Time.deltaTime);
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, crouchfovTime * Time.deltaTime);
        }

    }

    void ControlZoom()
    {

        if (Input.GetKey(zoomKey))
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoomfov, zoomfovTime * Time.deltaTime);
        }
        else
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, zoomfovTime * Time.deltaTime);
        }

    }

    void ControlSpeed()
    {

        if (Input.GetKey(sprintKey) && isGrounded)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, sprintfov, sprintfovTime * Time.deltaTime);
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, sprintfovTime * Time.deltaTime);
        }

    }

    void ControlDrag()
    {

        if(isGrounded)
        {
            rb.drag = groundDrag;
        }
        
        else
        {
            rb.drag = airDrag;
        }

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {

        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }

    }

}
