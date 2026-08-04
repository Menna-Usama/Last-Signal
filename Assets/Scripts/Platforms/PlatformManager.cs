using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance { get; private set; }
    private readonly List<PlatformController> platforms = new();
    public PlatformController CurrentPlatform { get; set; }

    [Header("Platform Prefabs")]
    [SerializeField] private GameObject normalPrefab;
    [SerializeField] private GameObject movingPrefab;
    [SerializeField] private GameObject blinkingPrefab;



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    private void OnEnable()
    {
        Jump.OnPlayerJumped += CyclePlatforms;
    }
    private void OnDisable()
    {
        Jump.OnPlayerJumped -= CyclePlatforms;
    }



    public void Register(PlatformController platform)
    {
        if (!platforms.Contains(platform))
            platforms.Add(platform);
    }
    public void Unregister(PlatformController platform)
    {
        platforms.Remove(platform);
    }


    public GameObject GetNextPrefab(PlatformType type)
    {
        switch (type)
        {
            case PlatformType.Normal:
                return movingPrefab;

            case PlatformType.Moving:
                return blinkingPrefab;

            default:
                return normalPrefab;
        }
    }


    private void CyclePlatforms()
    {
        List<PlatformController> copy = new List<PlatformController>(platforms);

        foreach (PlatformController platform in copy)
        {
            if (platform != null && platform != CurrentPlatform)
            {
                platform.Cycle();
            }
        }
    }


}
