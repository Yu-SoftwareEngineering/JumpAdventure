using Unity.VisualScripting;
using UnityEngine;

public class Trap_FireButton : MonoBehaviour
{
    [SerializeField] Trap_FireTrap[] FireTraps;
    private Animator anim;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        FireTraps = GetComponentsInChildren<Trap_FireTrap>();
    }

    void OnTriggerEnter2D(Collider2D other){
        
        Player player = other.GetComponent<Player>();
        
        if(!player)
            return;
        
        bool positionCheck = player.transform.position.y >= this.transform.position.y + 0.5f;

        if(!positionCheck)
            return;

        anim.SetBool("BtnPush", true);
        foreach (Trap_FireTrap fireTrap in FireTraps){
            fireTrap.FireOff();
        }
        
    }
}
