using UnityEngine;

public class StartPoint : MonoBehaviour
{
    private Animator anim => GetComponent<Animator>();

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null )
        {
            anim.SetTrigger("Active");
        }
    }
}
