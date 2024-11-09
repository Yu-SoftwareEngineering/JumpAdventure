using UnityEngine;

public class KnockbackTrigger : MonoBehaviour
{

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {

        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            player.canKnockback = true;

            // Hp ���� 
            UI_InGame.instance.DamageToHp(1);


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
