using UnityEngine;

public class FirstController : MonoBehaviour
{
    [Header("Default Data")]
    private Rigidbody rb;
    private MoveControl Control;

    [Header("Camera")]
    private Transform orientation;
    public Camera cameraTransform;

    [Header("Look and Movement Settings")]
    private float MoveSpeed;
    private bool isGround;
    private float airMultiplier = 0.4f;

    [Header("Mouse")]
    private float MouseX;
    private float MouseY;

    [Header("Rotation")]
    private float xRotation;
    private float yRotation;
    public float smoothTime = 0.2f;

    [Header("Inputs")]
    public Vector3 movement;
    private float InputX, InputY, InputZ;
    public float turnSpeed = 10f;

    [Header("Mouse-Settings")]
    public float MouseSpeed = 1000;

    [Header("Underwater")]
    public float UnderwaterForce;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        orientation = transform.Find("Center");
        Control = GetComponent<MoveControl>();
        transform.localRotation = Quaternion.Euler(0, xRotation, 0);
    }

    private void Update()
    {
        HandleFirstCamera();
        HandleInput();
        isGround = Control.isOnGround;
        MoveSpeed = Control.speed;
    }

    private void HandleFirstCamera()
    {
        MouseX = Input.GetAxisRaw("Mouse X") * MouseSpeed * Time.deltaTime;
        MouseY = Input.GetAxisRaw("Mouse Y") * MouseSpeed * Time.deltaTime;

        yRotation += MouseX;
        xRotation -= MouseY;

        xRotation = Mathf.Clamp(xRotation, -90, 45f);
        yRotation = Mathf.Repeat(yRotation, 360f);
    }

    private void HandleInput()
    {
        if (InputX == 0 && InputZ == 0)
        {
            movement = Vector3.zero;
        }
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");
        InputY = Input.GetAxis("Mouse Y");

        if (!Control.ISUnderwater)
        {
            movement = (InputZ * orientation.forward + InputX * orientation.right).normalized;
        }
        else if ((Control.ISUnderwater))
        {
            float verticalInput = Mathf.Clamp(InputY, -1f, 1f);
            movement = (InputZ * orientation.forward + InputX * orientation.right).normalized;
        }
        
    }

    private void FixedUpdate()
    {
        cameraTransform.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        Quaternion targetRotation;
        if (Control.PlayerisUnderWater)
        {
            targetRotation = Quaternion.Euler(xRotation, yRotation, 0);
        }
        else
        {
            targetRotation = Quaternion.Euler(0, yRotation, 0);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        
        FirstPersonMoveplayer();
    }

    private void FirstPersonMoveplayer()
    {
        if (!Control.InWater)
        {
            if (movement != Vector3.zero) // Apply forces only when there's movement input
            {
                if (isGround && !Control.isOnSlope)
                {
                    Controling();
                }
                else if (isGround && Control.isOnSlope)
                {
                    rb.AddForce(Control.GetSlopeMoveDirection(movement) * MoveSpeed * 30f, ForceMode.Acceleration);
                    if (rb.velocity.y > 0)
                        rb.AddForce(Vector3.down * 300f, ForceMode.Force);
                }
                else if (!isGround)
                {
                    rb.AddForce(Vector3.Normalize(movement) * MoveSpeed * 10f * airMultiplier, ForceMode.Acceleration);
                }
            }

        }
        else
        {
            // Rotate the camera based on the input
            if (Control.PlayerisUnderWater)
            {
                if (movement != Vector3.zero) // Apply forces only when there's movement input
                {
                    Controling();
                }
                // Control for ascending and descending in water
                if (Input.GetKey(KeyCode.Space)) // Ascend
                {
                    rb.AddForce(Vector3.up * MoveSpeed * UnderwaterForce, ForceMode.Acceleration);
                }
                else if (Input.GetKeyDown(KeyCode.LeftControl) && !isGround) // Descend
                {
                    rb.AddForce(Vector3.down * MoveSpeed * UnderwaterForce, ForceMode.Acceleration);
                }
            }
            else
            {
                if (movement != Vector3.zero) // Apply forces only when there's movement input
                {
                    Controling();
                }
                if (Input.GetKeyDown(KeyCode.LeftControl)) // Descend
                {
                    rb.AddForce(Vector3.down * MoveSpeed * UnderwaterForce, ForceMode.Acceleration);
                }
            }
        }
    }

    private void Controling()
    {
        rb.AddForce(Vector3.Normalize(movement) * MoveSpeed*10, ForceMode.Acceleration);
    }

}
