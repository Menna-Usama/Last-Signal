using System.Collections;
using UnityEngine;

public class VFXHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem shockWave;

    private void OnEnable()
    {
        Jump.OnPlayerJumped += PlayShockwave;
    }

    private void OnDisable()
    {
        Jump.OnPlayerJumped -= PlayShockwave;
    }

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