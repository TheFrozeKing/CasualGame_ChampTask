using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RocketProjectile : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private void Awake()
    {
        Rigidbody rigidbody = transform.AddComponent<Rigidbody>();
        rigidbody.freezeRotation = true;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationY;
        _rigidbody = rigidbody;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.attachedRigidbody)
        {
            return;
        }
        Debug.Log("collided");
        other.attachedRigidbody.isKinematic = false;
        other.attachedRigidbody.AddForce(Random.insideUnitSphere * 15, ForceMode.Impulse);
        Invoke(nameof(SelfDestruct), 2);

    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = Vector3.right * 10;
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
