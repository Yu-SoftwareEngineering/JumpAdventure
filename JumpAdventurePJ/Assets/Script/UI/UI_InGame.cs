using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class UI_InGame : MonoBehaviour
{
    public static UI_InGame instance;
    public UI_FadeEffect fadeEffect;
    private bool isPaused = false;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI fruitText;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private GameObject pauseUI;
    private bool canReduceHP = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(instance);
        }
        
        fadeEffect = GetComponentInChildren<UI_FadeEffect>();
    }

    private void Start()
    {
        fadeEffect.ScreenFade(0, 1f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    public void UpdateFruitUI (int _collectedFruits , int _totalFruits)
    {
	    fruitText.text = _collectedFruits + "/" + _totalFruits;
    }

    public void UpdateTimerUI (float timer)
    {
	    timerText.text = timer.ToString("00") + " s";
    }

    // 일시정지 기능 함수
    public void Pause()
    {
        // 정지 상태가 아닐 경우 -> 정지 상태로 만들기
        if (isPaused == false)
        {
            isPaused = true;
            Time.timeScale = 0;
            pauseUI.SetActive(true);
        }
        // 정지 상태인 경우 -> 정지 상태 해제
        else
        {
            isPaused = false;
            Time.timeScale = 1;
            pauseUI.SetActive(false);
        }
    }

    // Pause_UI -> Main Menu 버튼 
    public void GoToMainMenu()
    {
        StartCoroutine(GoToMainMenuLogic());
    }

    private IEnumerator GoToMainMenuLogic()
    {
        Pause();
        fadeEffect.ScreenFade(1, 1f);
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("MainMenu");
    }

    #region Hp 

    public void UpdateHpUI(int hp)
    {
        hpText.text = hp + "/" + GameManager.instance.totalHp;
    }

    public void DamageToHp(int damage) => StartCoroutine(DamageToHpLogic(damage));


    public IEnumerator DamageToHpLogic(int damage)
    {
        if(canReduceHP)
        {
            GameManager.instance.hp -= damage;
        }
        canReduceHP = false;

        UpdateHpUI(GameManager.instance.hp);

        if (GameManager.instance.hp <= 0)
        {
            Pause();
            GoToMainMenu();
        }

        yield return new WaitForSeconds(0.4f);
        canReduceHP=true;
    }

    #endregion


}
