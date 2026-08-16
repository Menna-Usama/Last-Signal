using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class LogUIDisplay : MonoBehaviour
{
    [SerializeField] private LogManager logManager;

    [SerializeField] private Text logText; // switched to legacy text to add font
    [SerializeField] private Text collectedLogs;


    //[SerializeField]private TextMeshProUGUI logText;//write each manually
    [SerializeField]private CanvasGroup canvasGroup;
    [SerializeField]private float displyDuration = 4f;
    [SerializeField]private float fadeDuration = 1f;
   
    public void ShowLogText(string text)
    {
        StopAllCoroutines();
        StartCoroutine(FadeRoutine(text));
    }
    public void ShowCollectedLogsText()
    {
        collectedLogs.text = ("Logs Collected : " +logManager.LogsCollected.ToString() + "/" + logManager.LogsRequired);
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
