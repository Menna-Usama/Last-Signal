using System.Collections;
using UnityEngine;

public class BlinkingPlatform : MonoBehaviour
{

    [SerializeField, Range(1, 10)] float _blinkWait = 2f;
    [SerializeField, Range(1, 10)] float _appearWait = 2f;

    private bool _isBlinking;

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Collider2D _platformCollider;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _platformCollider = GetComponent<Collider2D>();
        _sr = GetComponent<SpriteRenderer>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isBlinking && collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Blink());
        }
    }

    private IEnumerator Blink()
    {
        _isBlinking = true;
        yield return new WaitForSeconds(_blinkWait);
        _platformCollider.enabled = false;
        _sr.enabled = false;

        yield return new WaitForSeconds(_appearWait);
        _platformCollider.enabled = true;
        _sr.enabled = true;
        _isBlinking = false;
    }




}
