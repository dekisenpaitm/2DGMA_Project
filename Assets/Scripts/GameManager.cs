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
    private int _maxHealth;
    private int _health;

    [SerializeField]
    private int _collectedCoins;

    [SerializeField]
    private int _maxBullets;
    private int _bullets;

    [SerializeField]
    private int _enemiesKilled;
    #endregion

    #region Referances
    private BulletHolder _bulletHolder;
    private CoinCounter _coinCounter;
    private HealthHolder _healthHolder;
    #endregion

    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }

    public int Bullets
    {
        get { return _bullets; }
        set { _bullets = value; }
    }

    public int Enemies
    {
        get { return _enemiesKilled; }
        set { _enemiesKilled = value; }
    }

    public void IncreaseEnemies()
    {
        _enemiesKilled ++;
    }

    public void DecreaseHealth(int decreaseBy)
    {
        if (_health > 0)
        {
            _health -= decreaseBy;
            UpdateHealthCounter();
        }
    }

    public void IncreaseHealth(int increaseBy)
    {
        if (_health < _maxHealth)
        {
            _health += increaseBy;
            UpdateHealthCounter();
        }
    }

    public void DecreaseBullets(int decreaseBy)
    {
        if (_bullets > 0)
        {
            _bullets -= decreaseBy;
            UpdateBulletsCounter();
        }
    }

    public void IncreaseBullets(int increaseBy)
    {
        if (_bullets < _maxBullets)
        {
            _bullets += increaseBy;
            UpdateBulletsCounter();
        }
    }

    public void UpdateHealthCounter()
    {
        _healthHolder.Health = _health;
        _healthHolder.UpdateHealth();
    }

    public void UpdateBulletsCounter()
    {
        _bulletHolder.Bullets = _bullets;
        _bulletHolder.UpdateBullets();
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
        _bulletHolder = FindObjectOfType<BulletHolder>();
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        _maxBullets = _bullets;
        _maxHealth = _health;
        UpdateHealthCounter();
        UpdateBulletsCounter();
    }

    // Update is called once per frame
    void Update()
    {

    }
}