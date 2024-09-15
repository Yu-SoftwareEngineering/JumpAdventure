using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    private Animator anim => GetComponent<Animator>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null )
        {
            if(GameManager.instance.fruitsCollected >= GameManager.instance.totalFruits/2)
            {
                anim.SetTrigger("Active");
                Debug.Log(" Level is Finished ");
                GameManager.instance.LevelFinished();

                // Ŭ����� ���� �̵� 
                StartCoroutine(GameManager.instance.MoveToEndScene());
            }
        }

    }




}
