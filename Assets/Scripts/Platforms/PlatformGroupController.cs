using System.Collections.Generic;
using UnityEngine;


public enum PlatformType { Normal, Moving, Blinking, Falling }//add to it later

public class PlatformGroupController : MonoBehaviour
{
    [System.Serializable]
    public struct StateEntry
    {
        public PlatformType type;
        public GameObject stateObject;
    }


    [Tooltip("List the only behaviours that are going to be used in this scene, in order")]
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


        int movingIndex = IndexOfType(PlatformType.Moving);
        int normalIndex = IndexOfType(PlatformType.Normal);

        if (movingIndex != -1 && normalIndex != -1)//if they are found
        {
            MovingPlatform moving = states[movingIndex].stateObject.GetComponent<MovingPlatform>();
            moving.InOrigin(states[normalIndex].stateObject.transform.position);
        }

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

        Vector3 currentPos = states[currentIndex].stateObject.transform.position;//save the transform where the active platforms are

        currentIndex = index;

        states[currentIndex].stateObject.transform.position = currentPos;//put the new platform in the position the old ones were

        //Debug.Log($"{name} switched to {states[currentIndex].type}");

        for (int i = 0; i < states.Count; i++)
        {
            states[i].stateObject.SetActive(i == currentIndex);
        }

    }

    private int IndexOfType(PlatformType type)//just finds the index of whatever type
    {
        for (int i = 0; i < states.Count; i++)
        {
            if (states[i].type == type)
                return i;
        }
        return -1; //if it's not there
    }

}
