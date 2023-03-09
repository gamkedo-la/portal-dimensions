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

    //flying
    public float acceleration = 1f;
    public float maxAcceleration = 5f;
    public float flyingSpeed = 5f;
    public float maxHeight;
    public float minHeight;

    bool isGrounded;
    bool isRunning = false;
    bool isJumping = false;
    bool isMoving = false;
    bool isFlying = false;

    bool isRunBoost = false;

    float runBoostTimer = 0;
    float runBoostMaxTime;

    //Sound
    public string walkSound;

    private AudioManager audioManager;

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
    public float FlyingSpeed { get { return flyingSpeed; } }
    public float Acceleration { get { return acceleration; } }
    public float MaxAcceleration { get { return maxAcceleration; } }
    public float Gravity { get { return gravity; } set { gravity = value; } }
    public float VelocityY { get { return velocity.y; } set { velocity.y = value; } }
    public float JumpHeight { get { return jumpHeight; } }
    public float MaxHeight { get { return maxHeight; } }
    public float MinHeight { get { return minHeight; } }
    public float GroundDistance { get { return groundDistance; } }
    public float TurnSmoothTime { get { return turnSmoothTime; } }
    //public float TurnSmoothVelocity { get { return turnSmoothVelocity; } }
    public bool IsGrounded { get { return isGrounded; } set { isGrounded = value; } }
    public bool IsJumping { get { return isJumping; } set { isJumping = value; } }
    public bool IsMoving { get { return isMoving; } set { isMoving = value; } }
    public bool IsRunning { get { return isRunning; } set { isRunning = value; } }
    public bool IsFlying { get { return isFlying; } set { isFlying = value; } }
    public bool IsRunBoost { get { return isRunBoost; } set { IsRunBoost = value; } }

    //public bool IsAttacking { get { return isAttacking; } set { isAttacking = value; } }

    //private Animator anim;
    private Animator childAnim;
  

    private void Awake()
    {
        states = new PlayerStateFactory(this);
        currentState = states.Grounded();
        currentState.EnterState();
    }

    private void OnEnable()
    {
        Rocket.RocketCollected += SetIsFlying;
    }

    private void OnDisable()
    {
        Rocket.RocketCollected -= SetIsFlying;
    }

    // Start is called before the first frame update
    void Start()
    {
        childAnim = GetComponentInChildren<Animator>();

        walkingSpeed = speed;
        runningSpeed = speed * 2.0f;
        runBoostSpeed = speed * 3.0f;

        runBoostTimer = 0f;
        runBoostMaxTime = 5f;

        cameraController = Camera.main.GetComponent<CameraController>();

        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audio manager found in scene");
        }

        StopSound(walkSound);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateStates();
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);


        if (isMoving  == true)
        {
            childAnim.SetBool("isRunning", true);
        }
        else
        {
            childAnim.SetBool("isRunning", false);
        }
            //childAnim.SetBool("isRunning", true);
            //childAnim.SetBool("isRunning", false);
      

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        //Move(walkingSpeed);
        if (isRunBoost)
        {
            CheckRunBoostTimer();
        }
    }

    private void SetIsFlying()
    {
        isFlying = true;
    }

    public void SetIsRunning(bool running)
    {
        //Debug.Log("[PlayerStateMachine]: Running");
        isRunning = running;

       
        if(isRunning)
        {
          

            walkingSpeed = runningSpeed;
           
        }
        else
        {
            
            walkingSpeed = speed;
        }
    }    
    
    public void SetIsRunBoost (bool runBoost)
    {
        //Debug.Log("[PlayerStateMachine]: Running Boost");
        isRunBoost = runBoost;
        if(isRunBoost)
        {
            walkingSpeed = runBoostSpeed;
        }
        else
        {
            walkingSpeed = speed;
        }
    }

    private void CheckRunBoostTimer()
    {
        if (runBoostTimer >= runBoostMaxTime)
        {
            SetIsRunBoost(false);
            return;
        }

        runBoostTimer += Time.deltaTime;
    }
    
    public void PlaySound(string soundName)
    {
        audioManager.Play(soundName);
    }

    public void StopSound(string soundName)
    {
        audioManager.Stop(soundName);
    }
}
