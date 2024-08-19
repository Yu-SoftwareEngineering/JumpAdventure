using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetZeroVelocity();
    }

    public override void Update()
    {
        base.Update();

        // ����Ű �Է��� 0�� �ƴҽ�, moveState�� ��ȯ
        if (xInput != 0)
        {
            stateMachine.ChangeState(player.moveState);
        }

    }


    public override void Exit()
    {
        base.Exit();
    }


}
