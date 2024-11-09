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
    [SerializeField] private float levelTimer;

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

    private void Update()
    {
        levelTimer += Time.deltaTime;
        UI_InGame.instance.UpdateTimerUI(levelTimer);
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
    public void AddFruit()
    {
        fruitsCollected++;
        UI_InGame.instance.UpdateFruitUI(fruitsCollected, totalFruits);
    }

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

        PlayerPrefs.SetInt("Level_" + currentLevelIndex + "Unlocked", 1);

        SaveBestTime();
        SaveFruitsInfo();
        StartCoroutine(LoadNextScene());
    }

    private IEnumerator LoadNextScene()
    {

        UI_InGame.instance.fadeEffect.ScreenFade(1, 3f);
        yield return new WaitForSeconds(3f);

        // ?????? ???????? ?????? Bool
        bool noMoreLevels = nexLevelIndex == SceneManager.sceneCountInBuildSettings - 1;

        // TheEnd Scene ???
        if (noMoreLevels)
        {
            SceneManager.LoadScene("TheEnd");
            AudioManager.instance.PlayBGM(6);
        }
        // ???? Level ???
        else
        {
            AudioManager.instance.PlayBGM(nexLevelIndex-1);
            SceneManager.LoadScene("Level_" + nexLevelIndex);
        }
    }

    #endregion

    // Ŭ���� �ð� ����
    private void SaveBestTime()
    {
        float ClearTimeBefore = PlayerPrefs.GetFloat("Level_" + currentLevelIndex + "BestTime", 999);

        // ���� ȸ�� Ŭ����Ÿ�Ӻ��� ������ 
        if (levelTimer < ClearTimeBefore)
            PlayerPrefs.SetFloat("Level_" + currentLevelIndex + "BestTime", levelTimer);
    }


    // ���� ���� ����
    private void SaveFruitsInfo()
    {
        // PlayerPrefs�� ���� ���� ���� �� ���ϼ� ����
        PlayerPrefs.SetInt("Level_" + currentLevelIndex + "TotalFruits", totalFruits);

        // ���� ȸ������ ȹ���� ���ϼ�
        int fruitsCollectedBefore = PlayerPrefs.GetInt("Level_" + currentLevelIndex + "FruitsCollected");

        if (fruitsCollectedBefore < fruitsCollected)
        {
            PlayerPrefs.SetInt("Level_" + currentLevelIndex + "FruitsCollected", fruitsCollected);
        }

        // ���࿡ ������ ���� 
        int FruitsInBankBefore = PlayerPrefs.GetInt("FruitsInBank");

        PlayerPrefs.SetInt("FruitsInBank", FruitsInBankBefore + fruitsCollected);
    }





}
