using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player _player, PlayerStateMachine _statemechine, string animBoolName) : base(_player, _statemechine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = .2f;
        player.SetVelocity(player.moveSpeed * -player.facingDir, player.jumpForce * 1.2f);
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (xInput != 0 && stateTimer < 0)
            rb.velocity = new Vector2(player.moveSpeed * xInput, rb.velocity.y);

        if (stateTimer < 0)
            stateMachine.ChangeState(player.airState);

        if (player.IsGroundedDetected())
            stateMachine.ChangeState(player.idleState);

    }
}
