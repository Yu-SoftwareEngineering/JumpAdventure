using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState currentState { get; private set; }

    // �ʱ� ���� ����
    public void Initialize(PlayerState _startState)
    {
        // �ʱ� ���� �Ҵ�
        currentState = _startState;

        // ���� State�� Enter() ����
        currentState.Enter();
    }

    // ���� ����
    public void ChangeState(PlayerState _newState)
    {
        // ���� State�� Exit() ����
        currentState.Exit();

        // ������ State �Ҵ�
        currentState = _newState;

        // ������ State�� Enter() ����
        currentState.Enter();
    }

}
