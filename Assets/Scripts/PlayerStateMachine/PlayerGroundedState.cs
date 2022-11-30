using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
    : base(currentContext, playerStateFactory) { }
    public override void EnterState()
    {
        Debug.Log("PlayerGroundedState EnterState");
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        
    }

    public override void CheckSwitchStates()
    {
        if (Input.GetButtonDown("Jump"))
        {
            SwitchState(factory.Jump());
        }
    }

    public override void InitializeSubState()
    {
        
    }
}
