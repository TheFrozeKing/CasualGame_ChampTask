using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text _timerText;
    [SerializeField] private int _startTime;
    [SerializeField] private string _format;
    [SerializeField] private UnityEvent _onZeroReached;

    private void OnEnable()
    {
        StartTicking();
    }

    public void StartTicking() 
    { 
        StopAllCoroutines();
        StartCoroutine(Tick());
    }

    private IEnumerator Tick()
    {
        int currentTime = _startTime;
        _timerText.text = _format + currentTime.ToString();

        while(currentTime > 0)
        {
            yield return new WaitForSeconds(1);
            currentTime--;
            _timerText.text = _format + currentTime.ToString();
        }
        _onZeroReached.Invoke();
    }
}
