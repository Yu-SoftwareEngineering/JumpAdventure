using UnityEngine;

public enum FruitType { Apple, Banana, Cherry, kiwi, Melon, Orange, Pineapple, Strawberry }

public class Fruits : MonoBehaviour
{
    [SerializeField] private FruitType fruitType;

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
            Destroy(gameObject);
        }

    }


    private void SetRandomLook()
    {
        if (gameManager.FruitsRandomLook() == false)
        {
            // ���� Ÿ�� ���� Ÿ�� ����
            UpdateFruitVisuals();
            return;
        }
        else
        {
            // ���� Ÿ�� ���� ����
            int randomIndex = Random.Range(0, 8);

            anim.SetFloat("FruitIndex", randomIndex);
        }
    }

    // ���� ����� �����ؼ� Visual Update
    private void UpdateFruitVisuals() => anim.SetFloat("FruitIndex", (int)fruitType);

}