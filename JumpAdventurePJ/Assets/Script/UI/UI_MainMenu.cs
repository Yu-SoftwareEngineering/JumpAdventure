using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UI_MainMenu : MonoBehaviour
{
    public string sceneName;

    private UI_FadeEffect fadeEffect => GetComponentInChildren<UI_FadeEffect>();


    private void Start()
    {
        fadeEffect.ScreenFade(0, 1.5f);
    }

    public void NewGame()
    {
        StartCoroutine(StartNewGame());
    }

    private IEnumerator StartNewGame()
    {
        fadeEffect.ScreenFade(1, 1.5f);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }

}
