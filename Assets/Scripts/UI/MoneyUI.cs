using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private Text _moneyText;
    [SerializeField] private UserDataHandler _dataHandler;
    public int CurrentCoinAmount
    {
        get { return _dataHandler.Data.CoinAmount; }
        set 
        {
            _dataHandler.Data.CoinAmount = value;
            CoinAmountChanged?.Invoke();
        }
    }

    public event Action CoinAmountChanged;

    private void Start()
    {
        UpdateMoney();
    }
    public void AddMoney(int amount)
    {
        CurrentCoinAmount += amount;
        UpdateMoney();
    }


    public void UpdateMoney()
    {
        if(CurrentCoinAmount >= 1000)
        {
            _moneyText.text = $"{(float)CurrentCoinAmount / (float)1000.0:F1}k".Replace(".0", "");
            return;
        }
        _moneyText.text = CurrentCoinAmount.ToString();
    }
}
