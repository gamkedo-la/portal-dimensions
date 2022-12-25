using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    private PlayerAttacking playerAttacking;
    public PlayerAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
       : base(currentContext, playerStateFactory) 
    {
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState()
    {
        //set attacking animation to false
        //Set player attack sound
        // Debug.Log("PlayerAttackState EnterState");
        playerAttacking = new PlayerAttacking();
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        //Debug.Log("PlayerRunState ExitState");
    }

    public override void CheckSwitchStates()
    {
        if(!playerAttacking.GetIsAttacking())
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
}
