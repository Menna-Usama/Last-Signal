using UnityEngine;

public class GuardianProjectile : MonoBehaviour
{
    [SerializeField] private float _projectileSpd = 7;
    [SerializeField] private float _knockBackForce = 10;

    private Vector2 _direction;
    private Vector3 _spawnPos;
    private float _travelDistance;
    private Rigidbody2D rb;
    private float _distanceTravelled;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _distanceTravelled = Vector3.Distance(transform.position, _spawnPos);
        if (_distanceTravelled >= _travelDistance)
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
                playerRB.linearVelocity = Vector2.zero;
                playerRB.AddForce(_direction * _knockBackForce, ForceMode2D.Impulse);

                Destroy(gameObject);
                return;
            }
        }

        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Guardian") || collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(gameObject);
        }
    }
}
