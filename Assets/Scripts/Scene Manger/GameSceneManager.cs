using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject PauseMenuPanel;
    [SerializeField] private GameObject DeathMenuPanel;
    [SerializeField] private GameObject ControlsPanel;
    [SerializeField] private GameObject progressbar;


    public static event Action onGameStart; 


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
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
            
            Application.Quit();
#endif
    }

    public void StartGame()
    {
        SceneManager.GetActiveScene();
        mainMenuPanel.SetActive(false);
        progressbar.SetActive(true);
        onGameStart?.Invoke();
        Debug.Log("Pressed play");
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PauseMenuPanel.SetActive(false);
    }


    public void LoadNextScene()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextScene);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadControlsPanel()
    {
        mainMenuPanel.SetActive(false);
        PauseMenuPanel.SetActive(false) ;
        ControlsPanel.SetActive(true);
    }
    public void BackButton()
    {
        ControlsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

}
