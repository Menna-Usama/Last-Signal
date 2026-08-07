using UnityEngine;

public class CurrentPlatform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlatformGroupController platform = collision.gameObject.GetComponentInParent<PlatformGroupController>();

        if (platform != null)
        {
            PlatformManager.Instance.CurrentPlatform = platform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        PlatformGroupController platform = collision.gameObject.GetComponentInParent<PlatformGroupController>();

        if (platform != null && PlatformManager.Instance.CurrentPlatform == platform)
        {
            PlatformManager.Instance.CurrentPlatform = null;
        }
    }
}
