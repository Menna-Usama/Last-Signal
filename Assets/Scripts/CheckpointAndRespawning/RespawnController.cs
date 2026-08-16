using UnityEngine;

public class RespawnController : MonoBehaviour
{
    public static RespawnController Instance { get; private set; }
    public Transform respawnPoint;
    public GameObject player;
    private void Start()
    {
    }


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


    public void Respawn()
    {
        player.transform.position = respawnPoint.position;
        //GameSceneManager.Instance.PauseMenuPanel.SetActive(false);
    }


}
