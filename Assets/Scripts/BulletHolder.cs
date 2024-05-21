using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHolder : MonoBehaviour
{
    #region Bullets
    [Header("BulletContainer")]
    public GameObject[] bullets;

    public int Bullets
    {
        get { return _currentPlayerBullets; }
        set { _currentPlayerBullets = value; }
    }

    #endregion

    #region PlayerStats
    private Player _player;
    [SerializeField]
    private int _currentPlayerBullets;
    private int _maxPlayerBullets;
    #endregion

    void Awake()
    {
        _player = FindObjectOfType<Player>();
        _maxPlayerBullets = _player.GetBullets();
        _currentPlayerBullets = _maxPlayerBullets;
        UpdateBullets();
    }

    public void UpdateBullets()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            if (i < _currentPlayerBullets)
            {
                bullets[i].SetActive(true);
            }
            else
            {
                bullets[i].SetActive(false);
            }
        }
    }
}
