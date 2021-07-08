using UnityEngine;

public class playerMovement : MonoBehaviour
{
    //script attached to player object for movement
    //rigidbody
    private Rigidbody rb;
    //movement
    public bool move = true;
    private float forward;
    private float forwardDirection;
    private float right;
    private float rightDirection;
    private float left;
    private float leftDirection;
    private float back;
    private float backDirection;
    private Vector3 movement;
    //jump
    private float jumpResetTimer = 0;
    //ground check
    private bool grounded;
    private Vector3 groundCheckPosition;
    private LayerMask groundMask;
    //gravity
    private float gravity = -19.62f;
    //controls
    public string forwardInput = "e";
    public string rightInput = "f";
    public string leftInput = "s";
    public string backInput = "d";
    public string jumpInput = "space";
    //values
    private float forwardInital = 10;
    private float forwardMax = 25;
    private float forwardAcceleration = 5;
    private float horizontalInital = 5;
    private float horizontalMax = 20;
    private float horizontalAcceleration = 5;
    private float backInital = 10;
    private float backMax = 15;
    private float backAcceleration = 5;
    private float jump = 13;

    private void Start()
    {
        //rigidbody
        rb = transform.GetComponentInParent<Rigidbody>();
        //ground check
        groundMask = LayerMask.GetMask("ground");
    }

    private void FixedUpdate()
    {
        if (move)
        {
            Movement();
            GroundCheck();
            Gravity();
            Jump();
        }
    }

    private void Movement()
    {
        forwardDirection = Vector3.Dot(rb.velocity, transform.forward);
        forward = CalculateMovement(forwardInput, forwardDirection, forwardInital, forwardMax, forwardAcceleration, 5);
        rightDirection = Vector3.Dot(rb.velocity, transform.right);
        right = CalculateMovement(rightInput, rightDirection, horizontalInital, horizontalMax, horizontalAcceleration, 5);
        leftDirection = Vector3.Dot(rb.velocity, -transform.right);
        left = CalculateMovement(leftInput, leftDirection, horizontalInital, horizontalMax, horizontalAcceleration, 5);
        backDirection = Vector3.Dot(rb.velocity, -transform.forward);
        back = CalculateMovement(backInput, backDirection, backInital, backMax, backAcceleration, 5);
        Rigidbody();
    }

    private float CalculateMovement(string input, float velocity, float initial, float max, float acceleration, float decceleration)
    {
        //return value to add to velocity
        if (Input.GetKey(input))
        {
            if (grounded)
            {
                //key held and not in the air
                if (velocity < (initial - 0.1f))
                {
                    //remove current velocity then add initial
                    return (initial - velocity);
                }
                else if (velocity < max)
                {
                    //add acceleration to accelerate to max
                    return (acceleration * Time.deltaTime);
                }
                else
                {
                    //subtract decceleration to deccelerate back to max
                    return (-decceleration * Time.deltaTime);
                }
            }
            else
            {
                //if in the air, retain current velocity
                return 0;
            }
        }
        else
        {
            //key not held
            if (velocity > initial)
            {
                //subtract decceleration to deccelerate back to initial
                return (-decceleration * Time.deltaTime);
            }
            else if (velocity > 0)
            {
                //subtract current velocity to stop moving
                return -velocity;
            }
            else
            {
                return 0;
            }
        }
    }

    private void GroundCheck()
    {
        groundCheckPosition = transform.GetChild(1).position;
        grounded = Physics.CheckSphere(groundCheckPosition, 0.05f, groundMask);
    }

    private void Gravity()
    {
        rb.AddForce(transform.up * gravity * Time.deltaTime, ForceMode.Force);
    }

    private void Jump()
    {
        if(Input.GetKeyDown(jumpInput) && grounded && jumpResetTimer <= 0)
        {
            //jump
            rb.AddForce(transform.up * jump * Time.deltaTime, ForceMode.Impulse);
            //initiate timer to temprarily prevent jumping
            jumpResetTimer = 1;
        }

        TimeJumpReset();
    }

    void TimeJumpReset()
    {
        if(jumpResetTimer > 0)
        {
            //count down reset timer
            jumpResetTimer -= Time.deltaTime;
        }
    }

    private void Rigidbody()
    {
        movement = (transform.forward * forward + transform.right * right - transform.right * left - transform.forward * back);
        rb.velocity += movement;
    }
}
