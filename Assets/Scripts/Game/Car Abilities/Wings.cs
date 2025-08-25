using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wings : CarAbility
{
    [SerializeField] private float _force = 5;
    private Rigidbody2D _master;
    private float _timeOfLastUse;
    public override void Initialize()
    {
        _master = transform.parent.GetComponent<Rigidbody2D>();
    }

    public void Use()
    {
        if(Time.timeSinceLevelLoad - _timeOfLastUse < 1)
        {
            return;
        }
        Debug.Log("Wings used!");
        _timeOfLastUse = Time.timeSinceLevelLoad;
        _master.AddForce(transform.up * _force, ForceMode2D.Impulse);

    }

}
