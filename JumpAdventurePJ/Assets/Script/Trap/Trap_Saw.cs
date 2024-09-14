using UnityEngine;

public class Trap_Saw : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform[] wayPoint;
    private int wayPointIndex = 1;
    private bool moveForward = true;
    Animator anim => GetComponent<Animator>();

    void Start()
    {
        anim.SetBool("Active", true);
        transform.position = wayPoint[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,
            wayPoint[wayPointIndex].position, moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, wayPoint[wayPointIndex].position) < 0.1f)
        {
            if (moveForward == true)
            {
                wayPointIndex++;
                if (wayPointIndex == wayPoint.Length - 1)
                {
                    moveForward = false;
                    return;
                }
            }
            if (moveForward == false)
            {
                wayPointIndex--;
                if (wayPointIndex == 0)
                {
                    moveForward = true;
                }
            }

        }
    }
}

