using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        Jump();
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
        ctx.VelocityY += ctx.Gravity * Time.deltaTime;
        ctx.Controller.Move(ctx.Velocity * Time.deltaTime);
    }

    public override void ExitState()
    {
        ctx.IsGrounded = Physics.CheckSphere(ctx.GroundCheck.position, ctx.GroundDistance, ctx.GroundMask);

        if (ctx.IsGrounded && ctx.VelocityY < 0)
        {
            ctx.VelocityY = -2f;
        }
    }

    public override void CheckSwitchStates()
    {
        if(ctx.Controller.isGrounded)
        {
            SwitchState(factory.Grounded());
        }
    }

    public override void InitializeSubState()
    {
        
    }

    private void Jump()
    {
        ctx.VelocityY = Mathf.Sqrt(ctx.JumpHeight * -2f * ctx.Gravity);


    }
}
