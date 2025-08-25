using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WingsButton : MonoBehaviour
{
    private Wings _wings;
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => UseWings());
    }
    private void OnEnable()
    {
        _wings = FindObjectOfType<Wings>();
    }

    public void UseWings()
    {
        _wings.Use();
    }

}
