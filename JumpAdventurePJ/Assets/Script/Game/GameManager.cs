using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;

    [Header("Respawn info")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform respwanPoint;
    [SerializeField] private float respawnDelay;


    [SerializeField] private CinemachineCamera cine;

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

        GameObject newPlayer = Instantiate(playerPrefab, respwanPoint.position, Quaternion.identity);
        player = newPlayer.GetComponent<Player>();
        cine.Follow = player.transform;
    }




}
