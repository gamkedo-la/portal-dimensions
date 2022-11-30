/*
using UnityEngine;

public class PlayerPassiveState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        //background music starts
        //animation of tail wags
        Debug.Log("PlayerPassiveState EnterState");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        //if player is walking, animate legs, tail, and ears
        //check if player hits bark or charge button, then switch states to attack
        if(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.LeftShift))
        {
            player.SwitchState(player.AttackState);
        }

        Debug.Log("PlayerPassiveState UpdateState");
    }

    public override void OnCollisionEnter(PlayerStateManager player, Collision collision)
    {
        //Check for collision tag
            //if item, collect it
            //if portal, then go through
            //if enemy radius, switch to AttackState
        Debug.Log("PlayerPassiveState OnCollisionEnter");
    }
}
*/
