using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manage : MonoBehaviour
{
    private bool hasSpawnedInSeg2 = false;
    public bool HasSpawnedIn2 => hasSpawnedInSeg2;

    private bool _isAdvancing = false; // prevents repeated triggering

    void Update()
    {

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        bool noGuardiansLeft = GameObject.FindGameObjectsWithTag("Guardian").Length == 0;

        if (currentIndex == 2 && noGuardiansLeft)
        {
            GameSceneManager.Instance.WinPanel.SetActive(true);
        }
        else if (currentIndex != 2 && noGuardiansLeft && !_isAdvancing)
        {
            _isAdvancing = true;
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