using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectibleType
{
    heart,
    coin,
    blade
}

public class Coin : MonoBehaviour
{

    [Header("TypeOfCollectible")]
    [SerializeField]
    private CollectibleType _collectibleType;
    [Header("Audio_Effect")]
    public GameObject _audio;

    #region Referances
    private GameManager _gamMan;
    private Player _player;
    #endregion

    void Start()
    {
        _gamMan = FindObjectOfType<GameManager>();
        _player = FindObjectOfType<Player>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_audio != null)
            {
                GameObject audio = Instantiate(_audio);
            }

            if (_collectibleType == CollectibleType.coin)
            {
                _gamMan.IncreaseCollectedCoins(100);
                Destroy(gameObject);

            }

            if (_collectibleType == CollectibleType.heart)
            {
                _gamMan.IncreaseHealth(1);
                _player.HealthUp();
                Destroy(gameObject);
            }

            if(_collectibleType == CollectibleType.blade)
            {
                _gamMan.IncreaseBullets(1);
                _player.BulletUp();
                Destroy(gameObject);
            }
        }
    }
}

