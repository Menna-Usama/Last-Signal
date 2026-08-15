using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class LogManager : MonoBehaviour
{

    public static LogManager Instance { get; private set; }
    private readonly List<LogData> _collectedLogs = new();
    [SerializeField]private LogUIDisplay uiDisplay;//LogUIDisplay is another script

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }


    public void CollectLog(LogData log)//LogInteract accesses this func
    {
        if (_collectedLogs.Contains(log)) return;

        _collectedLogs.Add(log);
        //Debug.Log("Calling ShowLogText" + log.logText);
        uiDisplay.ShowLogText(log.logText);
    }


    public List<LogData> GetCollectedLogs() => _collectedLogs;

}
