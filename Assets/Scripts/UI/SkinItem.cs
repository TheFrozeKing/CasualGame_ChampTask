using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkinItem : MonoBehaviour
{ 
    [SerializeField] private int _cost;
    [SerializeField] private Text _costText;
    [SerializeField] private UnityEvent _onBuy;
    private MoneyUI _moneyUI;
    private void Start()
    {
        _moneyUI = FindObjectOfType<MoneyUI>();
        _moneyUI.CoinAmountChanged += UpdateCostColor;
        UpdateCostColor();
    }

    private void UpdateCostColor()
    {
        _costText.color = _moneyUI.CurrentCoinAmount < _cost ? Color.red : Color.white;
    }
    public void TryBuy()
    {
        if(_moneyUI.CurrentCoinAmount < _cost)
        {
            return;
        }
        _moneyUI.AddMoney(-_cost);
        _onBuy.Invoke();
        _moneyUI.CoinAmountChanged -= UpdateCostColor;
        Destroy(this);
    }


}
