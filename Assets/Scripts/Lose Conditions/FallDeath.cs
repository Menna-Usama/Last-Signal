using UnityEngine;
using System;
using System.Threading.Tasks;

public class FallDeath : MonoBehaviour
{
    public static event Action OnPlayerLost;

    [SerializeField] private float deathDistance = 5f;
    private float fallStartY;
    private bool isFalling;

    private Rigidbody2D rb;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.linearVelocityY < 0)
        {
            if (!isFalling)
            {
                isFalling = true;
                fallStartY = transform.position.y;
            }

            float fallenDistance = fallStartY - transform.position.y;

            if (fallenDistance > deathDistance)
            {
                Lose();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Ground"))
        {
            ResetFall();
        }
    }


    private void ResetFall()
    {
        isFalling = false;
        fallStartY = transform.position.y;
    }


    private void Lose()
    {
        isFalling = false;
        OnPlayerLost?.Invoke();
    }
}
