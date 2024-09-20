using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Transform checkPointPosition;

    private Animator anim => GetComponent<Animator>();
    private bool active;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (active)
        {
            return;
        }

        Player player = collision.GetComponent<Player>();

        if (player != null )
        {
            AudioManager.instance.PlaySFX(12);
            ActivateCheckPoint();
        }

    }


    private void ActivateCheckPoint()
    {
        active = true;
        anim.SetTrigger("Active");

        // 리스폰 위치 업데이트
        GameManager.instance.UpdateRespawnPosition(checkPointPosition);
    }





}
