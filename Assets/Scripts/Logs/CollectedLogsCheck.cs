using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CollectedLogsCheck : MonoBehaviour
{
    [SerializeField] private LogManager logManager;
    [SerializeField] private Text StillLogsLeft;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float displayDuration = 3f;
    [SerializeField] private float fadeDuration = 1f;

    private Collider2D col2D;

    void Start()
    {
        col2D = GetComponent<Collider2D>();
        col2D.isTrigger = false;
        canvasGroup.alpha = 0f; // start hidden instead of active text
    }

    private void OnEnable()
    {
        logManager.OnAllLogsCollected += SetTriggerOn;
    }

    private void OnDisable()
    {
        logManager.OnAllLogsCollected -= SetTriggerOn;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (logManager.LogsCollected != logManager.LogsRequired)
            {
                StillLogsLeft.text = "Still " + (logManager.LogsRequired - logManager.LogsCollected) + " Logs Left!";
                StopAllCoroutines();
                StartCoroutine(FadeRoutine());

                Debug.Log("Still " + (logManager.LogsRequired - logManager.LogsCollected) + " Logs Left");
            }
        }
    }

    private IEnumerator FadeRoutine()
    {
        canvasGroup.alpha = 1f;

        yield return new WaitForSeconds(displayDuration);

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 0f;
    }

    private void SetTriggerOn()
    {
        col2D.isTrigger = true;
    }
}