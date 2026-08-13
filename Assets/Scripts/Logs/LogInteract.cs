using UnityEngine;

public class LogInteract : MonoBehaviour
{
    private bool isPlayerTouchLog;



    private void Update()
    {
        if(isPlayerTouchLog && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Read Log"); // to be an event connected with the ui manager.
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
