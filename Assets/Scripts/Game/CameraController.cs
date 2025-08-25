using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Transform _toFollow;
    private Vector3 _startPosition;
    private void Awake()
    {
        _startPosition = transform.position;
    }

    public void ResetPosition()
    {
        _toFollow = null;
        transform.position = _startPosition;
    }

    public void FollowObject(Transform toFollow)
    {
        _toFollow = toFollow;
    }

    public void LateUpdate()
    {
        if (!_toFollow)
        {
            return;
        }
        transform.position = new Vector3(_toFollow.position.x - _startPosition.x, _toFollow.position.y + _startPosition.y, transform.position.z);
    }
}
