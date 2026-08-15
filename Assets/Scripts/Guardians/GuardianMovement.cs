using UnityEngine;
using System;

public class GuardianMovement : MonoBehaviour
{
    public static event Action<bool> OnGuardianMoveStateChanged; // true= moving, false = idle

    [SerializeField] private Vector3 moveOffset = new Vector3(3f, 0f, 0f);
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float waitTime = 10f;
    [SerializeField] private GuardianJump guardianJump;

    private Vector3 _pointA;
    private Vector3 _pointB;
    private Vector3 _nextPosition;
    private bool _isMoving = false;
    private float _waitTimer;

    void Start()
    {
        _pointA = transform.position;
        _pointB = transform.position + moveOffset;
        _nextPosition = _pointB;
        _waitTimer = waitTime;
    }

    void Update()
    {

        if (guardianJump != null && guardianJump.IsJumping)
        {
            return; 
        }
        if (!_isMoving)
        {
            _waitTimer -= Time.deltaTime;

            if (_waitTimer <= 0f)
            {
                _isMoving = true;
                OnGuardianMoveStateChanged?.Invoke(true);
            }
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, _nextPosition, moveSpeed * Time.deltaTime);
        FlipTowardsMovement();

        if (Vector3.Distance(transform.position, _nextPosition) < 0.01f)
        {
            _nextPosition = (_nextPosition == _pointA) ? _pointB : _pointA;
            _isMoving = false;
            _waitTimer = waitTime;
            OnGuardianMoveStateChanged?.Invoke(false);
        }
    }

    private void FlipTowardsMovement()
    {
        float direction = (_nextPosition - transform.position).x;
        if (Mathf.Abs(direction) > 0.01f)
        {
            Vector3 scale = transform.localScale;
            scale.x = direction > 0 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }
}