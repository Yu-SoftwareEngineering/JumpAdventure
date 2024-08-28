using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // 점프 velocity 부여
        player.SetVelocity(0, player.jumpForce);

        player.canDoubleJump = true;

        player.isJumped = true;
        player.canFallJump = false;
    }


    public override void Update()
    {
        base.Update();

        // yVelocity < 0 (아래 방향 속도 = 떨어짐) => airState로 전환
        if (rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.airState);
        }

        // 올라가는 공중 상태 이동
        if (rb.velocity.y > 0)
        {
            player.SetVelocity(xInput * player.moveSpeed * 0.8f, rb.velocity.y);
        }

        // 더블 점프
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            player.DoubleJump();
        }
    }


    public override void Exit()
    {
        base.Exit();
    }


}
