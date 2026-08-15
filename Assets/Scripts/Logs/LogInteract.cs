using System;
using UnityEngine;

public class LogInteract : MonoBehaviour
{
    private bool isPlayerTouchLog;


    public static event Action onLogInteract;//not sure event is used anywhere
    private bool _logCollected = false;
    [SerializeField]private LogData logData; //LogData is the scriptable object


    private void Update()
    {
        if(isPlayerTouchLog && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Read Log"); // to be an event connected with the ui manager.

            onLogInteract?.Invoke();
            Debug.Log("Interacted");
            Collect();
            
        }
    }

    private void Collect()
    {
        _logCollected = true;
        LogManager.Instance.CollectLog(logData);
        Debug.Log("log collected" + logData.logText + logData.logID);


        //maybe add the glow effect here
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerTouchLog = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerTouchLog = false;
        }
    }


}
