using UnityEngine;

public class KnockbackTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            player.canKnockback = true;

            // 넉백 방향 전달
            if (player.transform.position.x < transform.position.x)
            {
                player.knockbackDir = -1;
            }

            if (player.transform.position.x > transform.position.x)
            {
                player.knockbackDir = 1;
            }
        }
    }

}
