using UnityEngine;

public class PlayerHeightProgress : MonoBehaviour
{
    void Update()
    {
        ProgressbarManager.Instance.UpdatePlayerHeight(transform.position.y);
    }
}
