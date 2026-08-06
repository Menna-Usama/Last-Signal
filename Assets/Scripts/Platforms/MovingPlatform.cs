using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 moveOffset = new Vector3(3f, 0f, 0f); // how far, and in what direction, to travel
    public float moveSpeed = 2f;

    private Vector3 _pointA;
    private Vector3 _pointB;
    private Vector3 _nextPosition;

    private Vector3 _lastPosition;
    private Transform _player;

    void Start()
    {
        _pointA = transform.position;
        _pointB = transform.position + moveOffset;
        _nextPosition = _pointB;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _nextPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _nextPosition) < 0.01f)
        {
            _nextPosition = (_nextPosition == _pointA) ? _pointB : _pointA;
        }
    }

    private void LateUpdate()
    {
        Vector3 delta = transform.position - _lastPosition;
        if (_player != null)
        {
            _player.position += delta;
        }

        _lastPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player = null;
        }
    }
}