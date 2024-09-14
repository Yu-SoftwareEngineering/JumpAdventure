using UnityEngine;

public class StunTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            player.canStun = true;
            player.canKnockback = true;

            // �˹� ���� ����
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
