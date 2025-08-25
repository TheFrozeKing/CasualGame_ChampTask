using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketButton : MonoBehaviour
{
    private Rocket _rocket;

    private void OnEnable()
    {
        _rocket = FindObjectOfType<Rocket>();
    }

    public void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => SendRocket());
    }

    public void SendRocket()
    {
        _rocket.SpawnProjectile();
    }
}
