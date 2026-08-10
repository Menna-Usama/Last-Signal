using UnityEngine;
using System.Collections;
using System;

public class GuardianJump : MonoBehaviour
{
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float jumpDuration = 1f;
    [SerializeField] private float waitForJump = 3f;
    private Rigidbody2D rb;

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
            yield return new WaitForSeconds(waitForJump);
            yield return StartCoroutine(Jump());
        }
    }

    private IEnumerator Jump()
    {
        OnGuardianJump?.Invoke();

        Vector2 startPosition = rb.position;
        float elapsed = 0f;

        while (elapsed < jumpDuration)
        {
            elapsed += Time.fixedDeltaTime;
            float t = elapsed / jumpDuration;
            float height = Mathf.Sin(t * Mathf.PI) * jumpHeight;

            rb.MovePosition(startPosition + Vector2.up * height);
            yield return new WaitForFixedUpdate();
        }

        rb.MovePosition(startPosition);
    }

}
