using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpikeButton : MonoBehaviour
{
    private Wheel[] _spikedWheels = new Wheel[1];

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => ToggleSpikes());
    }   
    private void OnEnable()
    {
        _spikedWheels = FindObjectsByType<Wheel>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
    }
    private void ToggleSpikes()
    {
        foreach(var spike in _spikedWheels)
        {
            spike.IsSpiked = !spike.IsSpiked;
        }
    }
}
