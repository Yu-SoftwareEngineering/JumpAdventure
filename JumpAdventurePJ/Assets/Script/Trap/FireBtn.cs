using Unity.VisualScripting;
using UnityEngine;

public class FireBtn : MonoBehaviour
{
    [SerializeField] FireTrap[] FireTraps;
    private Animator anim;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        FireTraps = GetComponentsInChildren<FireTrap>();
    }

    void OnTriggerEnter2D(Collider2D other){
        
        Player player = other.GetComponent<Player>();
        
        if(!player)
            return;
        
        bool positionCheck = player.transform.position.y >= this.transform.position.y + 0.5f;

        if(!positionCheck)
            return;

        anim.SetBool("BtnPush", true);
        foreach (FireTrap fireTrap in FireTraps){
            fireTrap.FireOff();
        }
        
    }
}
