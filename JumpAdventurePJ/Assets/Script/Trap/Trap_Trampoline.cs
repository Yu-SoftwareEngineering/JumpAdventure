using System.Collections;
using UnityEngine;

public class Trap_Trampoline : MonoBehaviour
{
    [SerializeField] private float pushPower;
    [SerializeField] private float pushDuration;
    [SerializeField] private float pushCooldown;
    private float lastPushTime = -Mathf.Infinity;

    private Player player;
    private Animator anim;
    private Rigidbody2D rb;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if (player != null)
        {
            rb = player.rb;
        }

        if (rb != null)
        {
            if (lastPushTime + pushCooldown > Time.time)
            {
                return;
            }

            lastPushTime = Time.time;
            AudioManager.instance.PlaySFX(8 , true);
            StartCoroutine(Push());
        }
    }

    private IEnumerator Push()
    {
        player.isPusing = true;

        anim.SetTrigger("Active");
        rb.velocity = Vector2.zero;
        rb.AddForce(transform.up * pushPower, ForceMode2D.Impulse);

        yield return new WaitForSeconds(pushDuration);

        player.isPusing = false;
    }
}
