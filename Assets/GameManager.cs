using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    #region Attributes
    [SerializeField]
    private int _health;

    [SerializeField]
    private int _collectedCoins;
    #endregion

    #region Referances
    private CoinCounter _coinCounter;
    private HealthHolder _healthHolder;
    #endregion

    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }

    public void DecreaseHealth(int decreaseBy)
    {
        _health -= decreaseBy;
        UpdateHealthCounter();
    }

    public void IncreaseHealth(int increaseBy)
    {
        _health += increaseBy;
        UpdateHealthCounter();
    }

    public void UpdateHealthCounter()
    {
        _healthHolder.Health = _health;
        _healthHolder.UpdateHealth();
    }

    public int GetCollectedCoins() => _collectedCoins;

    public void DecreaseCollectedCoins(int decreaseBy)
    {
        _collectedCoins -= decreaseBy;
        UpdateCoinCounter();
    }
    public void IncreaseCollectedCoins(int increaseBy)
    {
        _collectedCoins += increaseBy;
        UpdateCoinCounter();
    }

    public void UpdateCoinCounter()
    {
        _coinCounter.Coins = _collectedCoins;
    }
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            return;
        }
        _coinCounter = FindObjectOfType<CoinCounter>();
        _healthHolder = FindObjectOfType<HealthHolder>();
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}