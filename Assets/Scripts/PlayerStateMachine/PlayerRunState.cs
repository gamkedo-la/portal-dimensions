using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { }
    public override void EnterState()
    {
        //set walking animation to false
        //set running animation to true
        //set walking sound to false
        //set running sound to true
       // Debug.Log("PlayerRunState EnterState");
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
        
    }

    public override void InitializeSubState()
    {
        if (!Ctx.IsMoving)
        {
            SetSubState(Factory.Idle());
        }
        else if (Ctx.IsMoving && !Ctx.IsRunning)
        {
            SetSubState(Factory.Walk());
        }
    }
}
