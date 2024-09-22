using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class UI_FadeEffect : MonoBehaviour
{
    [SerializeField] private Image fadeImage => GetComponent<Image>();

    public void ScreenFade(float targetAlpha, float duration)
    {
        StartCoroutine(Fade(targetAlpha, duration));
    }
    private IEnumerator Fade(float targetAlpha, float duration)
    {
        fadeImage.raycastTarget = true;

        float time = 0;
        Color currentColor = fadeImage.color;
        float startAlpha = currentColor.a;
        
        while (time < duration)
        {
            time += Time.deltaTime;

            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            fadeImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            yield return null;
        }
        fadeImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha);

        fadeImage.raycastTarget = false;
    }
}
