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
        AudioManager.instance.PlaySFX(4,true);
        player.dustFx.Play();
        base.Enter();

        // PlayerState에서 상속받은 stateTimer[계속해서 감소하는 타이머]
        stateTimer = 0.4f;

        // 진입시 속도 설정
        player.SetVelocity(player.moveSpeed * -player.facingDir, player.jumpForce * 0.5f);
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
