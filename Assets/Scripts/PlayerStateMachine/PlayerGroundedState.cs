using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
    : base(currentContext, playerStateFactory) 
    {
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState()
    {
        //Debug.Log("PlayerGroundedState EnterState");
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        //Debug.Log("PlayerGroundedState ExitState");
    }

    public override void CheckSwitchStates()
    {
        if (Input.GetButtonDown("Jump") && Ctx.IsGrounded)
        {
            SwitchState(Factory.Jump());
        }
        else if(Input.GetButtonDown("Vertical") || Input.GetButtonDown("Horizontal"))
        {
            SwitchState(Factory.Walk());
        }
    }

    public override void InitializeSubState()
    {
        if(!Ctx.IsMoving && !Ctx.IsRunning)
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
}
