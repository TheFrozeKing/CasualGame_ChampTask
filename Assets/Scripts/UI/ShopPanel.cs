using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private MoneyUI _moneyUI;
    [SerializeField] private Transform _buyProcess;
    [SerializeField] private GameObject _adProcess;
    [SerializeField] private Transform _coinPrefab;
    [SerializeField] private Transform _flyingCoinHolder;
    
    [Space]
    [SerializeField] private AudioClip _purchasedSound;
    [SerializeField] private AudioClip _moneyGotSound;
    private AudioObject _audioPlayer;
    private ShopItem _chosenItem;
    private Coroutine _adCoroutine;
    private void Start()
    {
        _audioPlayer = FindObjectOfType<AudioObject>();
    }

    public void Buy(ShopItem itemToBuy)
    {
        _chosenItem = itemToBuy;
        _chosenItem.TryBuy();
    }

    private void OnDisable()
    {
        for(int i = 0; i < _flyingCoinHolder.childCount; i++)
        {
            Destroy(_flyingCoinHolder.GetChild(i).gameObject);
        }
    }


    public void ProcessPurchase()
    {
        _buyProcess.gameObject.SetActive(true);
        Invoke(nameof(AfterPurchaseProcessed), 2);
    }

    public void AfterPurchaseProcessed()
    {
        _buyProcess.gameObject.SetActive(false);
        _moneyUI.AddMoney(_chosenItem.Reward);
        StartCoroutine(CoinVisual(_chosenItem.transform.GetChild(0)));
        _audioPlayer.Play(_purchasedSound);
    }
    
    public void StartAdProcess()
    {
        _adCoroutine = StartCoroutine(AdProcess());    
    }

    public void StopAd()
    {
        try
        {
            _chosenItem = null;
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
        _moneyUI.AddMoney(_chosenItem.Reward);
        StartCoroutine(CoinVisual(_chosenItem.transform));
    }

    private IEnumerator CoinVisual(Transform flyFrom)
    {
        List<Transform> _flyingCoins = new();

        for(int i = 0; i < 20; i++)
        {
            _flyingCoins.Add(Instantiate(_coinPrefab, flyFrom.position + ((Vector3)Random.insideUnitCircle * 120), Quaternion.identity, _flyingCoinHolder));
            yield return new WaitForSeconds(0.001f);
        }

        bool allCoinsReached = false;
        while (!allCoinsReached)
        {
            foreach(Transform coin in _flyingCoins)
            {
                coin.position = Vector3.MoveTowards(coin.position, _moneyUI.transform.position, 2000 * Time.deltaTime);
                if(coin.position == _moneyUI.transform.position)
                {
                    allCoinsReached = true;
                    continue;
                }
                allCoinsReached = false;
            }
            yield return null;
        }

        foreach(Transform coin in _flyingCoins)
        {
            Destroy(coin.gameObject);
        }
        _flyingCoins.Clear();
        _audioPlayer.Play(_moneyGotSound);
    }
}
