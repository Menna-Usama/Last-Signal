using System;
using UnityEngine;

public class LogInteract : MonoBehaviour
{
    private bool isPlayerTouchLog;
    public static event Action onLogInteract;


    private void Update()
    {
        if(isPlayerTouchLog && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Read Log"); // to be an event connected with the ui manager.

            onLogInteract?.Invoke();
        }
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
