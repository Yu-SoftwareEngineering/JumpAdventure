using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State
    public PlayerStateMachine stateMachine;

    #endregion

    #region Component
    public Rigidbody2D rb;
    public Animator anim;
    #endregion




    private void Awake()
    {
        // stateMachine 할당
        stateMachine = new PlayerStateMachine();

        // State 할당


        // 컴포넌트 할당
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }



    void Start()
    {
        // 초기 상태 설정 () 안에 idleState 넣고 수정 바람.
        // stateMachine.Initialize();
    }



    void Update()
    {
        stateMachine.currentState.Update();
    }


    public void AnimationTrigger() => stateMachine.currentState.AnimationTrigger();

}
