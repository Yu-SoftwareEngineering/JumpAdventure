using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
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
    }


    public override void Exit()
    {
        base.Exit();
    }


}
