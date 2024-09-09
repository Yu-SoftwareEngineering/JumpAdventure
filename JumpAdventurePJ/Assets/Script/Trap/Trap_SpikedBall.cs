using UnityEngine;

public class Trap_SpikedBall : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float pushForce;
    private void Start() 
    {
        rb.AddForce( new Vector2(pushForce , 0), ForceMode2D.Impulse);
    }
}
