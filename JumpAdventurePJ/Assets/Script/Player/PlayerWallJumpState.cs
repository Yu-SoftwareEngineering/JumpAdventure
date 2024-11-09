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

        // PlayerState���� ��ӹ��� stateTimer[����ؼ� �����ϴ� Ÿ�̸�]
        stateTimer = 0.4f;

        // ���Խ� �ӵ� ����
        player.SetVelocity(player.moveSpeed * -player.facingDir, player.jumpForce * 0.5f);
    }

    public override void Update()
    {
        base.Update();

        // WallJumpState ������
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.airState);
        }

        // �ٴ� ������ idleState�� ��ȯ
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
