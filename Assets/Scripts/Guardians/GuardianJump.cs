using UnityEngine;
using System.Collections;
using System;

public class GuardianJump : MonoBehaviour
{
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float jumpDuration = 1f;
    [SerializeField] private float waitBeforeJump = 5f;   // matches Guardian Fire's Burst Duration
    [SerializeField] private float waitAfterJump = 3f;    // pads out the remaining cycle time

    private Rigidbody2D rb;

    public bool IsJumping { get; private set; }
    public static event Action OnGuardianJump;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        GuardianDeath.OnGuardianDefeated += StopJumping;
    }
    private void OnDisable()
    {
        GuardianDeath.OnGuardianDefeated -= StopJumping;
    }

    private void Start()
    {
        StartCoroutine(JumpLoop());
    }

    private void StopJumping()
    {
        StopAllCoroutines();
    }

    private IEnumerator JumpLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitBeforeJump);
            yield return StartCoroutine(Jump(Vector2.up));
            yield return new WaitForSeconds(waitAfterJump);
        }
    }

    public IEnumerator Jump(Vector2 dir)
    {
        IsJumping = true;
        OnGuardianJump?.Invoke();

        Vector2 startPosition = rb.position;
        float elapsed = 0f;

        while (elapsed < jumpDuration)
        {
            elapsed += Time.fixedDeltaTime;
            float t = elapsed / jumpDuration;
            float height = Mathf.Sin(t * Mathf.PI) * jumpHeight;

            rb.MovePosition(startPosition + dir * height);
            yield return new WaitForFixedUpdate();
        }

        rb.MovePosition(startPosition);
        IsJumping = false;
    }
}