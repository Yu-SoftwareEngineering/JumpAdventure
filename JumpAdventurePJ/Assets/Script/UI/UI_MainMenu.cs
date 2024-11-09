using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UI_MainMenu : MonoBehaviour
{
    public string sceneName;

    [SerializeField] private GameObject[] uiElements;
    private UI_FadeEffect fadeEffect => GetComponentInChildren<UI_FadeEffect>();
    [SerializeField] private UI_Setting setting;

    private void Start()
    {
        fadeEffect.ScreenFade(0, 1.5f);
        loadSetting();
        AudioManager.instance.PlayBGM(5);
    }

    public void NewGame()
    {
        StartCoroutine(StartNewGame());
        AudioManager.instance.PlaySFX(10);
    }

    private IEnumerator StartNewGame()
    {
        fadeEffect.ScreenFade(1, 1.5f);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
        AudioManager.instance.PlayBGM(0);
    }

    public void SwitchUI(GameObject uiToEnable)
    {
        foreach (GameObject ui in uiElements)
        {
            // �ٸ� UI ��� ��Ȱ��ȭ
            ui.SetActive(false);
        }

        uiToEnable.SetActive(true);
        AudioManager.instance.PlaySFX(9);
    }

    private void loadSetting()
    {
        setting.sfxSlider.value = PlayerPrefs.GetFloat(setting.sfxParameter, 0.6f);
        setting.bgmSlider.value = PlayerPrefs.GetFloat(setting.bgmParameter, 0.6f);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                // ����Ƽ �����Ϳ��� ���� ���� ��
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                    // ����� ���ӿ��� ���� ���� ��
                    Application.Quit();
        #endif
    }
}
