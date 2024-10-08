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

        // 넉백 상태 
        player.isKnocked = true;

        // 넉백 방향 | 힘 가하기
        rb.velocity = new Vector2(player.knockbackForce.x * player.knockbackDir, player.knockbackForce.y);
        
        // knocbackDuration ( = knockbackState 지속시간 )
        stateTimer = player.knockbackDuration;
    }

    public override void Update()
    {
        base.Update();

        // knockbackDuration 만큼 시간 경과시 상태 전환
        if (stateTimer < 0)
        {
            // canStun = true => stunnedState로 전환
            if(player.canStun)
            {
                stateMachine.ChangeState(player.stunnedState);
            }
            // canStun = false => idleState로 전환
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
