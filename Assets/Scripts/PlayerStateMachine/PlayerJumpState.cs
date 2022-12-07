using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) 
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        Jump();
        //Debug.Log("PlayerJumpState EnterState");
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
        Move(Ctx.WalkingSpeed);
    }

    public override void ExitState()
    {     
        //Debug.Log("PlayerJumpState ExitState");
    }

    public override void CheckSwitchStates()
    {
        if(Ctx.Controller.isGrounded)
        {
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

    private void Jump()
    {
        Ctx.VelocityY = Mathf.Sqrt(Ctx.JumpHeight * -2f * Ctx.Gravity);
    }

    private void Move(float currentSpeed)
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

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
