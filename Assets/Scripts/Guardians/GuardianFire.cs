using UnityEngine;
using System.Collections;
using System;

public class GuardianFire : MonoBehaviour
{
    private GuardianDeath guardianDeath;
    [SerializeField] private GameObject _guardianProjectile;
    [SerializeField] private float _horizontalFiringDistance = 6f;
    [SerializeField] private float _verticalFiringDistance = 6f;
    [SerializeField] private float _shootingInterval = 0.75f; // time between single shots during a burst
    [SerializeField] private float _burstDuration = 4f;   // how long the guardian will keep firing
    [SerializeField] private float _cooldownDuration = 5f; // how long it pauses between bursts

    private bool _isNextVertical = false;
    public bool IsFiring { get; private set; }

    public static event Action onProjectileFired;

    private void Awake()
    {
        guardianDeath = GetComponent<GuardianDeath>();

    }
    private void OnEnable()
    {
        guardianDeath.OnGuardianDefeated += StopFiring;
    }
    private void OnDisable()
    {
        guardianDeath.OnGuardianDefeated -= StopFiring;
    }

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
            IsFiring = true;
            // Firing burst
            float burstTimer = 0f;
            while (burstTimer < _burstDuration)
            {
                yield return new WaitForSeconds(_shootingInterval);
                burstTimer += _shootingInterval;

                if (_isNextVertical)
                {
                    FireVerticalPair();
                }
                else
                {
                    FireHorizontalPair();
                }
                _isNextVertical = !_isNextVertical;
            }

            // Cooldown pause
            yield return new WaitForSeconds(_cooldownDuration);
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
    }

    private GameObject SpawnProjectile(Vector2 direction, float travelDistance)
    {
        if (_guardianProjectile != null)
        {
            GameObject proj = Instantiate(_guardianProjectile, transform.position, Quaternion.identity);
            GuardianProjectile projScript = proj.GetComponentInChildren<GuardianProjectile>();

            projScript.Launch(direction, travelDistance);
            Physics2D.IgnoreCollision(proj.GetComponent<Collider2D>(), GetComponent<Collider2D>());

            onProjectileFired?.Invoke();

            return proj;
        }
        return null;
    }
    public void InterruptAttack()
    {
        StopAllCoroutines();
        _isNextVertical = false; // reset pattern so it starts fresh horizontal next time
        StartCoroutine(FiringLoop()); // restart the loop 
    }
}