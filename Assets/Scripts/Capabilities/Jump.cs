using UnityEngine;
using System;

[RequireComponent(typeof(Controller))]
public class Jump : MonoBehaviour
{
    public static event Action OnPlayerJumped;

    [SerializeField, Range(0f, 10f)] private float _jumpHeight = 3f;
    [SerializeField, Range(0, 5)] private int _maxAirJumps = 0;
    [SerializeField, Range(0f, 5f)] private float _downwardMovementMultiplier = 3f; // gravity while the player falling
    [SerializeField, Range(0f, 5f)] private float _upwardMovementMultiplier = 1.7f; // gravity while the player jumping

    private Controller _controller;
    private Rigidbody2D _rb;
    private Ground _ground;
    private Vector2 _velocity;

    private int _jumpPhase;
    private float _defaultGravityScale, _jumpSpeed;

    private bool _desiredJump, _onGround;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _ground = GetComponent<Ground>();
        _controller = GetComponent<Controller>();

        _defaultGravityScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        _desiredJump |= _controller.input.RetrieveJumpInput();
    }

    private void FixedUpdate()
    {
        _onGround = _ground.OnGround;
        _velocity = _rb.linearVelocity;

        if (_onGround)
        {
            _jumpPhase = 0;
        }

        if (_desiredJump)
        {
            _desiredJump = false;
            JumpAction();
        }

        if (_rb.linearVelocity.y > 0)
        {
            _rb.gravityScale = _upwardMovementMultiplier;
        }
        else if (_rb.linearVelocity.y < 0)
        {
            _rb.gravityScale = _downwardMovementMultiplier;
        }
        else if (_rb.linearVelocity.y == 0)
        {
            _rb.gravityScale = _defaultGravityScale;
        }

        _rb.linearVelocity = _velocity;
    }

    private void JumpAction()
    {
        if (_onGround || _jumpPhase < _maxAirJumps)
        {
            _jumpPhase += 1;

            _jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _jumpHeight);

            if (_velocity.y > 0f)
            {
                _jumpSpeed = Mathf.Max(_jumpSpeed - _velocity.y, 0f);
            }
            else if (_velocity.y < 0f)
            {
                _jumpSpeed += Mathf.Abs(_rb.linearVelocity.y);
            }
            _velocity.y += _jumpSpeed;

            if (_onGround)
            {
                OnPlayerJumped?.Invoke();
            }
        }
    }
}
