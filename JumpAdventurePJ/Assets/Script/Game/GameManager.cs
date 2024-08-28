using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;

    [Header("Respawn info")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private float respawnDelay;

    [SerializeField] private CinemachineCamera cine;

    [Header("Fruits info")]
    public bool fruitsRandomLook;
    public int fruitsCollected;

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

    // 과일 개수 증가 함수
    public void AddFruit() => fruitsCollected++;

    public bool FruitsRandomLook() => fruitsRandomLook;

    #endregion
}
