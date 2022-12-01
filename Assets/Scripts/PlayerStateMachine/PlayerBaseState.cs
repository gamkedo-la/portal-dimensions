using UnityEngine;

public abstract class PlayerBaseState
{
    private bool isRootState = false;
    private PlayerStateMachine ctx;
    private PlayerStateFactory factory;
    private PlayerBaseState currentSubState;
    private PlayerBaseState currentSuperState;

    protected bool IsRootState { set { isRootState = value; } }
    protected PlayerStateMachine Ctx {  get { return ctx; } }
    protected PlayerStateFactory Factory { get { return factory; } }

    public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    {
        ctx = currentContext;
        factory = playerStateFactory;
    }

    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();

    public abstract void CheckSwitchStates();

    public abstract void InitializeSubState();

    public void UpdateStates() 
    {
        UpdateState();
        if(currentSubState != null)
        {
            currentSubState.UpdateStates();
        }
    }

    protected void SwitchState(PlayerBaseState newState) 
    { 
        ExitState();

        newState.EnterState();

        if(isRootState)
            ctx.CurrentState = newState;
        else if(currentSuperState != null)
            currentSuperState.SetSubState(newState);
    }

    protected void SetSuperState(PlayerBaseState newSuperState) 
    {
        currentSuperState = newSuperState;
    }

    protected void SetSubState(PlayerBaseState newSubState) 
    {
        currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }

}
