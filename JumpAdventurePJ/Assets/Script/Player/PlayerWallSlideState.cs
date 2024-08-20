using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {   
        base.Update();

        if (yInput >= 0) 
        {
            player.SetVelocity(0, rb.velocity.y * 0.95f);
        }
        
        else if (yInput < 0) 
        {
            player.SetVelocity(0, rb.velocity.y);
        }

        if (xInput != 0 && player.facingDir != xInput)
        {
            stateMachine.ChangeState(player.idleState);
        }

        // when player reaches the ground, change to idleState
        if (player.IsGroundDetected()) 
        {
            stateMachine.ChangeState(player.idleState);
        }

        // when player is not reaching the wall, change to idleState
        if (player.IsWallDetected() == false)
        {
            stateMachine.ChangeState(player.idleState);
        }

        // 벽 점프로 전환
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJumpState);
        }

    }
    public override void Exit()
    {
        base.Exit();
    }
}
