    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }


    public override void Update()
    {
        base.Update();

        // Space �Է� => jumpState ��ȯ
        if(Input.GetKeyDown(KeyCode.Space) && !player.isRespawning)
        {
            stateMachine.ChangeState(player.jumpState);
        }

        // �� ����x => airState ��ȯ
        if (player.IsGroundDetected() == false)
        {
            stateMachine.ChangeState(player.airState);
        }
        
    }


    public override void Exit()
    {
        base.Exit();
    }


}
