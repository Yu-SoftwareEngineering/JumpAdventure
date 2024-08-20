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

        // 바닥 감지 => idleState로 전환
        if (player.IsGroundDetected()) 
        {
            stateMachine.ChangeState(player.idleState);
        }

        // 벽 감지x => idleState로 전환 ( airState로 바로 변경됨 )
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
