
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Events;

public class AdPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _adProcess;
    [SerializeField] private MoneyUI _moneyUI;
    [SerializeField] private UnityEvent _afterAdPlayed;
    private Coroutine _adCoroutine;

    public void StartAdProcess()
    {
        _adCoroutine = StartCoroutine(AdProcess());
    }

    public void StopAd()
    {
        try
        {
            StopCoroutine(_adCoroutine);
            _adProcess.SetActive(false);
        }
        catch
        {

        }
    }
    public IEnumerator AdProcess()
    {
        _adProcess.SetActive(true);
        yield return new WaitForSeconds(7);
        _adProcess.SetActive(false);
        _afterAdPlayed.Invoke();
    }

}
