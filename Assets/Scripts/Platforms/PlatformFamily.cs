using UnityEngine;
using System;


[Serializable]
public class PlatformPrefab
{
    public PlatformType type;
    public GameObject prefab;
}


[CreateAssetMenu(menuName = "Platforms/Platform Family")]
public class PlatformFamily : ScriptableObject
{

    public PlatformPrefab[] prefabs;

    public GameObject GetNextPrefab(PlatformType type)
    {
        foreach (PlatformPrefab platform in prefabs)
        {
            if (platform.type == type)
            {
                return platform.prefab;
            }
        }

        return null;
    }

}
