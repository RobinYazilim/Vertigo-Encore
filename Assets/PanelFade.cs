using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PanelFade : MonoBehaviour
{
    public float fadeDuration = 0.15f;
    private Image img;

    void Start()
    {

        img = GetComponent<Image>();
    }

    public Coroutine FadeIn()
    {
        return StartCoroutine(Fade(0f, 1f));
    }

    public Coroutine FadeOut()
    {
        return StartCoroutine(Fade(1f, 0f));
    }

    public IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            img.color = new Color(0, 0, 0, newAlpha);
            yield return null;
        }
        img.color = new Color(0, 0, 0, endAlpha);
    }
}