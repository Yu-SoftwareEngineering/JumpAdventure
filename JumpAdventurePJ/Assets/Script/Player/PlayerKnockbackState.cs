using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockbackState : PlayerState
{
    public PlayerKnockbackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        AudioManager.instance.PlaySFX(5);
        player.dustFx.Play();
        base.Enter();

        // �˹� ���� 
        player.isKnocked = true;

        // �˹� ���� | �� ���ϱ�
        rb.velocity = new Vector2(player.knockbackForce.x * player.knockbackDir, player.knockbackForce.y);
        
        // knocbackDuration ( = knockbackState ���ӽð� )
        stateTimer = player.knockbackDuration;
    }

    public override void Update()
    {
        base.Update();

        // knockbackDuration ��ŭ �ð� ����� ���� ��ȯ
        if (stateTimer < 0)
        {
            // canStun = true => stunnedState�� ��ȯ
            if(player.canStun)
            {
                stateMachine.ChangeState(player.stunnedState);
            }
            // canStun = false => idleState�� ��ȯ
            else
            {
                stateMachine.ChangeState(player.idleState);
            }
        }

    }

    public override void Exit()
    {
        base.Exit();

        player.SetZeroVelocity();

        if(player.canStun == false)
        {
            player.isKnocked = false;
        }

    }

}
