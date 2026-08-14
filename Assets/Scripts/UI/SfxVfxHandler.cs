using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class VFXHandler : MonoBehaviour
{
    [Header("SFX")]
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AudioSource CamAudioSource;

    [SerializeField] private AudioClip gamePlayTheme;

    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip HitTheGroundSound;
    [SerializeField] private AudioClip DashSound;

    [SerializeField] private AudioClip CollectSound;


    [Header("VFX")]
    [SerializeField] private ParticleSystem shockWave;

    private void OnEnable()
    {
        GameSceneManager.onGameStart += PlayGameplayTheme;

        Jump.OnPlayerJumped += PlayShockwave;
        Jump.OnPlayerJumped += PlayJumpSound;

        Jump.OnPlayerLanded += PlayGroundHitSound;

        Dash.OnPlayerDashed += PlayDashSound;


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
        playerAudioSource.PlayOneShot(jumpSound, 1f);   
    }
    private void PlayDashSound()
    {
        playerAudioSource.PlayOneShot(DashSound, 1f);
    }
    private void PlayGroundHitSound()
    {
        playerAudioSource.PlayOneShot(HitTheGroundSound, 1f);
    }

    // ======= VFX =======
    private void PlayShockwave()
    {
        shockWave.Play();
        StartCoroutine(ParticleStopDelay());
    }
    IEnumerator ParticleStopDelay()
    {
        yield return new WaitForSeconds(1f);
        shockWave.Stop();

    }
}