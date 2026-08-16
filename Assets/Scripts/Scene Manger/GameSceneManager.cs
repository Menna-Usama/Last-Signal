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

    [SerializeField] private GameObject endingCutscenePanel;
    [SerializeField] private VideoPlayer endingVideoPlayer;

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
            cutsceneVideoPlayer.loopPointReached += OnCutsceneFinished;
        



        SceneManager.sceneLoaded += OnAnySceneLoaded;
    }

    private void OnDisable()
    {
        if (cutsceneVideoPlayer != null)
            cutsceneVideoPlayer.loopPointReached -= OnCutsceneFinished;

    

        SceneManager.sceneLoaded -= OnAnySceneLoaded;
    }

    private void OnAnySceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnAnySceneLoaded fired, calling FadeIn");
        FadeController.Instance.FadeIn();
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

    public void PlayEndingCutscene()
    {
        FadeController.Instance.FadeOut(() =>
        {
            progressbar.SetActive(false);
            endingCutscenePanel.SetActive(true);
            endingVideoPlayer.Play();
            FadeController.Instance.FadeIn();
        });
    }

    public void LoadScene(string sceneName)
    {
        FadeController.Instance.FadeOut(() => SceneManager.LoadScene(sceneName));
    }

    public void LoadNextScene()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        FadeController.Instance.FadeOut(() => SceneManager.LoadScene(nextScene));
    }

    public void ReloadScene()
    {
        string current = SceneManager.GetActiveScene().name;
        FadeController.Instance.FadeOut(() =>
        {
            SceneManager.LoadScene(current);
            PauseMenuPanel.SetActive(false);
        });
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