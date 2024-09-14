using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Credits : MonoBehaviour
{
    [SerializeField] private RectTransform rect;
    [SerializeField] private float scrollSpeed;
    [SerializeField] private float offScreenPosition = 1150;

    private bool creditsSkipped;

    private void Update()
    {
        // Credits 오브젝트 끌어올리기
        rect.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        if (rect.anchoredPosition.y > offScreenPosition)
        {
            GoToMainMenu();
        }
    }

    // Credits 오브젝트 끌어올리는 속도 증가
    public void SkipCredits()
    {
        if (creditsSkipped == false)
        {
            scrollSpeed *= 5;
            creditsSkipped = true;
        }
        else
        {
            GoToMainMenu();
        }
    }

    // 메인 메뉴 이동 함수
    private void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }



}
