using UnityEngine;

public class CurrentPlatform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlatformController platform = collision.gameObject.GetComponent<PlatformController>();

        if (platform != null)
        {
            PlatformManager.Instance.CurrentPlatform = platform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        PlatformController platform = collision.gameObject.GetComponent<PlatformController>();

        if (platform != null && PlatformManager.Instance.CurrentPlatform == platform)
        {
            PlatformManager.Instance.CurrentPlatform = null;
        }
    }
}
