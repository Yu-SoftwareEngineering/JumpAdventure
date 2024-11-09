using UnityEngine;

public class StunTrigger : KnockbackTrigger
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            player.canStun = true;

            // Hp °¨¼Ò 
            UI_InGame.instance.DamageToHp(2);
        }

        base.OnTriggerEnter2D(collision);
    }
}
