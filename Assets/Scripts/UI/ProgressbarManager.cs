using UnityEngine;

public class ProgressbarManager : MonoBehaviour
{
    public static ProgressbarManager Instance;

    [SerializeField] private float towerHeight;

    [SerializeField] private float segmentStartHeight;
    [SerializeField] private float segmentEndHeight;

    private float currentOverallProgress;
    public float currentProgress => currentOverallProgress;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void SetSegmentRange(float startHeight, float endHeight)
    {
        segmentStartHeight = startHeight;
        segmentEndHeight = endHeight;
    }

    public void UpdatePlayerHeight(float currentPlayerY)
    {
        float clampedY = Mathf.Clamp(currentPlayerY, segmentStartHeight, segmentEndHeight);
        float overallHeight = Mathf.Lerp(segmentStartHeight, segmentEndHeight, 
            Mathf.InverseLerp(segmentStartHeight, segmentEndHeight, clampedY));

        currentOverallProgress = overallHeight / towerHeight;
    }
}
