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

        // 상태 타이머 = 스턴 시간으로 설정
        stateTimer = player.stunDuration;
    }

    public override void Update()
    {
        base.Update();

        // 이동 불가 
        rb.velocity = new Vector2(0,0);

        // stunDruation이 지나면 스턴 종료
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
