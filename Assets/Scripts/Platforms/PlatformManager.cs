using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance { get; private set; }
    private readonly List<PlatformGroupController> platforms = new();
    public PlatformGroupController CurrentPlatform { get; set; }


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
        GuardianJump.OnGuardianJump += CyclePlatforms;
    }
    private void OnDisable()
    {
        Jump.OnPlayerJumped -= CyclePlatforms;
        GuardianJump.OnGuardianJump -= CyclePlatforms;
    }



    public void Register(PlatformGroupController platform)
    {
        if (!platforms.Contains(platform))
            platforms.Add(platform);
    }
    public void Unregister(PlatformGroupController platform)
    {
        platforms.Remove(platform);
    }


    private void CyclePlatforms()
    {
        List<PlatformGroupController> copy = new List<PlatformGroupController>(platforms);

        foreach (PlatformGroupController platform in copy)
        {
            if (platform != null)//&& platform != CurrentPlatform used to be here but it exempts the whole platform type not an individual platform
            {
                platform.Cycle();
            }
        }
    }


}
