using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manage : MonoBehaviour
{
    private bool hasSpawnedInSeg2 = false;
    public bool HasSpawnedIn2 => hasSpawnedInSeg2;

    private bool hasTriggeredEnding = false; // NEW guard

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        hasSpawnedInSeg2 = false;
        hasTriggeredEnding = false; // reset on scene load 
    }

    void Update()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        bool noGuardiansLeft = GameObject.FindGameObjectsWithTag("Guardian").Length == 0;

        if (currentIndex == 2 && noGuardiansLeft && !hasTriggeredEnding)
        {
            hasTriggeredEnding = true;
            GameSceneManager.Instance.PlayEndingCutscene();
        }
        if (currentIndex != 2 && noGuardiansLeft && !hasSpawnedInSeg2)
        {
            hasSpawnedInSeg2 = true;
            StartCoroutine(DelayBeforeSpawn());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameSceneManager.Instance.PauseMenuPanel.SetActive(true);
        }
    }

    IEnumerator DelayBeforeSpawn()
    {
        yield return new WaitForSeconds(1);
        GameSceneManager.Instance.LoadNextScene();
    }
}