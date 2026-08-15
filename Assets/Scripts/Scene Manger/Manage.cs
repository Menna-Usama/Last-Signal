using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manage : MonoBehaviour
{
    private bool hasSpawnedInSeg2 = false;
    public bool HasSpawnedIn2 => hasSpawnedInSeg2;

    //private bool _isAdvancing = false; // prevents repeated triggering


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
        bool noGuardiansLeft = GameObject.FindGameObjectsWithTag("Guardian").Length == 0;

        if (currentIndex == 2 && noGuardiansLeft)
        {
            GameSceneManager.Instance.WinPanel.SetActive(true);
        }
        if (currentIndex != 2 && noGuardiansLeft && !hasSpawnedInSeg2)
        {
            //_isAdvancing = true;
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