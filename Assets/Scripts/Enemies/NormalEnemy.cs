using System.Collections;
using UnityEngine;

public class NormalEnemy : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField]private GameObject _enemyProjectile;
    [SerializeField] private float _horizontalFiringDistance = 4f;
    [SerializeField] private float _verticalFiringDistance = 4f;
    [SerializeField]private float _shootingInterval = 2f;
    [SerializeField]private float _EnemyknockBackForce = 2f;


    [Header("Health")]
    [SerializeField] private int _startingEnemyHealth = 2;
    private int _hitsTaken;
    private bool _isDefeated;
    private bool _isNextVertical = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(FiringLoop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator FiringLoop()
    {
        while (!_isDefeated)
        {
            yield return new WaitForSeconds(_shootingInterval);

            if (_isDefeated) yield break;

            if (_isNextVertical)
            {
                FireVerticalPair();
            }
            else if (!_isNextVertical)
            {
                FireHorizontalPair();
            }
            _isNextVertical = !_isNextVertical;
        }
    }

    private void FireHorizontalPair()
    {
        GameObject left = SpawnProjectile(Vector2.left, _horizontalFiringDistance);
        GameObject right = SpawnProjectile(Vector2.right, _horizontalFiringDistance);
        Physics2D.IgnoreCollision(left.GetComponent<Collider2D>(), right.GetComponent<Collider2D>());
    }
    private void FireVerticalPair()
    {

        GameObject up = SpawnProjectile(Vector2.up, _verticalFiringDistance);
        GameObject down = SpawnProjectile(Vector2.down, _verticalFiringDistance);//leave the projectile going down
        Physics2D.IgnoreCollision(down.GetComponent<Collider2D>(), up.GetComponent<Collider2D>());

    }

    private GameObject SpawnProjectile(Vector2 direction, float travelDistance)
    {
        if (_enemyProjectile != null)
        {
            GameObject proj = Instantiate(_enemyProjectile, transform.position, Quaternion.identity);
            Projectile projScript = proj.GetComponentInChildren<Projectile>();
            //Debug.Log(projScript == null ? "Projectile script missing on prefab!" : "Found it");
            projScript.Launch(direction, travelDistance);
            Physics2D.IgnoreCollision(proj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            // Projectile is the script on the enemies projectile, launch is a func in it
            return proj;
        }
        return null;
    }


    //now for the dashing detection and health

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isDefeated) return;

        Dash dash = collision.gameObject.GetComponent<Dash>();//fetch the Dash script on the player
        if(dash != null && dash.isDashing)
        {
            TakeHit();
        }


        if(collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRB = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRB != null)
            {
                Vector2 enemyToPlayerDirection = collision.transform.position - transform.position;
                playerRB.linearVelocity = Vector2.zero;//stop player's movement
                playerRB.AddForce(enemyToPlayerDirection * _EnemyknockBackForce, ForceMode2D.Impulse);//hit player with knockback
            }
        }

    }

    private void TakeHit()
    {
        _hitsTaken++;
        if(_hitsTaken >= _startingEnemyHealth)
        {
            _isDefeated = true;
            EnemyDefeated();
        }
    }

    private void EnemyDefeated()
    {
        StopAllCoroutines();
        Destroy(transform.parent.gameObject); //because logic is child of the empty enemy parent

    }
}
