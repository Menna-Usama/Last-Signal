using UnityEngine;
using System.Collections;
using System;

public class GuardianJump : MonoBehaviour
{
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float jumpDuration = 1f;
    [SerializeField] private int waitForJump;
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
            waitForJump = UnityEngine.Random.Range(3, 11);
            yield return new WaitForSeconds(waitForJump);
            yield return StartCoroutine(Jump(Vector2.up));
        }
    }

    public IEnumerator Jump(Vector2 dir)
    {
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
    }

}
