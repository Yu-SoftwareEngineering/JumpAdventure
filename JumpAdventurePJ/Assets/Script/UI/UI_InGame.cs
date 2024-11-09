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
    [SerializeField] private GameObject pauseUI;

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

    // �Ͻ����� ��� �Լ�
    public void Pause()
    {
        // ���� ���°� �ƴ� ��� -> ���� ���·� �����
        if (isPaused == false)
        {
            isPaused = true;
            Time.timeScale = 0;
            pauseUI.SetActive(true);
        }
        // ���� ������ ��� -> ���� ���� ����
        else
        {
            isPaused = false;
            Time.timeScale = 1;
            pauseUI.SetActive(false);
        }
    }

    // Pause_UI -> Main Menu ��ư 
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

}
