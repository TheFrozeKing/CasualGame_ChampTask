using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Rocket : CarAbility
{
    [SerializeField] private GameObject _projectilePrefab;
    private float _timeOfLastSpawn;
    public override void Initialize()
    {        

    }
    
    public void SpawnProjectile()
    {
        if(Time.timeSinceLevelLoad - _timeOfLastSpawn < 1)
        {
            return;
        }
        _timeOfLastSpawn = Time.timeSinceLevelLoad;
        Instantiate(_projectilePrefab, transform.position + Vector3.right, Quaternion.Euler(0,0,-90));
    }


    
}
