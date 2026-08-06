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

    private Controller _controller;
    private Rigidbody2D _rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _controller = GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
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
        _rb.linearVelocity = new Vector2(transform.localScale.x * _dashingPower, 0f);
        yield return new WaitForSeconds(_dashingTime);

        _rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(_dashingCooldown);
        canDash = true;
    }
}
