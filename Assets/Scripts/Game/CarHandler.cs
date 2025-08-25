using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] _carModels;
    [SerializeField] private Transform[] _abilities;
    [SerializeField] private GameObject[] _abilityButtons;
    [SerializeField] private AbilityFrameHandler _frameHandler;


    public void SpawnCar(int modelIndex, params bool[] abilityStates)
    {
        DespawnCar();
        Car car = Instantiate(_carModels[modelIndex], transform).GetComponent<Car>();
        
        List<Transform> abilitiesToAdd = new List<Transform>();
        for(int i = 0; i < abilityStates.Length; i++)
        {
            if (abilityStates[i])
            {
                abilitiesToAdd.Add(_abilities[i]);
            }
            _abilityButtons[i].SetActive(abilityStates[i]);
        }

        List<int> abilityIds = new();
        foreach(Transform ability in abilitiesToAdd)
        {
            abilityIds.Add(ability.GetComponent<AbilityDrag>().AbilityId);
        }
        foreach (var snapPoint in car.GetSnapPoints())
        {
            snapPoint.CheckIfShouldExist(abilityIds);
        }

        _frameHandler.SpawnFrames(abilitiesToAdd);
    }

    public void DespawnCar()
    {
        try
        {
            Destroy(transform.GetChild(0).gameObject);
            _frameHandler.SpawnFrames(new());
        }
        catch
        {

        }
    }
}
