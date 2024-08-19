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

        // ����x && �������� ����(AirState) ���·� ���� ���
        if (player.isJumped == false) 
        {
            player.canFallJump = true;
        }
    }


    public override void Update()
    {
        base.Update();

        // ���߿��� �¿� �Է½� �̵�
        if (xInput != 0)
        {
            player.SetVelocity(player.moveSpeed * xInput * 0.8f, rb.velocity.y);
        }

        // �� ������ idleState�� ��ȯ
        if (player.IsGroundDetected())
        {
            player.stateMachine.ChangeState(player.idleState);
        }

        // ���� ����
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            FallJump();

            player.DoubleJump();

        }

        if (player.IsWallDetected()) //me
        {
            player.stateMachine.ChangeState(player.wallSlideState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        player.isJumped = false;
        player.canFallJump = false;
    }

    private void FallJump() 
    {
        if (player.canFallJump == true) 
        {
            player.SetVelocity(rb.velocity.x * 2, player.fallJumpForce);
            player.canFallJump = false;
        }
    }


}
