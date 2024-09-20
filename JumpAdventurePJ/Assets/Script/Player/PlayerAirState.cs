using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // 점프x && 떨어져서 공중(AirState) 상태로 왔을 경우
        if (player.isJumped == false)
        {
            player.canFallJump = true;
        }
    }


    public override void Update()
    {
        base.Update();

        // 공중에서 좌우 입력시 이동
        if (xInput != 0)
        {
            player.SetVelocity(player.moveSpeed * xInput * 0.8f, rb.velocity.y);
        }

        // 땅 감지시 idleState로 전환
        if (player.IsGroundDetected())
        {
            player.stateMachine.ChangeState(player.idleState);
        }

        // 더블 점프
        if (Input.GetKeyDown(KeyCode.Space) && !player.isRespawning)
        {
            // 땅에서 떨어져서 Air상태로 왔을 경우 1회 점프
            FallJump();

            player.DoubleJump();
        }

        // 벽 감지시 wallSlideState 상태로 전환
        if (player.IsWallDetected())
        {
            player.stateMachine.ChangeState(player.wallSlideState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        player.canDoubleJump = true;
        player.canFallJump = false;
        player.isJumped = false;
    }

    private void FallJump()
    {
        if (player.canFallJump == true)
        {
            AudioManager.instance.PlaySFX(4, true);
            player.SetVelocity(rb.velocity.x * 2, player.fallJumpForce);
            player.canFallJump = false;
        }
    }


}
