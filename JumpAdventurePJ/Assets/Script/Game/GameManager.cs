using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;


    private void Awake()
    {
        // instance 객체가 두개이상 존재하지 않게 함.
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }


    private void Start()
    {
        
    }


}
