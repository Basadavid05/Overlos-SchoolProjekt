using System.Collections;
using UnityEditor;
using UnityEngine;

public class MoveControl : MonoBehaviour
{
    [Header("Other-Components")]
    private Rigidbody rb;
    private Transform player;

    [Header("Speed")]
    public float speed;
    private float Walkspeed = 9f;
    private float Sprintspeed = 13f;
    private float Crouchspeed = 5f;
    private float Acceleration = 0.9f;
    private float SlideSpeed = 10f;
    private float Waterwalk = 11f;
    private float UnderWaterMoving = 15f;

    [Header("Keybind")]
    [HideInInspector] public KeyCode Sprintkey = KeyCode.LeftShift;
    [HideInInspector] public KeyCode jumpkey = KeyCode.Space;
    [HideInInspector] public KeyCode Slidekey = KeyCode.LeftControl;
    [HideInInspector] public KeyCode Chrouchkey = KeyCode.C;
    [HideInInspector] public KeyCode IsAltkey = KeyCode.LeftAlt;

    [Header("bools")]
    private bool IsTryingToSprinting;
    private bool IsTryingToSliding;
    private bool IsAltkeyDown;
    private bool IsTryingToChrouch;
    private bool isnotmoving;
    [HideInInspector] public bool isOnGround;
    private bool Ceilingup;

    [Header("Water")]
    public LayerMask Water;
    [HideInInspector] public bool InWater=false;
    [HideInInspector] public bool ISUnderwater=false;
    [HideInInspector] public bool PlayerisUnderWater=false;
    public static bool UnderwaterEffects=false;
    private Transform Center;


    [Header("Check")]
    private Transform groundCheck;
    private Transform CeilingCheck;

    [Header("Ground")]
    private float groundDistance = .5f;
    public LayerMask groundmask;
    private float celingdistancecheck;

    [Header("Slope")]
    [HideInInspector] public bool isOnSlope;
    private float maxSlopeAngle = 35f;
    private float slopeAdjustmentForce;
    private RaycastHit slopeHit;
    private float raycastOffset =1.5f;

    [Header("Drag")]
    private float groundDrag = 4f;
    private float gravity = 20f;
    private float airDrag = 0.2f;
    private float terminalVelocity = 10f;
    private float ActiveDownForce;

    [Header("Jump")]
    private float jumpheight = 13f;
    private bool readytojump;
    private bool cannotjump;
    private bool ExitSlope;
    private bool jumping;

    [Header("Character-Heights")]
    private float currentHeight;
    private float standingHeight = 2f; // Height when standing
    private float crouchHeight = 1f; // Height when crouching
    private float Heightarget;
    private bool failingfromsky;
    private float horizontalInput, verticalInput;

    [Header("Chrouching")]
    private int cKeyPressCount;
    private bool changegrav;
    private float TransitionSpeed = 3f;
    private bool help;
    private bool ChrouchIsOn => Heightarget >= crouchHeight && standingHeight - currentHeight > .1f;
    private bool something => ChrouchIsOn ? (currentHeight - Heightarget) > .1f : (Heightarget-currentHeight)>.1f;

    [Header("Sliding--Needing")]
    private Transform orientation;

    [Header("Sliding")]
    private bool sliding;
    private float maxSlideTime = 0.75f;
    private float slideForce;
    private float slideTimer=.5f;
    private float slideYscale = 0.75f;

    [Header("Animations")]
    public static Animator Animator;
    private static string LegMovementAnimation;
    public static string UpperBodyMovementAnimation;
    private string PreviusAnimation;
    private static bool NoUpperAnimation;


    private Vector3 previousPosition;
    public float maxFallDamageHeight = 10f;
    public float maxFallDamage = 50f;


    private void Start()
    {
        DefaultBools();
    }

    private void DefaultBools()
    {
        rb = GetComponent<Rigidbody>();
        Animator = transform.Find("Skin").GetComponent<Animator>();

        orientation = transform.Find("Center");
        CeilingCheck = transform.Find("CeilingCheck");
        Center = transform.Find("Center");
        groundCheck = transform.Find("GroundCheck");

        rb.freezeRotation = true;
        readytojump = true;

        currentHeight = standingHeight;
        player = this.transform;
        cKeyPressCount = 0;
        help = false;
        previousPosition = transform.position;
    }

    private void SwitchMovement()
    {
        InWater = !InWater;

        rb.useGravity = !rb.useGravity;
    }

    private void Update()
    {
        BoolControl();
        SpeedControl();
        GroupOfIfs();
        Gravity();
        //Slope();
        ControlDrag();
        SpeedLimiter();
        ControlDrag();
        if (NoUpperAnimation) { NormalAnimation(); }
    }

    private void BoolControl()
    {
        isOnGround = Physics.Raycast(groundCheck.position, Vector3.down,groundDistance, groundmask);
        Ceilingup = Physics.Raycast(CeilingCheck.position, Vector3.up, celingdistancecheck);

        cannotjump = Ceilingup || !isOnGround || ChrouchIsOn || sliding || PlayerisUnderWater;
        celingdistancecheck = ChrouchIsOn ? 1f : 0.4f;

        isnotmoving = (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0 && isOnGround);

        IsTryingToSprinting = Input.GetKey(Sprintkey);
        IsTryingToChrouch = Input.GetKey(Chrouchkey);
        IsTryingToSliding = Input.GetKeyDown(Slidekey);
        IsAltkeyDown = Input.GetKeyDown(IsAltkey);

        gravity = Physics.gravity.magnitude;
        isOnSlope = HandleSlope();
        rb.useGravity = !HandleSlope();
    }

    private void FixedUpdate()
    {
        UnderneathorFloating();

        if (sliding)
        {
            SlidingMovement();
        }
        if(!InWater && !isOnGround)
        {
            /*if (IsFalling())
            {
                CalculateFallDamage();
            }*/
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (jumping)
        {
                jumping = false;
                Invoke(nameof(JumpCooldown), 2f);
        }
    }
        
    private void ControlDrag()
    {
        if (isOnGround || ISUnderwater)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag*5;
        }

    }

    private void SpeedControl()
    {
        if (!PlayerisUnderWater && isOnGround)
        {
            if (isnotmoving) {
                speed = Mathf.Lerp(speed, 0, Time.deltaTime);
                ChangeAnimation("stand", false);
            }
            else if(!isnotmoving && !IsTryingToSprinting)
            {
                speed = Mathf.Lerp(speed, Walkspeed, Acceleration * Time.deltaTime);
                ChangeAnimation("walk", false);
            }
            else if (!isnotmoving && IsTryingToSprinting && !ChrouchIsOn)
            {
                speed = Mathf.Lerp(speed, Sprintspeed, Acceleration * Time.deltaTime);
                ChangeAnimation("run", false);
            }
            else if (sliding && !isnotmoving)
            {
                if (!HandleSlope() || rb.velocity.y > -0.1f)
                {
                    speed = Mathf.Lerp(speed, SlideSpeed, Acceleration * Time.deltaTime);
                    ChangeAnimation("sliding", false);
                }
                else
                {
                    speed = Mathf.Lerp(speed, SlideSpeed + slideForce, Acceleration * Time.deltaTime);
                    ChangeAnimation("sliding", false);
                }
            }
            else if ((ChrouchIsOn && !isnotmoving && !sliding) || IsAltkeyDown) //crouching
            {
                speed = Mathf.Lerp(speed, Crouchspeed, Acceleration * Time.deltaTime);
                ChangeAnimation("crouch", false);
            }
        }
        else if (PlayerisUnderWater)
        {
            if(Input.GetKey(Sprintkey))
            {
                speed = Mathf.Lerp(speed, UnderWaterMoving, Acceleration * Time.deltaTime);
                ChangeAnimation("fastswim", false);
            }
            else
            {
                speed = Mathf.Lerp(speed, Waterwalk, Acceleration * Time.deltaTime);
                ChangeAnimation("swim", false);
            }
        }
        else if(!InWater && !isOnGround) 
        {
            speed = Mathf.Lerp(speed, 7, Acceleration * Time.deltaTime);
            ChangeAnimation("fall", false);
        }

    }

    private void Gravity()
    {
        if(!PlayerisUnderWater && !isOnGround && !jumping)
        {
            rb.AddForce(Vector3.down * gravity * rb.mass * 15f);
            //Debug.Log("s");
        }
        else if(!PlayerisUnderWater && isOnGround && !jumping)
        {
            if (HandleSlope())
            {
                if (!GoingUpInSlope())
                {
                    rb.AddForce(Vector3.down * gravity * rb.mass * 8);
                }
                else
                {
                    rb.AddForce(Vector3.down * gravity * rb.mass / 3);
                }
            }
            else
            {
                //Debug.Log("se");
                rb.AddForce(Vector3.down * gravity * rb.mass);
            }
        }
        else if (PlayerisUnderWater)
        {
            rb.AddForce(Vector3.down * 3);
        }
        else if(jumping)
        {
            rb.AddForce(Vector3.down*1);
            //Debug.Log("ss");
        }
        
    }

    private void SpeedLimiter()
    {
        // Check if the character is on a slope and hasn't exited the slope
        if (HandleSlope() && !ExitSlope)
        {
            // If the magnitude of the velocity is greater than the specified speed
            if (rb.velocity.magnitude > speed)
            {
                // Limit the velocity to the specified speed
                rb.velocity = rb.velocity.normalized * speed;
            }
        }
        else
        {
            // If not on a slope or has exited the slope

            // Extract only the horizontal component of the velocity
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // If the magnitude of the horizontal velocity is greater than the specified speed
            if (flatVel.magnitude > speed)
            {
                // Limit the horizontal velocity to the specified speed
                Vector3 limitedVel = flatVel.normalized * speed;

                // Set the updated velocity while maintaining the vertical component
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    /*
    private bool IsFalling()
    {
        // Cast a ray downwards to detect ground
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, groundmask))
        {
            // Check if the distance to the ground is greater than a threshold
            if (hit.distance > maxFallDamageHeight)
            {
                return true; // Character is falling
            }
        }

        return false; // Character is not falling
    }

    private void CalculateFallDamage()
    {
        float fallHeight = Mathf.Max(0f, transform.position.y - previousPosition.y); // Calculate fall height
        float damage = 0f;

        if (fallHeight > maxFallDamageHeight)
        {
            // Calculate fall damage based on fall height
            damage = Mathf.Lerp(0f, maxFallDamage, (fallHeight - maxFallDamageHeight) / (maxFallDamageHeight * 2));
        }

        // Apply fall damage to the character's health or any relevant variable
        // For example, you can reduce health by the calculated damage amount
        // Replace this line with your actual logic to apply fall damage
        Debug.Log("Fall Damage: " + damage);
    }*/

    private void GroupOfIfs()
    {
        if (!ISUnderwater)
        {
            if (Input.GetKeyDown(jumpkey) && !cannotjump && readytojump && isOnGround)
            {
                Jump();
                ChangeAnimation("jump", true);
            }

            

            Sliding();

            if (isOnGround)
            {
                ToggleCrouch();
            }
        }


    }


    private void Jump()
    {
            readytojump = false;
            ExitSlope = true;
            // Calculate the jump force based on the desired jump height
            float jumpForce = Mathf.Sqrt(1.3f* jumpheight * 10);

            // Apply the jump force
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            jumping=true;

    }

    private void JumpCooldown()
    {
        readytojump = true;
        ExitSlope = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            SwitchMovement();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            SwitchMovement();
        }
    }

    private void UnderneathorFloating()
    {
        bool swimCheck = false;
        UnderwaterEffects = false;
        PlayerisUnderWater = false;
        if (InWater)
        {
            RaycastHit hit;
            if (Physics.Raycast(new Vector3(player.position.x, player.position.y + 0.5f, player.position.z), Vector3.down, out hit, 2f, Water))
            {
                if (hit.distance < 0.5f)
                {
                    swimCheck = false;
                }
            }
            else
            {
                swimCheck = true;
            }

            PlayerisUnderWater = Physics.CheckSphere(Center.position, 0.1f, Water);
            UnderwaterEffects = Physics.CheckSphere(CeilingCheck.position+new Vector3(0,11,0), 0.1f, Water);
        }


        ISUnderwater = swimCheck;
    }

    private bool HandleSlope()
    {
        RaycastHit slopeHit;
        Ray ray = new Ray(groundCheck.position, Vector3.down);

        // Check if there is a slope beneath the character
        if (Physics.Raycast(ray, out slopeHit, raycastOffset))
        {
            // Calculate the slope angle
            float slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return slopeAngle < maxSlopeAngle && slopeAngle != 0;
        }

        return false;
    }

    private bool GoingUpInSlope()
    {
        RaycastHit slopeHit;
        Ray ray = new Ray(groundCheck.position, Vector3.down);
        if (Physics.Raycast(ray, out slopeHit, raycastOffset))
        {

            // Check if moving up or down
            Vector3 currentPosition = transform.position;
            if (currentPosition.y > previousPosition.y)
            {
                // Moving up the slope
                return true;
            }
            else if (currentPosition.y < previousPosition.y)
            {
                // Moving down the slope
                return false;
            }

            // Update previous position
            previousPosition = currentPosition;
        }
        return false;
    }

    private void Slope()
    {
        if (HandleSlope())
        {
            RaycastHit slopeHit;
            Ray ray = new Ray(groundCheck.position, Vector3.down);
            if (Physics.Raycast(ray, out slopeHit, raycastOffset))
            {
                // Calculate the slope angle
                float slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
                Quaternion slopeRotation = Quaternion.Euler(-slopeAngle, transform.localRotation.y, transform.localRotation.z);
                transform.localRotation = slopeRotation;
            }

        }
    }

    private void ToggleCrouch()
    {

        if (Input.GetKeyDown(Chrouchkey))
        {
            cKeyPressCount++;
        }
        if (cKeyPressCount == 1)
        {
            Heightarget = crouchHeight;
            SeizeChange(Heightarget);
        }
        if (cKeyPressCount == 0)
        {
            Heightarget = standingHeight;
        }
        else if (cKeyPressCount == 2)
        {
            Heightarget = standingHeight;
            StandUp();
            SeizeChange(Heightarget);
            cKeyPressCount = 0;
        }

    }

    private void SeizeChange(float Heightarget)
    {
        if (currentHeight != Heightarget && something)
        {
            float crouchdelta = Time.deltaTime * TransitionSpeed;
            currentHeight = Mathf.Lerp(currentHeight, Heightarget, crouchdelta);
            rb.transform.localScale = new Vector3(1, currentHeight, 1);
        }
        if (currentHeight != Heightarget && !something)
        {
            rb.transform.localScale = new Vector3(1, Heightarget, 1);
            currentHeight = Heightarget;
        }
    }

    private void StandUp()
    {
        Vector3 castorigin = CeilingCheck.position;

        if (Physics.Raycast(CeilingCheck.position, Vector3.up, out RaycastHit hit, celingdistancecheck))
        {
            float DistancetoCeiling = hit.point.y - CeilingCheck.position.y; //castorigin.y;
            Heightarget = Mathf.Max
            (
            currentHeight + DistancetoCeiling - 0.15f,
            currentHeight
            );
        }

    }

    private void Sliding()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(Slidekey) && (horizontalInput != 0 || verticalInput != 0) && isOnGround)
        {
            StartSliding();
        }
        else if (Input.GetKeyUp(Slidekey) && sliding)
        {
            StopSliding();
        }
    }

    private void StartSliding()
    {
        sliding = true;
        SeizeChange(slideYscale);
        slideTimer = maxSlideTime;
    }

    private void SlidingMovement()
    {
        Vector3 inputDirection = (verticalInput * orientation.forward + horizontalInput * orientation.right).normalized;
        if (!HandleSlope() || rb.velocity.y > -0.1f)
        {
            //rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);
            slideTimer -= Time.deltaTime;
        }
        else
        {
            //rb.AddForce(GetSlopeMoveDirection(inputDirection) * slideForce, ForceMode.Force);
            if (slideForce < 10)
            {
                Invoke(nameof(SlideForceExp), 2.5f);
            }
        }

        if (slideTimer <= 0)
        {
            StopSliding();
        }
    }

    private void SlideForceExp()
    {
        slideForce++;
    }

    private void StopSliding()
    {
        sliding = false;
        StandUp();
        SeizeChange(standingHeight);
        slideForce = 0;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 movement)
    {
        return Vector3.ProjectOnPlane(movement, slopeHit.normal).normalized;
    }


    private void ChangeAnimation(string animation,bool hunt, int layer = 0, float crossfade = 0.2f)
    {
        if (LegMovementAnimation != animation)
        {
            LegMovementAnimation = animation;
            if (hunt)
            {
                if (Animator != null)
                {
                    Animator.Play(animation, layer);
                }
            }
            else
            {
                if(Animator != null)
                {
                    Animator.CrossFade(animation, crossfade, layer);
                }
            }
            PreviusAnimation= animation;
        }
    }

    private void NormalAnimation(int layer = 1, float crossfade = 0.2f)
    {
        if (LegMovementAnimation != PreviusAnimation)
        {
            Animator.CrossFade(LegMovementAnimation, crossfade, layer);
        }
    }

    public static void ChangeAction(string animation, int layer = 1, float crossfade = 0.2f)
    {
        if (UpperBodyMovementAnimation != animation)
        {
            NoUpperAnimation = false;
            UpperBodyMovementAnimation = animation;
            if (UpperBodyMovementAnimation==" ")
            {
                NoUpperAnimation=true;
            }
            else
            {
                Animator.Play(animation, layer);
            }

        }
    }


    /*
      if (!ExitSlope && !isnotmoving)
        {
            if (rb.velocity.y >= 0)
            {
                ActiveDownForce = 80f;
            }
            if (rb.velocity.y < -0.1)
            {
                if (sliding)
                {
                    ActiveDownForce = 500f;
                }
                else
                {
                    ActiveDownForce = 500f;
                }
            }
        }
        else if (isnotmoving)
        {
            ActiveDownForce = 1f;
        }
     
     */


}
