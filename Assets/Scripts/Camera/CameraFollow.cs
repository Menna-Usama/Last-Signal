using UnityEngine;

public class CameraFollowY : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform _player;

    [Header("Horizontal")]
    [SerializeField] private float _fixedX;      // the horizontal center point the camera stays locked to
    [SerializeField] private bool _useStartXAsCenter = true; // if true, grabs camera's own starting X instead of _fixedX

    [Header("Vertical Follow")]
    [SerializeField] private float _yOffset = 0f;
    [SerializeField] private bool _smoothFollow = true;
    [SerializeField] private float _smoothTime = 0.15f;

    [Header("Optional Clamping")]
    [SerializeField] private bool _useYClamp = false;
    [SerializeField] private float _minY = -10f;
    [SerializeField] private float _maxY = 10f;

    private float _fixedZ;
    private Vector3 _velocity = Vector3.zero;

    void Start()
    {
        if (_useStartXAsCenter)
        {
            _fixedX = transform.position.x;
        }
        _fixedZ = transform.position.z; // keep whatever Z the camera already has (e.g. -10 for 2D)
    }

    void LateUpdate()
    {
        if (_player == null) return;

        float targetY = _player.position.y + _yOffset;

        if (_useYClamp)
        {
            targetY = Mathf.Clamp(targetY, _minY, _maxY);
        }

        Vector3 targetPos = new Vector3(_fixedX, targetY, _fixedZ);

        if (_smoothFollow)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref _velocity, _smoothTime);
        }
        else
        {
            transform.position = targetPos;
        }
    }
}