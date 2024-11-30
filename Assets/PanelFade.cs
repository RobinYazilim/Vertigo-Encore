using UnityEngine;

public class PanelFade : MonoBehaviour
{
    public CanvasGroup panelGroup;

    public float fadeDuration = 1f;

    public void FadeIn()
    {
        StartCoroutine(Fade(0f, 1f));
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(1f, 0f));
    }

    private System.Collections.IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            panelGroup.alpha = newAlpha;
            yield return null;
        }

        panelGroup.alpha = endAlpha;
    }
}