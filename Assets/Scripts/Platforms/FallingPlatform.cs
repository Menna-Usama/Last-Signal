using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 2f;

    private Vector3 _nextPosition;
    private Vector3 _lastPosition;

    private Transform _player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _nextPosition = pointB.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _nextPosition, moveSpeed * Time.deltaTime);
    }

    private void LateUpdate()
    {
        
        if (_player != null)
        {
            Vector3 delta = transform.position - _lastPosition;
            _player.position += delta;
        }

        _lastPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player = collision.transform;
            _nextPosition = pointA.position;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player = null;
            _nextPosition = pointB.position;
        }
    }
}
