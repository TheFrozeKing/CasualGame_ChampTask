using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Car : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private List<SnapPoint> _snapPoints = new();
    [SerializeField] private List<Transform> _abilityPrefabs = new();
    private List<CarAbility> _abilities = new();

    [Header("Movement")]
    private List<Wheel> _wheels = new();


    #region Setup
    public void RemoveFromSnapPoints(SnapPoint snapPoint)
    {
        _snapPoints.Remove(snapPoint);
        if(_snapPoints.Count < 1)
        {
            FindObjectOfType<GameLevelManager>().ShowStartButton();
        }
    }

    public List<SnapPoint> GetSnapPoints() => new(_snapPoints);

    public void SpawnAbility(int id, Transform snapTo)
    {
        Transform newAbility = Instantiate(_abilityPrefabs[id], transform);
        newAbility.localPosition = new Vector3(snapTo.localPosition.x, snapTo.localPosition.y, 0);
        _abilities.Add(newAbility.GetComponent<CarAbility>());
        if (id < 2)
        {
            newAbility.transform.localScale = Vector3.one * 0.85f;
            _wheels.Add(newAbility.GetComponent<Wheel>());
        }
    }

    #endregion

    #region Movement

    public void StartMovement()
    {
        foreach(var carAbility in _abilities)
        {
            carAbility.Initialize();
        }
        GetComponent<Rigidbody2D>().simulated = true;
    }

    #endregion
}
