using UnityEngine;
using UnityEngine.SceneManagement;

public class Manage : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {

        if (SceneManager.GetActiveScene().buildIndex == 3 && GameObject.FindGameObjectsWithTag("Guardian").Length == 0)
        {
            GameSceneManager.Instance.WinPanel.SetActive(true);
        }


        if (SceneManager.GetActiveScene().buildIndex != 3 && GameObject.FindGameObjectsWithTag("Guardian").Length == 0)
        {
            GameSceneManager.Instance.LoadNextScene();
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameSceneManager.Instance.PauseMenuPanel.SetActive(true);
        }

    }
}
