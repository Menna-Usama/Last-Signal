using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SfxVfxHandler : MonoBehaviour
{
    [Header("SFX")]
     private AudioSource playerAudioSource;
     public AudioSource CamAudioSource;

    [SerializeField] private AudioClip gamePlayTheme;

    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip HitTheGroundSound;
    [SerializeField] private AudioClip DashSound;

    [SerializeField] private AudioClip CollectSound;


    [Header("VFX")]
     private ParticleSystem shockWave;

    private void OnEnable()
    {
        GameSceneManager.onGameStart += PlayGameplayTheme;

        Jump.OnPlayerJumped += PlayShockwave;
        Jump.OnPlayerJumped += PlayJumpSound;

        Jump.OnPlayerLanded += PlayGroundHitSound;

        Dash.OnPlayerDashed += PlayDashSound;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindSceneReferences();
    }

    private void FindSceneReferences()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerAudioSource = player.GetComponent<AudioSource>();
            shockWave = player.GetComponentInChildren<ParticleSystem>();
        }

        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        if (cam != null)
        {
            CamAudioSource = cam.GetComponent<AudioSource>();
        }
    }

    private void OnDisable()
    {
        GameSceneManager.onGameStart -= PlayGameplayTheme;

        Jump.OnPlayerJumped -= PlayShockwave;
        Jump.OnPlayerJumped -= PlayJumpSound;

        Jump.OnPlayerLanded -= PlayGroundHitSound;

        Dash.OnPlayerDashed -= PlayDashSound;


    }
    // ======== SFX =========
    private void PlayGameplayTheme()
    {
        CamAudioSource.clip = gamePlayTheme;
        CamAudioSource.loop = true;
        CamAudioSource.Play();
    }
    private void PlayJumpSound()
    {
        if (playerAudioSource != null) playerAudioSource.PlayOneShot(jumpSound, 1f);   
    }
    private void PlayDashSound()
    {
        if (playerAudioSource != null) playerAudioSource.PlayOneShot(DashSound, 1f);
    }
    private void PlayGroundHitSound()
    {
        if (playerAudioSource != null) playerAudioSource.PlayOneShot(HitTheGroundSound, 1f);
    }

    // ======= VFX =======
    private void PlayShockwave()
    {
        if (shockWave != null) shockWave.Play();
    }
   
}