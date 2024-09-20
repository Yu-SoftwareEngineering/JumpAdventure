using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UI_MainMenu : MonoBehaviour
{
    public string sceneName;

    [SerializeField] private GameObject[] uiElements;
    private UI_FadeEffect fadeEffect => GetComponentInChildren<UI_FadeEffect>();


    private void Start()
    {
        fadeEffect.ScreenFade(0, 1.5f);
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
    }

    public void SwitchUI(GameObject uiToEnable)
    {
        foreach (GameObject ui in uiElements)
        {
            // 다른 UI 모두 비활성화
            ui.SetActive(false);
        }

        uiToEnable.SetActive(true);
        AudioManager.instance.PlaySFX(9); 
    }
}
