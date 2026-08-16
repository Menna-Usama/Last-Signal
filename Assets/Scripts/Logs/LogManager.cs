using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

public class LogManager : MonoBehaviour
{

    public static LogManager Instance { get; private set; }
    private readonly List<LogData> _collectedLogs = new();
    [SerializeField]private LogUIDisplay uiDisplay;//LogUIDisplay is another script

    private int logsCollected, logsRequired = 4;
    public int LogsCollected => logsCollected;
    public int LogsRequired => logsRequired;

    public event Action OnAllLogsCollected;

    private bool _allLogsCollectedFired = false;

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
        logsCollected++;
        //Debug.Log("Calling ShowLogText" + log.logText);
        uiDisplay.ShowLogText(log.logText);
        uiDisplay.ShowCollectedLogsText();

        if (logsCollected >= logsRequired && !_allLogsCollectedFired)
        {
            _allLogsCollectedFired = true;
            OnAllLogsCollected?.Invoke();
        }

    }


    public List<LogData> GetCollectedLogs() => _collectedLogs;

}
