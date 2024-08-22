using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        // 이동 설정
        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);

        // 상태 전환
        if (xInput == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }

    }


    public override void Exit()
    {
        base.Exit();
    }




}
