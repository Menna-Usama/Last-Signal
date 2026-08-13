using UnityEngine;

public class FallingPlatformCopy : MonoBehaviour
{
    [SerializeField] private Vector3 fallOffset = new Vector3(0f,-2f, 0f);
    public float moveSpeed = 2f;


    private Vector3 _topPos;
    private Vector3 _bottomPos;
    private Vector3 _nextPosition;
    private Vector3 _lastPosition;

    private Transform _player;

    
    private void OnEnable()
    {
        //each time ApplyState gets called on falling, positions are recalculated
        _topPos = transform.position;
        _bottomPos = transform.position + fallOffset;
        _nextPosition = _topPos;
        _lastPosition = transform.position;
        _player = null;
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
            _nextPosition = _bottomPos;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player = null;
            _nextPosition = _topPos;
        }
    }
}


