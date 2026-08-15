using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Controller))]
public class Dash : MonoBehaviour
{
    public static event Action OnPlayerDashed;
    private bool canDash = true;
    public bool isDashing; // public cus normalenemy uses it

    [SerializeField, Range(0f, 20f)] private float _dashingPower = 10f;
    [SerializeField, Range(0f, 5f)] private float _dashingTime = 0.2f;
    [SerializeField, Range(0f, 5f)] private float _dashingCooldown = 1f;
    private float _lastDirection = 1f; // defaults to right if player hasn't moved yet

    private float _lastDirection;

    private Controller _controller;
    private Rigidbody2D _rb;
    [SerializeField] private TrailRenderer _trail;


    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _controller = GetComponent<Controller>();
        _trail = GetComponent<TrailRenderer>();
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        float inputDirection = Input.GetAxisRaw("Horizontal");

        if (inputDirection != 0)
        {
            _lastDirection = inputDirection;
        }


        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(ToDash());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
    }

    private IEnumerator ToDash()
    {
        canDash = false;
        isDashing = true;

        float originalGravity = _rb.gravityScale;
        _rb.gravityScale = 0f;

        _trail.emitting = true;

        _rb.linearVelocity = new Vector2(_lastDirection * _dashingPower, 0f);

        OnPlayerDashed?.Invoke();


        yield return new WaitForSeconds(_dashingTime);

        _trail.emitting = false;

        _rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(_dashingCooldown);
        canDash = true;
    }
}