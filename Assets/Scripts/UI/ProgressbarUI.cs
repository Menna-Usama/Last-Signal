using UnityEngine;
using UnityEngine.UI;

public class ProgressbarUI : MonoBehaviour
{
 
    [SerializeField] private Slider progressSlider;

    void Update()
    {
        progressSlider.value = ProgressbarManager.Instance.currentProgress;
    }
}

