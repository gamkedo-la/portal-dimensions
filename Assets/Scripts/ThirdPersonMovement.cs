using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 66f;
    private float walkingSpeed;
    private float runningSpeed;
    private float runBoostSpeed;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public float gravity = -9.81f;
    Vector3 velocity;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = .04f;
    public LayerMask groundMask;

    bool isGrounded;
    bool isRunning = false;
    bool runBoostActive = false;

    private void Start()
    {
        walkingSpeed = speed;
        runningSpeed = speed * 1.5f;
        runBoostSpeed = speed * 2.2f;
    }

    void Update()
    {
        Fall();
        Run();
        if (runBoostActive)
            Move(runBoostSpeed);
        else if(!isRunning)
            Move(walkingSpeed);
        else 
            Move(runningSpeed); 
        Jump();
    }

    private void Fall()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    private void Move(float currentSpeed)
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * currentSpeed * Time.deltaTime);


        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            isRunning = true;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift) || !isGrounded)
        {
            isRunning = false;
        }
    }

    public void UpdateRunBoost()
    {
        if (!runBoostActive)
            runBoostActive = !runBoostActive;
    }
}
