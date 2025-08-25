using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private UnityEvent _onBuy;
    [field: SerializeField] public int Reward { get; private set; }
    [SerializeField] private float _cooldown;
    private float _timeOfLastPurchase;

    public void TryBuy()
    {
        if(Time.timeSinceLevelLoad - _timeOfLastPurchase < _cooldown)
        {
            return;
        }
        _timeOfLastPurchase = Time.timeSinceLevelLoad;
        _onBuy.Invoke();
    }
}
