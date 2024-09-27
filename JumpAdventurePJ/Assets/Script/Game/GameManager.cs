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
    [SerializeField] public int hp;
    [SerializeField] public int totalHp;

    private void Awake()
    {
        //  instance가 한개만 존재하도록 하는 로직
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

        // HP 설정
        hp = totalHp;
        UI_InGame.instance.UpdateHpUI(totalHp);
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

    // 리스폰 위치 업데이트 함수
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

        // 다음 레벨 존재 여부 확인용 bool
        bool noMoreLevels = nexLevelIndex == SceneManager.sceneCountInBuildSettings - 1;

        // TheEnd Scene 이동
        if (noMoreLevels)
        {
            SceneManager.LoadScene("TheEnd");
            AudioManager.instance.PlayBGM(6);
        }
        // 인게임 Level 이동
        else
        {
            AudioManager.instance.PlayBGM(nexLevelIndex-1);
            SceneManager.LoadScene("Level_" + nexLevelIndex);
        }
    }

    #endregion

    // 클리어 시간 저장
    private void SaveBestTime()
    {
        float ClearTimeBefore = PlayerPrefs.GetFloat("Level_" + currentLevelIndex + "BestTime", 999);

        // 이전 회차 클리어타임보다 빠를시 
        if (levelTimer < ClearTimeBefore)
            PlayerPrefs.SetFloat("Level_" + currentLevelIndex + "BestTime", levelTimer);
    }


    // 과일 정보 저장
    private void SaveFruitsInfo()
    {
        // PlayerPrefs에 현재 레벨 맵의 총 과일수 저장
        PlayerPrefs.SetInt("Level_" + currentLevelIndex + "TotalFruits", totalFruits);

        // 이전 회차에서 획득한 과일수
        int fruitsCollectedBefore = PlayerPrefs.GetInt("Level_" + currentLevelIndex + "FruitsCollected");

        if (fruitsCollectedBefore < fruitsCollected)
        {
            PlayerPrefs.SetInt("Level_" + currentLevelIndex + "FruitsCollected", fruitsCollected);
        }

        // 은행에 수집한 과일 
        int FruitsInBankBefore = PlayerPrefs.GetInt("FruitsInBank");

        PlayerPrefs.SetInt("FruitsInBank", FruitsInBankBefore + fruitsCollected);
    }





}
