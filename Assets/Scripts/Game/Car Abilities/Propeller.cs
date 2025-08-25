using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Propeller : CarAbility
{
    [SerializeField] private float _speed = 10;
    private Wheel[] _wheels;
    public override void Initialize()
    {
        _wheels = transform.parent.GetComponentsInChildren<Wheel>();
    }

    public void Use()
    {
        foreach(Wheel wheel in _wheels)
        {
            wheel.Speed = _speed;
        }
    }

    public void StopUsing()
    {
        foreach (Wheel wheel in _wheels)
        {
            wheel.Speed = 5;
        }
    }

}
