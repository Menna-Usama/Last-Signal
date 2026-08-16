using System;
using System.Collections;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public static FadeController Instance;

    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration = 0.5f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void FadeOut(Action onComplete)
    {
        StartCoroutine(FadeRoutine(0f, 1f, onComplete));
    }

    public void FadeIn()
    {
        StartCoroutine(FadeRoutine(1f, 0f, null));
    }

    private IEnumerator FadeRoutine(float startAlpha, float endAlpha, Action onComplete)
    {
        Debug.Log($"FadeRoutine started: {startAlpha} -> {endAlpha}");
        fadeCanvasGroup.blocksRaycasts = true;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = endAlpha;
        fadeCanvasGroup.blocksRaycasts = (endAlpha > 0.5f);
        Debug.Log("FadeRoutine finished, final alpha: " + fadeCanvasGroup.alpha);

        onComplete?.Invoke();
    }
}