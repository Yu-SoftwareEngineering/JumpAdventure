using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State
    public PlayerStateMachine stateMachine;
    public PlayerIdleState idleState;
    public PlayerMoveState moveState;
    public PlayerJumpState jumpState;
    public PlayerAirState airState;
    public PlayerWallSlideState wallSlideState; 
    public PlayerWallJumpState wallJumpState;
    public PlayerKnockbackState knockbackState;
    public PlayerStunnedState stunnedState;
    public PlayerDeadState deadState;
    #endregion

    #region Component
    public Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer sr;
    #endregion

    [Header("Move info")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float jumpForce; 
    [SerializeField] public float doubleJumpForce;
    [SerializeField] public float fallJumpForce;
    [NonSerialized] public bool canDoubleJump;
    [NonSerialized] public bool canFallJump;
    [NonSerialized] public bool isJumped = false;

    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;

    [Header("Knockback & Stun info")]
    [SerializeField] public float knockbackDuration;
    [SerializeField] public Vector2 knockbackForce;
    [SerializeField] public bool canKnockback;
    [SerializeField] public bool isKnocked;
    [SerializeField] public float stunDuration;
    [SerializeField] public bool canStun;
    [NonSerialized] public float knockbackDir = -1;

    [Header("Respawn info")]
    [SerializeField] GameObject respawnEffectPrefab;
    [NonSerialized] public bool isRespawning;

    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;

    private void Awake()
    {
        // stateMachine
        stateMachine = new PlayerStateMachine();

        // State
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "JumpFall");
        airState = new PlayerAirState(this, stateMachine, "JumpFall");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "JumpFall");
        knockbackState = new PlayerKnockbackState(this, stateMachine, "KnockBack");
        stunnedState = new PlayerStunnedState(this, stateMachine, "Stun");
        deadState = new PlayerDeadState(this, stateMachine, "Dead");

        // Component
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }


    void Start()
    {
        // 초기 상태 = idleState
        stateMachine.Initialize(idleState);

        // 리스폰 효과 실행
        StartCoroutine(Respawn());
    }



    void Update()
    {
        stateMachine.currentState.Update();
    }


    public void AnimationTrigger() => stateMachine.currentState.AnimationTrigger();

    public void DoubleJump() 
    {
        if (canDoubleJump && IsGroundDetected() == false) 
        {
            SetVelocity(rb.velocity.x, doubleJumpForce);
            canDoubleJump = false;
        }
    }


    #region Setting Velocity Function
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if (isKnocked)
            return;
        rb.velocity = new Vector2(_xVelocity, _yVelocity);

        Flip(_xVelocity);
    }

    public void SetZeroVelocity()
    {
        if (isKnocked)
            return;
        rb.velocity = new Vector2(0, 0);
    }
    #endregion

    #region Flip
    public void FlipLogic()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void Flip(float _x)
    {
        if (_x > 0 && facingRight == false)
        {
            FlipLogic();
        }
        else if (_x < 0 && facingRight == true)
        {
            FlipLogic();
        }
    }
    #endregion

    #region Collision Check & Gizmos

    // 땅 감지 함수
    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    // 벽 감지 함수
    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    // 위 함수들의 Raycast를 유니티내에서 시각적으로 그리기
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(groundCheck.position,
            new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(wallCheck.position, 
            new Vector2(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y));
    }

    #endregion

    #region Dead
    public void Die() => stateMachine.ChangeState(deadState);
    public void DestroyPlayer() => Destroy(gameObject);

    #endregion

    #region Respawn

    private IEnumerator Respawn()
    {
        isRespawning = true;

        // RigidBody로 인한 이동x
        rb.bodyType = RigidbodyType2D.Static;
        // 이펙트 생성
        Instantiate(respawnEffectPrefab,
        new Vector2(transform.position.x - 0.1f, transform.position.y + 0.25f),
        Quaternion.identity);
        // 0.5초 딜레이
        yield return new WaitForSeconds(0.5f);
        // RIgidBody 정상화
        rb.bodyType = RigidbodyType2D.Dynamic;

        isRespawning = false;
    }

    #endregion
}
