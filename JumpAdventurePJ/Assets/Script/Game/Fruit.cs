using UnityEngine;

public enum FruitType { Apple, Banana, Cherry, kiwi, Melon, Orange, Pineapple, Strawberry }

public class Fruit : MonoBehaviour
{
    [SerializeField] private FruitType fruitType;
    [SerializeField] private GameObject pickUpFX;

    private GameManager gameManager;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        gameManager = GameManager.instance;
        SetRandomLook();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            gameManager.AddFruit();
            GameObject newFX = Instantiate(pickUpFX,transform.position,Quaternion.identity);
            AudioManager.instance.PlaySFX(3 , true);
            Destroy(gameObject);
        }

    }


    private void SetRandomLook()
    {
        if (gameManager.FruitsRandomLook() == false)
        {
            // 과일 타입 직접 타입 선택
            UpdateFruitVisuals();
            return;
        }
        else
        {
            // 과일 타입 랜덤 선택
            int randomIndex = Random.Range(0, 8);

            anim.SetFloat("FruitIndex", randomIndex);
        }
    }

    // 과일 목록중 선택해서 Visual Update
    private void UpdateFruitVisuals() => anim.SetFloat("FruitIndex", (int)fruitType);

}
