using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlyState : PlayerBaseState
{
    private const float FLYING_GRAVITY = 0f;
    private float originalGravity;
    private float yAxis = 0f;

    public PlayerFlyState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) 
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        //Debug.Log("[PlayerFlyState] EnterState(): Start the Fly");
        originalGravity = Ctx.Gravity;
        //Debug.Log("PlayerFlyState EnterState");
    }

    public override void UpdateState()
    {
        float currentHeight = this.Ctx.gameObject.transform.position.y;
        //Debug.Log(currentHeight);
        CheckSwitchStates();
        if(Input.GetKey(KeyCode.Q) && currentHeight < Ctx.MaxHeight)
            FlyUp();
        if (Input.GetKey(KeyCode.E) && currentHeight > Ctx.MinHeight)
            FlyDown();
        Move(Ctx.FlyingSpeed);
        yAxis = 0f;
    }

    public override void ExitState()
    {     
        //Debug.Log("PlayerFlyState ExitState");
    }

    public override void CheckSwitchStates()
    {
        if(!Ctx.IsFlying)
        {
            Ctx.Gravity = originalGravity;
            if (Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
            {
                SwitchState(Factory.Walk());
            }
            else
            {
                SwitchState(Factory.Grounded());
            }
                
        }
    }

    public override void InitializeSubState()
    {
        if (!Ctx.IsMoving && !Ctx.IsRunning)
        {
            SetSubState(Factory.Idle());
        }
        else if (Ctx.IsMoving && !Ctx.IsRunning)
        {
            SetSubState(Factory.Walk());
        }
        else
        {
            SetSubState(Factory.Run());
        }
    }

    private void FlyUp()
    {
        //Debug.Log("[PlayerFlyState] EnterState(): Flying");
        Ctx.VelocityY = 0;
        Ctx.Gravity = FLYING_GRAVITY;

        if(yAxis < Ctx.MaxAcceleration)
        {
            yAxis += Ctx.Acceleration;
        }
        else
        {
            yAxis = Ctx.MaxAcceleration;
        }
    }

    private void FlyDown()
    {
        //Debug.Log("[PlayerFlyState] EnterState(): Flying");
        Ctx.VelocityY = 0;
        Ctx.Gravity = FLYING_GRAVITY;

        if (yAxis > -(Ctx.MaxAcceleration))
        {
            yAxis -= Ctx.Acceleration;
        }
        else
        {
            yAxis = -(Ctx.MaxAcceleration);
        }
    }

    private void Move(float currentSpeed)
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, yAxis, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Ctx.Cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(Ctx.transform.eulerAngles.y, targetAngle, ref Ctx.turnSmoothVelocity, Ctx.TurnSmoothTime);
            Ctx.transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Ctx.CameraController.PlanarRotation * direction;//Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            Ctx.Controller.Move(moveDir.normalized * currentSpeed * Time.deltaTime);
        }
    }
}
