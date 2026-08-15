using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance;
    [SerializeField] private SfxVfxHandler camRef;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject DeathMenuPanel;
    [SerializeField] private GameObject ControlsPanel;
    [SerializeField] private GameObject progressbar;
    [SerializeField] private GameObject cutscenePanel;
    [SerializeField] private VideoPlayer cutsceneVideoPlayer;
    public GameObject PauseMenuPanel;
    public GameObject WinPanel;

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

    private void OnEnable()
    {
        if (cutsceneVideoPlayer != null)
        {
            cutsceneVideoPlayer.loopPointReached += OnCutsceneFinished;
        }
    }

    private void OnDisable()
    {
        if (cutsceneVideoPlayer != null)
        {
            cutsceneVideoPlayer.loopPointReached -= OnCutsceneFinished;
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
        mainMenuPanel.SetActive(false);
        cutscenePanel.SetActive(true);
        camRef.CamAudioSource.clip = null;
        cutsceneVideoPlayer.Play();
        Debug.Log("Playing intro cutscene");
    }

    private void OnCutsceneFinished(VideoPlayer vp)
    {
        cutscenePanel.SetActive(false);
        progressbar.SetActive(true);
        onGameStart?.Invoke();
        Debug.Log("Cutscene finished, game started");
    }

    // Optional: let the player skip the cutscene
    public void SkipCutscene()
    {
        cutsceneVideoPlayer.Stop();
        OnCutsceneFinished(cutsceneVideoPlayer);
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
        PauseMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void LoadControlsPanel()
    {
        mainMenuPanel.SetActive(false);
        PauseMenuPanel.SetActive(false);
        ControlsPanel.SetActive(true);
    }

    public void BackButton()
    {
        ControlsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}