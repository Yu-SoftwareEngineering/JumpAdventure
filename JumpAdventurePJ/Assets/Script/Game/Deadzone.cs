using UnityEngine;

public class Deadzone : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            // 플레이어 죽음
            player.Die();

            // 플레이어 리스폰
            GameManager.instance.RespawnPlayer();
        }
    }

}
