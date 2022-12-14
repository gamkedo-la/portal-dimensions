using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    //walking
    public float speed = 66f;
    private float walkingSpeed;
    private float runningSpeed;
    private float runBoostSpeed;
    public float turnSmoothTime = 0.1f;
    [HideInInspector] public float turnSmoothVelocity;

    //jumping/falling
    public float gravity = -9.81f;
    Vector3 velocity;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = .04f;
    public LayerMask groundMask;

    bool isGrounded;
    bool isRunning = false;
    bool isJumping = false;
    bool isMoving = false;
    bool runBoostActive = false;

    CameraController cameraController;
    PlayerBaseState currentState;
    PlayerStateFactory states;

    //getters and setters
    public PlayerBaseState CurrentState { get { return currentState; } set { currentState = value; } }
    public CharacterController Controller { get { return controller; } }
    public Transform GroundCheck { get { return groundCheck; } }
    public Transform Cam { get { return cam; } }
    public CameraController CameraController { get { return cameraController; } }
    public Vector3 Velocity { get { return velocity; } set { velocity = value; } }
    public LayerMask GroundMask { get { return groundMask; } }
    public float WalkingSpeed { get { return walkingSpeed; } }
    public float Gravity { get { return gravity; } set { gravity = value; } }
    public float VelocityY { get { return velocity.y; } set { velocity.y = value; } }
    public float JumpHeight { get { return jumpHeight; } }
    public float GroundDistance { get { return groundDistance; } }
    public float TurnSmoothTime { get { return turnSmoothTime; } }
    //public float TurnSmoothVelocity { get { return turnSmoothVelocity; } }
    public bool IsGrounded { get { return isGrounded; } set { isGrounded = value; } }
    public bool IsJumping { get { return isJumping; } set { isJumping = value; } }
    public bool IsMoving { get { return isMoving; } set { isMoving = value; } }
    public bool IsRunning { get { return isRunning; } set { isRunning = value; } }

    private void Awake()
    {
        states = new PlayerStateFactory(this);
        currentState = states.Grounded();
        currentState.EnterState();
    }

    // Start is called before the first frame update
    void Start()
    {
        walkingSpeed = speed;
        runningSpeed = speed * 1.5f;
        runBoostSpeed = speed * 2.2f;

        cameraController = Camera.main.GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentState);
        currentState.UpdateStates();
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        //Move(walkingSpeed);
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

            Vector3 moveDir = cameraController.PlanarRotation * direction;//Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
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
        if (Input.GetKeyUp(KeyCode.LeftShift) || !isGrounded)
        {
            isRunning = false;
        }
    }
}
