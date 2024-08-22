using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerState
{
    // Script ���
    protected Player player;
    protected PlayerStateMachine stateMachine;

    // ������Ʈ
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;

    // Setting
    protected float xInput;
    protected float yInput;
    protected string animBoolName;

    protected float stateTimer;
    protected bool triggerCalled;


    // ������
    public PlayerState(Player _player,PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }


    // ���� ���Խ� ����
    public virtual void Enter()
    {
        // �ִϸ��̼� �Ķ���� => true
        player.anim.SetBool(animBoolName, true);

        // ������Ʈ �Ҵ�
        rb = player.rb;
        sr = player.sr;

        // triggerCalled = false ���·� �ʱ�ȭ
        triggerCalled = false;
    }



    public virtual void Update()
    {
        // ����ؼ� �����ϴ� Ÿ�̸�
        stateTimer -= Time.deltaTime;

        // �����¿� �Է� ����
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        // ���� �Ķ���� �� ������Ʈ
        player.anim.SetFloat("yVelocity",rb.velocity.y);

        // �÷��̾� knockback ���� ��ȯ
        KnockBack();
    }



    // ���� ����� ����
    public virtual void Exit()
    {
        // �ִϸ��̼� �Ķ���� => false
        player.anim.SetBool(animBoolName, false);
    }

    // triggerCalled�� true�� ������ִ� �Լ�
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
