using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manage : MonoBehaviour
{
    private bool hasSpawnedInSeg2 = false;

    public bool HasSpawnedIn2 => hasSpawnedInSeg2;

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
    }

    void Update()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;

        bool noGuardiansLeft =
            GameObject.FindGameObjectsWithTag("Guardian").Length == 0;

        if (currentIndex == 2 && noGuardiansLeft)
        {
            GameSceneManager.Instance.PlayEndingCutscene();
        }

        if (currentIndex != 2 &&
            noGuardiansLeft &&
            !hasSpawnedInSeg2)
        {
            hasSpawnedInSeg2 = true;

            Debug.Log("NO GUARDIANS LEFT - LOADING NEXT SCENE");

            StartCoroutine(DelayBeforeSpawn());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameSceneManager.Instance.PauseMenuPanel.SetActive(true);
        }
    }

    IEnumerator DelayBeforeSpawn()
    {
        yield return new WaitForSeconds(1f);

        Debug.Log("CALLING LOAD NEXT SCENE");

        GameSceneManager.Instance.LoadNextScene();
    }
}