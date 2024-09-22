using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    private bool levelClear = false;

    private Animator anim => GetComponent<Animator>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null )
        {
            if(GameManager.instance.fruitsCollected >= GameManager.instance.totalFruits/2)
            {
                if (levelClear)
                {
                    return;
                }

                anim.SetTrigger("Active");
                AudioManager.instance.PlaySFX(2);
                Debug.Log(" Level is Finished ");
                GameManager.instance.LevelFinished();
            
                levelClear = true;
            }
        }

    }




}
