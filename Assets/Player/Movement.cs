using System.Collections;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    Camera playerCamera;

    [SerializeField]
    Transform playerCameraTrs;

    [SerializeField]
    Rigidbody playerBody;

    [SerializeField]
    float sensitivity = 1;

    [SerializeField]
    float moveSpeed;

    [SerializeField]
    float jumpForce;

    [SerializeField]
    GameObject groundCheck;


    [SerializeField]
    float groundDrag;

    public bool isGrappled;

    public float Zmultiplier = 1;
    public float Xmultiplier;

    public bool grounded;
    public bool exploded;

    bool inputReady;

    private float x;
    private float y;
    private Vector3 rotateValue;

    float rotationX;

    private void Start()
    {
        StartCoroutine(FixCamera());
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void Update()
    {
        RotateCamera();
        RotatePlayer();
        Jump();
        //GroundDrag();
        GrappleCheck();
        if (grounded)
        {
            exploded = false;
        }


    }

    void FixedUpdate()
    {
        
        Move();
        //LimitSpeed();
        //ExplodedManager();

    }

    private void GroundDrag()
    {
        if (grounded)
        {
            playerBody.linearDamping = groundDrag;
        }
        else
        {
            playerBody.linearDamping = 0;
        }
    }

    private IEnumerator FixCamera()
    {
        yield return null; // Wait 1 frame to allow scene/camera to load and stabilize

        Vector3 camEuler = playerCamera.transform.eulerAngles;

        // Convert Unity's 0–360° to signed -180 to 180 for X
        rotationX = -((camEuler.x > 180f) ? camEuler.x - 360f : camEuler.x);

        inputReady = true;
    }

    private void RotatePlayer()
    {
        this.transform.rotation = Quaternion.Euler(transform.eulerAngles.x, playerCameraTrs.eulerAngles.y, transform.eulerAngles.z);
    }

    private void Move()
    {
        float moveX = Input.GetAxisRaw("Vertical");
        float moveZ = Input.GetAxisRaw("Horizontal");

        Vector3 cameraForward = playerCamera.transform.forward;
        Vector3 cameraRight = playerCamera.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 forwardRelative = moveX * cameraForward * Xmultiplier;
        Vector3 rightRelative = moveZ * cameraRight * Zmultiplier;

        Vector3 moveDirection = forwardRelative + rightRelative;

        moveDirection = moveDirection.normalized;


        moveDirection.Normalize();

        
        if(grounded)
        {
            //playerBody.linearVelocity = new Vector3(moveDirection.x * moveSpeed, playerBody.linearVelocity.y, moveDirection.z * moveSpeed);//main movement
            playerBody.linearVelocity = new Vector3(moveDirection.x * moveSpeed, playerBody.linearVelocity.y, moveDirection.z * moveSpeed);
        }
        else //if (exploded || isGrappled)
        {
            playerBody.linearVelocity += new Vector3(moveDirection.x / 5f, 0, moveDirection.z / 5f);
        }


        //playerBody.AddForce(moveDirection.normalized * moveSpeed * 10, ForceMode.VelocityChange);
        //playerBody.MovePosition(playerBody.position + moveDirection.normalized * moveSpeed);


    }

    private void ExplodedManager()
    {
        Vector3 fallVel = new Vector3(0f, playerBody.linearVelocity.y, 0f);
        if (fallVel.magnitude <= 0)
        {
            exploded = false;
        }
    }



    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            playerBody.linearVelocity = new Vector3(playerBody.linearVelocity.x, jumpForce, playerBody.linearVelocity.z);
        }
    }
    
    private void RotateCamera()
    {
        if (!inputReady) return;

        y = Input.GetAxis("Mouse X");
        x = Input.GetAxis("Mouse Y");

        rotationX += x * sensitivity;
        rotationX = Mathf.Clamp(rotationX, -80, 80);

        rotateValue = new Vector3(rotationX, y * -1 * sensitivity, 0);
        playerCamera.transform.eulerAngles = new Vector3(-rotationX, playerCamera.transform.eulerAngles.y - y * -1 * sensitivity, 0);

        
    }

    private void GrappleCheck()
    {
        if (isGrappled)
        {
            groundCheck.SetActive(false);
            grounded = false;
        }
        else
        {
            groundCheck.SetActive(true);
        }
    }



}
