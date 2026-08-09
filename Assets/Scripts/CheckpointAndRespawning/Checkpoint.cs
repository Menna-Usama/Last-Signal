using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private CapsuleCollider2D trigger;

    private void Start()
    {
        trigger = GetComponent<CapsuleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RespawnController.Instance.respawnPoint = transform;
            trigger.enabled = false;
        }
    }


}
