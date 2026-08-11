using UnityEngine;
using System.Collections;

public class GuardianFire : MonoBehaviour
{

    [SerializeField] private GameObject _guardianProjectile;
    [SerializeField] private float _horizontalFiringDistance = 6f;
    [SerializeField] private float _verticalFiringDistance = 6f;
    [SerializeField] private float _shootingInterval = 0.75f;

    private bool _isNextVertical = false;




    private void OnEnable()
    {
        GuardianDeath.OnGuardianDefeated += StopFiring;
    }
    private void OnDisable()
    {
        GuardianDeath.OnGuardianDefeated -= StopFiring;
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(FiringLoop());
    }

    private void StopFiring()
    {
        StopAllCoroutines();
    }

    private IEnumerator FiringLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(_shootingInterval);

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
        SpawnProjectile(Vector2.up, _verticalFiringDistance);
        //SpawnProjectile(Vector2.down, _verticalFiringDistance); that just hits the platform so it's useless
    }

    private GameObject SpawnProjectile(Vector2 direction, float travelDistance)
    {
        if (_guardianProjectile != null)
        {
            GameObject proj = Instantiate(_guardianProjectile, transform.position, Quaternion.identity);
            GuardianProjectile projScript = proj.GetComponentInChildren<GuardianProjectile>();

            projScript.Launch(direction, travelDistance);
            Physics2D.IgnoreCollision(proj.GetComponent<Collider2D>(), GetComponent<Collider2D>());

            return proj;
        }
        return null;
    }
}
