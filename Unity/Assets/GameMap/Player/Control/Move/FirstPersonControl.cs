using UnityEngine;

public class FirstPersonControl : MonoBehaviour
{
    [Header("Default Data")]
    private Rigidbody rb;
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform cameraTransform;

    [Header("Other Scripts")]
    private MoveControl Control;

    [Header("")]
    [SerializeField] private float airMultiplier = 0.4f;
    public float turnSpeed = 10f;
    public float UnderwaterForce = 33f;

    [Header("Control")]
    private float MoveSpeed;
    private float MouseX, MouseZ, MouseY;
    public Vector3 movement;

    [Header("bool")]
    private bool isGround;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Control = GetComponent<MoveControl>();
    }

    void FirstPersonInput()
    {
        MouseX = Input.GetAxis("Horizontal");
        MouseZ = Input.GetAxis("Vertical");
        MouseY = Input.GetAxis("Mouse Y");

        if (!Control.ISUnderwater)
        {
            movement = (MouseZ * orientation.forward + MouseX * orientation.right).normalized;
        }
        else if((Control.ISUnderwater))
        {
            movement = (MouseZ * orientation.forward+ MouseY * orientation.up + MouseX * orientation.right).normalized;
        }

        if (movement != Vector3.zero)
        {
            // Rotate the character to face the movement direction
            Quaternion newRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, turnSpeed * Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        isGround = Control.isOnGround;
        MoveSpeed = Control.speed;
        FirstPersonInput();
    }


    void FixedUpdate()
    {
        FirstPersonMoveplayer();
    }

    private void FirstPersonMoveplayer()
    {
        if (!Control.InWater)
        {
            //cameraTransform.localRotation = Quaternion.identity;
            // Your existing movement code for ground and air
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
        else
        {
            // Rotate the camera based on the input
            if (Control.ISUnderwater)
            {
                Controling();
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
                Controling();
                if (Input.GetKeyDown(KeyCode.LeftControl)) // Descend
                {
                    rb.AddForce(Vector3.down * MoveSpeed * UnderwaterForce, ForceMode.Acceleration);
                }
            }            
        }
    }

    private void Controling()
    {
        rb.AddForce(Vector3.Normalize(movement) * MoveSpeed * 10f, ForceMode.Acceleration);
    }

    private void RotateCamera()
    {
        float mouseY = Input.GetAxis("Mouse Y") * MoveSpeed;

        // Rotate the camera vertically based on mouse input
        cameraTransform.Rotate(-mouseY, 0, 0);
    }
}
