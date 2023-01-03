using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }

    //float turnSmoothVelocity;
    public override void EnterState()
    {
        //set walking animation to true
        //set running animation to false
        //set walking sound to true
        //set running sound to false
        //Debug.Log("PlayerWalkState EnterState");
    }

    public override void UpdateState()
    {
        //Debug.Log("UpdateState PlayerWalkState");
        Move(Ctx.WalkingSpeed);
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        //Debug.Log("PlayerWalkState ExitState");
    }

    public override void CheckSwitchStates()
    {
        if (Input.GetButtonDown("Jump") && Ctx.IsGrounded)
        {
            SwitchState(Factory.Jump());
        }
        if((Input.GetButtonUp("Vertical") || Input.GetButtonUp("Horizontal")) && (!Input.GetButton("Vertical") && !Input.GetButton("Horizontal")))
        {
            SwitchState(Factory.Grounded());
        }
    }

    public override void InitializeSubState()
    {
        if (!Ctx.IsMoving)
        {
            SetSubState(Factory.Idle());
        }
        else if (Ctx.IsMoving && Ctx.IsRunning)
        {
            SetSubState(Factory.Run());
        }
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
            //Debug.Log(currentSpeed);
        }
    }
}
