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

    #region Referances
    private Player _player;
    #endregion

    void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_collectibleType == CollectibleType.coin)
            {
                GameManager.instance.IncreaseCollectedCoins(100);
                Destroy(gameObject);
            }

            if (_collectibleType == CollectibleType.heart)
            {
                GameManager.instance.IncreaseHealth(1);
                _player.HealthUp();
                Destroy(gameObject);
            }

            if(_collectibleType == CollectibleType.blade)
            {
                GameManager.instance.IncreaseBullets(1);
                _player.BulletUp();
                Destroy(gameObject);
            }
        }
    }

}

