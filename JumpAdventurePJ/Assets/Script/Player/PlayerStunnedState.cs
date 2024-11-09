using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStunnedState : PlayerState
{
    public PlayerStunnedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        AudioManager.instance.PlaySFX(7);
        base.Enter();

        // ���� Ÿ�̸� = ���� �ð����� ����
        stateTimer = player.stunDuration;
    }

    public override void Update()
    {
        base.Update();

        // �̵� �Ұ� 
        rb.velocity = new Vector2(0,0);

        // stunDruation�� ������ ���� ����
        if(stateTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }

    }

    public override void Exit()
    {
        base.Exit();

        player.isKnocked = false;
        player.canStun = false;
    }

}
