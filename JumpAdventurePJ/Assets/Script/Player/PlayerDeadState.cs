using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // HP 3 감소
        UI_InGame.instance.DamageToHp(3);

        // 0.9초 후 State 종료를 위해 설정
        stateTimer = 0.9f;

        // 물리적 상호작용 비활성화
        rb.isKinematic = true;
        player.SetZeroVelocity();
    }


    public override void Update()
    {
        base.Update();

        // Color.a 서서히 투명하게 바꾸는 함수
        SetTransparent();

        // 0.9초 후, 플레이어 오브젝트 Destroy
        if (stateTimer < 0)
        {
            player.DestroyPlayer();
        }
    }


    public override void Exit()
    {
        base.Exit();
    }

    // 캐릭터 투명도 설정 함수
    private void SetTransparent()
    {
        Color currentColor = sr.color;
        currentColor.a = Mathf.Lerp(currentColor.a, 0f, 2.3f * Time.deltaTime);
        sr.color = currentColor;
    }

}
