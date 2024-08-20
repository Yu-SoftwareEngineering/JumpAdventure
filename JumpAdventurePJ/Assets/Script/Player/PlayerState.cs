using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerState
{
    // Script 등록
    protected Player player;
    protected PlayerStateMachine stateMachine;

    // 컴포넌트
    protected Rigidbody2D rb;

    // Setting
    protected float xInput;
    protected float yInput;
    protected string animBoolName;

    protected float stateTimer;
    protected bool triggerCalled;


    // 생성자
    public PlayerState(Player _player,PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }


    // 상태 진입시 실행
    public virtual void Enter()
    {
        // 애니메이션 파라미터 => true
        player.anim.SetBool(animBoolName, true);

        // 컴포넌트 할당
        rb = player.rb;

        // triggerCalled = false 상태로 초기화
        triggerCalled = false;
    }



    public virtual void Update()
    {
        // 계속해서 감소하는 타이머
        stateTimer -= Time.deltaTime;

        // 상하좌우 입력 감지
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        // 점프 파라미터 값 업데이트
        player.anim.SetFloat("yVelocity",rb.velocity.y);

        // 플레이어 knockback 상태 전환
        KnockBack();
    }



    // 상태 종료시 실행
    public virtual void Exit()
    {
        // 애니메이션 파라미터 => false
        player.anim.SetBool(animBoolName, false);
    }

    // triggerCalled를 true로 만들어주는 함수
    public void AnimationTrigger() => triggerCalled = true;

    #region KnockBack
    private void KnockBack()
    {
        if (player.canKnockback)
        {
            if(player.stateMachine.currentState == player.knockbackState)
            {
                player.canKnockback = false;
                return;
            }
            stateMachine.ChangeState(player.knockbackState);
            player.canKnockback = false;
        }
    }
    #endregion
}
