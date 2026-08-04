using UnityEngine;

public class PlatformController : MonoBehaviour
{

    [SerializeField] private PlatformType platformType;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlatformManager.Instance.Register(this);
    }
    private void OnDestroy()
    {
        if (PlatformManager.Instance != null)
            PlatformManager.Instance.Unregister(this);
    }


    public void Cycle()
    {
        GameObject nextPrefab = PlatformManager.Instance.GetNextPrefab(platformType);

        Instantiate(nextPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
