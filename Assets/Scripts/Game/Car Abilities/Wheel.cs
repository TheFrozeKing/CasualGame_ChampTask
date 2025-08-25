using Unity.VisualScripting;
using UnityEngine;

public class Wheel : CarAbility
{
    [field: SerializeField] public float Speed { get; set; }
    [SerializeField] private LayerMask _groundLayer;
    private Rigidbody2D _master;
    private WheelJoint2D _joint;
    private CircleCollider2D _collider;
    public bool IsSpiked { get; set; }

    private bool _isInitialized;

    public override void Initialize()
    {
        GetComponent<Rigidbody2D>().simulated = true;
        _collider = GetComponent<CircleCollider2D>();
        _joint = GetComponent<WheelJoint2D>();
        _joint.anchor = Vector3.zero;
        _joint.connectedAnchor = transform.localPosition;
        _master = transform.parent.GetComponent<Rigidbody2D>();
        _joint.connectedBody = _master;
        _isInitialized = true;
    }

    protected virtual void FixedUpdate()    
    {
        if(!_isInitialized)
        {
            return;
        }


        if (Physics2D.IsTouchingLayers(_collider, _groundLayer))
        {
            _master.AddForce(transform.parent.right * Speed);
        }

        if (IsSpiked)
        {
            if (Physics2D.OverlapCircle(transform.position, _collider.radius * 1.6f, _groundLayer))
            {
                _joint.connectedBody.AddForceAtPosition(-transform.parent.up * (10 + _joint.connectedBody.velocity.sqrMagnitude * 0.5f), transform.position);
            }
        }

        var suspension = _joint.suspension;

        suspension.dampingRatio = Mathf.Clamp01(Mathf.InverseLerp(0, 200, _master.velocity.sqrMagnitude)) * 0.3f + 0.7f;
        suspension.frequency = Mathf.Clamp01(Mathf.InverseLerp(0, 200, _master.velocity.sqrMagnitude)) * 100 + 20;

        _joint.suspension = suspension;
    }
}
