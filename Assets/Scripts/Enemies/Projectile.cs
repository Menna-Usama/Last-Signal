using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private float _projectileSpd = 5;
    [SerializeField] private float _knockBackForce = 10;

    [Header("Physicalayer")]
    [SerializeField] private LayerMask wallLayer;

    private Vector2 _direction;
    private Vector3 _spawnPos;
    private float _travelDistance;
    private Rigidbody2D rb;
    private float _distanceTravelled;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        _distanceTravelled = Vector3.Distance(transform.position ,_spawnPos);//get the distance the projectile travelled
        if(_distanceTravelled >= _travelDistance)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float travelDistance)
    {
        _direction = direction;
        _travelDistance = travelDistance;
        _spawnPos = transform.position;

        rb.linearVelocity = _projectileSpd * _direction;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRB = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRB != null)
            {
                playerRB.linearVelocity = Vector2.zero;//stop player's movement
                playerRB.AddForce(_direction * _knockBackForce, ForceMode2D.Impulse);//hit player with knockback


                //FOR ANY HEALTH FOR PLAYER, ADD LOGIC OF MAYBE PlayerHealth.Instance.LoseLife OR SOMETHING LIKE THAT HERE

                Destroy(gameObject);
                return;
            }


        }
        if (IsInLayerMask(collision.gameObject.layer, wallLayer))
        {
            //check if the thing we collided with in a wall _using layers_ and if so destroy the projectile
            //code here is pretty reusable
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }
    }

    private bool IsInLayerMask(int layer, LayerMask mask)//check for a specific layer
    {
        return (mask.value & (1 << layer)) != 0;
    }
}
