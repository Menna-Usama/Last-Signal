using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller))]
public class Dash : MonoBehaviour
{
    private bool canDash = true;
    public bool isDashing; //public cus normalenemy uses it
    [SerializeField, Range(0f, 20f)] private float _dashingPower = 10f;
    [SerializeField, Range(0f, 5f)] private float _dashingTime = 0.2f;
    [SerializeField, Range(0f, 5f)] private float _dashingCooldown = 1f;

    private float _lastDirection;

    private Controller _controller;
    private Rigidbody2D _rb;
    private TrailRenderer _trail;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _controller = GetComponent<Controller>();
        _trail = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
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

        //_direction = Input.GetAxisRaw("Horizontal");
        //if (_direction == 0) _direction = transform.localScale.x > 0 ? 1 : -1; // calculating the dash direction.

        _rb.AddForce(Vector2.right * _lastDirection * _dashingPower, ForceMode2D.Impulse); // default: dash to right.

        yield return new WaitForSeconds(_dashingTime);

        _trail.emitting = false;

        _rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(_dashingCooldown);
        canDash = true;
    }
}
