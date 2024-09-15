using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;

    [Header("Respawn info")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private float respawnDelay;

    [Header("Fruits info")]
    public int totalFruits;
    public int fruitsCollected;
    public bool fruitsRandomLook;

    [SerializeField] private CinemachineCamera cine;

    [Header("Level Management")]
    [SerializeField] private int currentLevelIndex;
    private int nexLevelIndex;


    private void Awake()
    {
        // instance ????? ?????? ???????? ??? ??.
        if (instance == null)
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
        CollectFruitsInfo();
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        nexLevelIndex = currentLevelIndex + 1;

    }

    public void RespawnPlayer() => StartCoroutine(RespawnLogic());

    private IEnumerator RespawnLogic()
    {
        yield return new WaitForSeconds(respawnDelay);

        GameObject newPlayer = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
        player = newPlayer.GetComponent<Player>();
        cine.Follow = player.transform;
    }

    // ?????? ??? ??????? ???
    public void UpdateRespawnPosition(Transform _newRespawnPoint)
    {
        respawnPoint = _newRespawnPoint;
    }

    #region Fruits

    // ???? ???? ???? ???
    public void AddFruit() => fruitsCollected++;

    // ???? ????
    public bool FruitsRandomLook() => fruitsRandomLook;

    // ????? ???? ?? ???? ??????
    private void CollectFruitsInfo()
    {
        Fruit[] allFruits = FindObjectsByType<Fruit>(FindObjectsSortMode.None);
        totalFruits = allFruits.Length;
    }

    #endregion


    #region Scene

    public IEnumerator MoveToEndScene()
    {
        yield return new WaitForSeconds(3f);

        if (SceneManager.GetActiveScene().name == "Level_1")
            SceneManager.LoadScene("TheEnd");
    }

    #endregion

    #region Level

    public void LevelFinished()
    {
        StartCoroutine(LoadNextScene());
    }

    private IEnumerator LoadNextScene()
    {

        UI_InGame.instance.fadeEffect.ScreenFade(1, 3f);
        yield return new WaitForSeconds(3f);

        // 마지막 레벨인지 확인하는 Bool
        bool noMoreLevels = nexLevelIndex == SceneManager.sceneCountInBuildSettings - 1;

        // TheEnd Scene 이동
        if (noMoreLevels)
        {
            SceneManager.LoadScene("TheEnd");
        }
        // 다음 Level 이동
        else
        {
            SceneManager.LoadScene("Level_" + nexLevelIndex);
        }
    }

    #endregion



}
