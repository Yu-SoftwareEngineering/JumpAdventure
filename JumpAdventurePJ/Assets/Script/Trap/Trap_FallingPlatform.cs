using System.Collections;
using UnityEditor;
using UnityEngine;

public class Trap_FallingPlatform : MonoBehaviour
{
    [Header("Repeat info")]
    [SerializeField] private float repeatDistance;
    [SerializeField] private float repeatSpeed;
    [SerializeField] private bool repeatHorizontal = false;
    [SerializeField] private bool repeatVertical = false;
    [SerializeField] private Vector3[] wayPoints;
    private bool canRepeatMove = true;
    private int wayPointIndex = 0;

    [Header("GroundCheck info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;

    private bool delete = false;

    Rigidbody2D rb => GetComponent<Rigidbody2D>();
    SpriteRenderer sr => GetComponent<SpriteRenderer>();


    void Start()
    {
        SetupWayPoint();
    }

    void Update()
    {
        RepeatMove();

        if (delete)
        {
            Color currentColor = sr.color;
            currentColor.a = Mathf.Lerp(currentColor.a, 0f, 0.7f * Time.deltaTime);
            sr.color = currentColor;
        }
    }


    // 반복이동 WayPoint
    private void SetupWayPoint()
    {
        wayPoints = new Vector3[2];
        float offset = repeatDistance / 2;

        // 좌우 이동
        if (repeatHorizontal)
        {
            wayPoints[0] = transform.position + new Vector3(offset, 0, 0);
            wayPoints[1] = transform.position + new Vector3(-offset, 0, 0);
        }
        // 상하 이동
        else if (repeatVertical)
        {
            wayPoints[0] = transform.position + new Vector3(0, offset, 0);
            wayPoints[1] = transform.position + new Vector3(0, -offset, 0);
        }
    }

    // 반복이동 MoveLogic
    private void RepeatMove()
    {
        if (!canRepeatMove)
            return;

        transform.position = Vector2.MoveTowards
            (transform.position, wayPoints[wayPointIndex], repeatSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, wayPoints[wayPointIndex]) < 0.1f)
        {
            wayPointIndex++;

            if (wayPointIndex >= wayPoints.Length)
            {
                wayPointIndex = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            StepOn_Impact();
            StartCoroutine(Falling());
        }
    }

    // 처음 밟았을때 아래로 살짝 밀려나는 효과; 
    private void StepOn_Impact()
    {
        canRepeatMove = false;
        transform.position = Vector2.MoveTowards
            (transform.position, transform.position + (Vector3.down * 20), 3 * Time.deltaTime);
    }

    // 플랫폼 하강 
    private IEnumerator Falling()
    {
        float randomSeconds = Random.Range(0.0f, 2.0f);
        yield return new WaitForSeconds(randomSeconds);
        // Collider 컴포넌트를 가져옵니다.
        Collider2D[] colliders = GetComponents<BoxCollider2D>();

        foreach ( Collider2D collider in colliders)
        {
            collider.isTrigger = true;
        }

        rb.isKinematic = false;
        rb.gravityScale = 1.5f;
        rb.drag = 0.5f;

        yield return new WaitForSeconds(2.5f);
        StartCoroutine(StartDestroy());
    }

    // Destroy 로직 함수
    private IEnumerator StartDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        delete = true;
        rb.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(1f);
        delete = false;
        Destroy(gameObject);
    }
}
