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

        // HP 3 ����
        UI_InGame.instance.DamageToHp(3);

        // 0.9�� �� State ���Ḧ ���� ����
        stateTimer = 0.9f;

        // ������ ��ȣ�ۿ� ��Ȱ��ȭ
        rb.isKinematic = true;
        player.SetZeroVelocity();
    }


    public override void Update()
    {
        base.Update();

        // Color.a ������ �����ϰ� �ٲٴ� �Լ�
        SetTransparent();

        // 0.9�� ��, �÷��̾� ������Ʈ Destroy
        if (stateTimer < 0)
        {
            player.DestroyPlayer();
        }
    }


    public override void Exit()
    {
        base.Exit();
    }

    // ĳ���� ���� ���� �Լ�
    private void SetTransparent()
    {
        Color currentColor = sr.color;
        currentColor.a = Mathf.Lerp(currentColor.a, 0f, 2.3f * Time.deltaTime);
        sr.color = currentColor;
    }

}
