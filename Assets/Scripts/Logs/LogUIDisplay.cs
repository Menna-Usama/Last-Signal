using UnityEngine;
using TMPro;
using System.Collections;
public class LogUIDisplay : MonoBehaviour
{

    [SerializeField]private TextMeshProUGUI logText;//write each manually
    [SerializeField]private CanvasGroup canvasGroup;
    [SerializeField]private float displyDuration = 3f;
    [SerializeField]private float fadeDuration = 1f;

    public void ShowLogText(string text)
    {
        StopAllCoroutines();
        StartCoroutine(FadeRoutine(text));
    }

    private IEnumerator FadeRoutine(string text)
    {
        logText.text = text;
        canvasGroup.alpha = 1f;


        yield return new WaitForSeconds(displyDuration);

        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            yield return null;  

        }
        canvasGroup.alpha = 0f;
    }
}
