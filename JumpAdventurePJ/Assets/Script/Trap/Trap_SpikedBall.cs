using NUnit.Framework.Constraints;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Trap_SpikedBall : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float pushForce;
    [SerializeField] private float fixForce;

    private void Start() 
    {
        fixForce = Mathf.Abs(pushForce / 2000);
        Debug.Log(fixForce);
        rb.AddForce( new Vector2(pushForce , 0), ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        if(rb.velocity.x <0)
        {
            int direction = -1;
            rb.AddForce(new Vector2(fixForce * direction,0), ForceMode2D.Impulse);
        }
        else
        {
            int direction = 1;
            rb.AddForce(new Vector2(fixForce * direction, 0), ForceMode2D.Impulse);
        }
    }
}
