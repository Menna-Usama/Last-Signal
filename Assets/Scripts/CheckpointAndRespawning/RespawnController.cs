using UnityEngine;

public class RespawnController : MonoBehaviour
{
    public static RespawnController Instance { get; private set; }
    public Transform respawnPoint;
    public GameObject player;
    public bool isDead = false; // to be and event from the "lose" script, now just for testing the respawn system.
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        FallDeath.OnPlayerLost += Respawn;
    }
    private void OnDisable()
    {
        FallDeath.OnPlayerLost -= Respawn;
    }

    //private void Update()
    //{
    //    if (isDead)
    //    {
    //        Respawn();
    //    }
    //}


    private void Respawn()
    {
        player.transform.position = respawnPoint.position;

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;//added these two line to make sure player doesn't retain velocity when respawning

        isDead = false;
    }


}
