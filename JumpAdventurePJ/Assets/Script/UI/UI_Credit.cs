using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UI_Credits : MonoBehaviour
{
    [SerializeField] private RectTransform rect;
    [SerializeField] private float scrollSpeed;
    [SerializeField] private float offScreenPosition = 1150;

    private bool creditsSkipped;

    private UI_FadeEffect fadeEffect => GetComponentInChildren<UI_FadeEffect>();

    private void Awake()
    {
        fadeEffect.ScreenFade(0, 2f);
    }

    private void Update()
    {
        // Credits ������Ʈ ����ø���
        rect.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        if (rect.anchoredPosition.y > offScreenPosition)
        {
            StartCoroutine(GoToMainMenu());
        }
    }

    // Credits ������Ʈ ����ø��� �ӵ� ����
    public void SkipCredits()
    {
        if (creditsSkipped == false)
        {
            scrollSpeed *= 5;
            creditsSkipped = true;
        }
        else
        {
            fadeEffect.ScreenFade(1, 1f);
            SceneManager.LoadScene("MainMenu");
        }
    }

    // ���� �޴� �̵� �Լ�
    private IEnumerator GoToMainMenu()
    {
        fadeEffect.ScreenFade(1, 2f);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainMenu");
    }



}
