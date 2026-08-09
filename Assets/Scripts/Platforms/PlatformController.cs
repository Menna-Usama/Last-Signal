using UnityEngine;
using UnityEngine.Rendering;

public class PlatformController : MonoBehaviour
{

    [SerializeField] private PlatformType platformType;
    [SerializeField] private PlatformFamily platformFamily;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlatformManager.Instance.Register(this);
    }
    private void OnDestroy()
    {
        if (PlatformManager.Instance != null)
        {
            PlatformManager.Instance.Unregister(this);
        }
    }


    public void Cycle()
    {
        PlatformType nextType = GetNextType();

        GameObject nextPrefab = platformFamily.GetNextPrefab(nextType);

        Instantiate(nextPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private PlatformType GetNextType()
    {
        switch (platformType)
        {
            case PlatformType.Normal:
                return PlatformType.Moving;

            case PlatformType.Moving:
                return PlatformType.Blinking;

            case PlatformType.Blinking:
                return PlatformType.Falling;

            default:
                return PlatformType.Normal;
        }
    }

}
