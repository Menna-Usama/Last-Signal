using System.Collections.Generic;
using UnityEngine;

public enum PlatformType { Normal, Moving, Blinking, Falling }

public class PlatformGroupController : MonoBehaviour
{
    [System.Serializable]
    public struct StateEntry
    {
        public PlatformType type;
        public GameObject stateObject;
    }

    [Tooltip("Only list the states this platform actually cycles through, in cycle order.")]
    [SerializeField] private List<StateEntry> states = new();

    [SerializeField] private int currentIndex = 0;
    public PlatformType CurrentType => states[currentIndex].type;

    private void Awake()
    {
        Debug.Log($"{name} Awake");
        Debug.Log($"Manager: {PlatformManager.Instance}");

        //PlatformManager.Instance.Register(this);
        //ApplyState(currentIndex);
    }

    private void Start()
    {
        PlatformManager.Instance.Register(this);
        ApplyState(currentIndex);
    }

    private void OnDestroy()
    {
        if (PlatformManager.Instance != null)
            PlatformManager.Instance.Unregister(this);
    }

    public void Cycle()
    {
        ApplyState((currentIndex + 1) % states.Count);
    }

    private void ApplyState(int index)
    {
        currentIndex = index;

        Debug.Log($"{name} switched to {states[currentIndex].type}");

        for (int i = 0; i < states.Count; i++)
            states[i].stateObject.SetActive(i == currentIndex);

    }
}
