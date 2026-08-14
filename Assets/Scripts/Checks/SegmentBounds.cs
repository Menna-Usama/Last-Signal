using UnityEngine;

public class SegmentBounds : MonoBehaviour
{
    [SerializeField] private float startHeight;
    [SerializeField] private float endHeight;

    void Start()
    {
        ProgressbarManager.Instance.SetSegmentRange(startHeight, endHeight);
    }
}
