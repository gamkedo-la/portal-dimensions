/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerStateManager : MonoBehaviour
{
    PlayerBaseState currentState;
    public PlayerAttackState AttackState = new PlayerAttackState();
    public PlayerPassiveState PassiveState = new PlayerPassiveState();

    // Start is called before the first frame update
    void Start()
    {
        currentState = PassiveState;

        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    public void SwitchState(PlayerBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
*/
