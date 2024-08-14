using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState currentState { get; private set; }

    // 초기 상태 설정
    public void Initialize(PlayerState _startState)
    {
        // 초기 상태 할당
        currentState = _startState;

        // 현재 State의 Enter() 실행
        currentState.Enter();
    }

    // 상태 변경
    public void ChangeState(PlayerState _newState)
    {
        // 현재 State의 Exit() 실행
        currentState.Exit();

        // 변경할 State 할당
        currentState = _newState;

        // 변경할 State의 Enter() 실행
        currentState.Enter();
    }

}
