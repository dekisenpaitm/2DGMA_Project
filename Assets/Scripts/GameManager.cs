using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
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

    [SerializeField]
    public bool _gameEnded;
    #endregion

    #region Referances
    private Player _player;
    private BulletHolder _bulletHolder;
    private CoinCounter _coinCounter;
    private HealthHolder _healthHolder;
    private GameEndMenu _end;
    #endregion

    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }

    public int MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    public int Bullets
    {
        get { return _bullets; }
        set { _bullets = value; }
    }

    public int MaxBullets
    {
        get { return _maxBullets; }
        set { _maxBullets = value; }
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
    void Awake()
    {
        Application.targetFrameRate = 61;
        //frick fricking webGL
        _end = FindObjectOfType<GameEndMenu>(includeInactive: true);
        _player = FindObjectOfType<Player>();
        _coinCounter = FindObjectOfType<CoinCounter>();
        _healthHolder = FindObjectOfType<HealthHolder>();
        _bulletHolder = FindObjectOfType<BulletHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_health <= 0)
        {
            _gameEnded = true;
            _end.finalPoints = _collectedCoins;
            _end.finalEnemies = _enemiesKilled;
            _end.gameObject.SetActive(true);
        }
    }
}