using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHolder : MonoBehaviour
{
    #region Hearts
    [Header("HeartContainer")]
    public GameObject[] hearts;

    public int Health
    {
        get { return _currentPlayerHealth; }
        set { _currentPlayerHealth = value; }
    }

    #endregion

    #region PlayerStats
    private Player _player;
    [SerializeField]
    private int _currentPlayerHealth;
    private int _maxPlayerHealth;
    #endregion

    void Start()
    {
        _player = FindObjectOfType<Player>();
        _maxPlayerHealth = _player.GetHealth();
        _currentPlayerHealth = _maxPlayerHealth;
        UpdateHealth();
    }

    private void Update()
    {
        _currentPlayerHealth = _player.GetHealth();
    }

    public void UpdateHealth()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < _currentPlayerHealth)
            {
                hearts[i].SetActive(true);
            }
            else
            {
                hearts[i].SetActive(false);
            }
        }
    }
}
