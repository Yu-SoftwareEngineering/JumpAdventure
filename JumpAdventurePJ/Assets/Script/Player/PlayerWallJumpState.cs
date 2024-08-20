using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // PlayerState에서 상속받은 stateTimer[계속해서 감소하는 타이머]
        stateTimer = 0.4f; // 어떤 상태가 종료되는지? 

        // 진입시 속도 설정
        player.SetVelocity(player.moveSpeed * -player.facingDir, player.jumpForce * 0.5f); // 벽 반대로 점프한다는뜻?
    }

    public override void Update()
    {
        base.Update();

        // WallJumpState 나가기
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.airState);
        }

        // 바닥 감지시 idleState로 전환
        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    
}
