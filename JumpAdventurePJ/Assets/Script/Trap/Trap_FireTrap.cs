using System.Collections;
using UnityEngine;

public class Trap_FireTrap : KnockbackTrigger
{
    private Animator anim;
    public float fireOffTime = 3;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void FireOff(){
        StartCoroutine(FireControl());
    }

    IEnumerator FireControl()
    {
        // 불끄기
        GetComponent<CapsuleCollider2D>().enabled = false;
        anim.SetBool("FireOff", true);
        // 버튼 비활성화
        transform.parent.gameObject.GetComponent<BoxCollider2D>().enabled = false;

        yield return new WaitForSecondsRealtime(fireOffTime);
        
        // 불 다시 키기
        GetComponent<CapsuleCollider2D>().enabled = true;
        anim.SetBool("FireOff", false);
        // 버튼 다시 활성화
        transform.parent.gameObject.GetComponent<Animator>().SetBool("BtnPush", false);
        transform.parent.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}

