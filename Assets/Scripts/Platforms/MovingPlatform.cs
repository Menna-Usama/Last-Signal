using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 moveOffset = new Vector3(3f, 0f, 0f); // how far, and in what direction, to travel
    public float moveSpeed = 2f;

    private Vector3 _pointA;
    private Vector3 _pointB;
    private Vector3 _nextPosition;
    private bool _isInitialized = false;

    private Vector3 _lastPosition;
    private Transform _player;

    void Start()
    {
        _pointA = transform.position;
        _pointB = transform.position + moveOffset;
        _nextPosition = _pointB;
    }

    void OnEnable()//so they snap back to the original position each time instead of starting where they were last
    {
        float distanceToA = Vector3.Distance(transform.position, _pointA);
        float distanceToB = Vector3.Distance(transform.position, _pointB);
        _nextPosition = (distanceToA < distanceToB) ? _pointB : _pointA;
        //if you're closer to A go to B, and vice versa. To not move out of the the offset
    }

    public void InOrigin(Vector3 origin)
    {
        if (_isInitialized) return;
        _pointA = origin;
        _pointB = origin + moveOffset;
        _isInitialized = true;

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