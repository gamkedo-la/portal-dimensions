using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { }
    public override void EnterState()
    {
        //set walking animation to false
        //set running animation to false
        //set walking sound to false
        //set running sound to false
        //Debug.Log("PlayerIdleState EnterState");
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        //Debug.Log("PlayerIdleState ExitState");
    }

    public override void CheckSwitchStates()
    {
        if (Input.GetButtonDown("Jump") && Ctx.IsGrounded)
        {
            SwitchState(Factory.Jump());
        }
        else if (Input.GetButtonDown("Vertical") || Input.GetButtonDown("Horizontal"))
        {
            SwitchState(Factory.Walk());
        }
        else
        {
            SwitchState(Factory.Idle());
        }
    }

    public override void InitializeSubState()
    {
        if (Ctx.IsMoving && Ctx.IsRunning)
        {
            SetSubState(Factory.Run());
        }
        else if (Ctx.IsMoving)
        {
            SetSubState(Factory.Walk());
        }
    }
}
