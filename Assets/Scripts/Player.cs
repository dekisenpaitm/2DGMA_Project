using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region PlayerStats
    [SerializeField]
    private string playerName;

    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private int currentHealth;

    [SerializeField]
    private int maxBullets;

    [SerializeField]
    private int currentBullets;

    [SerializeField]
    private int damage;
    #endregion

    #region DamageCheck
    private bool _hit;
    public Collider2D _hitBox;
    private AudioSource _hitSound;
    #endregion

    #region Referances
    private GameManager _gameMan;
    private Rigidbody _rb;
    private Animator _anim;
    #endregion

    private void OnEnable()
    {
        _gameMan = FindObjectOfType<GameManager>();
        _hitSound = GetComponent<AudioSource>();
        currentBullets = maxBullets;
        currentHealth = maxHealth;
        _gameMan.Health = maxHealth;
        _gameMan.Bullets = maxBullets;
        _gameMan.MaxHealth = maxHealth;
        _gameMan.MaxBullets = maxBullets;
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    public void HealthUp()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth++;
        }
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public void BulletUp()
    {
        if (currentBullets < maxBullets)
        {
            currentBullets++;
        }
    }

    public void BulletDown()
    {
        if (currentBullets > 0)
        {
            currentBullets--;
        }
    }

    public int GetBullets()
    {
        return maxBullets;
    }

    public int GetDamageValue()
    {
        return damage;
    }

    public void ApplyDamage(int damage)
    {
        Damage(damage);
    }

    private void Damage(int damage)
    {
        if (!_hit && currentHealth > 0)
        {
            currentHealth -= damage;
            StartHit();
            _gameMan.DecreaseHealth(damage);
            if (currentHealth <= 0)
            {
                //Destroy(this);
            }
        }
    }

    private void StartHit()
    {
        StartCoroutine(GotDamage());
    }

    public IEnumerator GotDamage()
    {
        _hitSound.Play();
        _hit = true;
        _anim.Play("Player_Hit");
        yield return new WaitForSeconds(0.3f);
        _hit = false;
    }
}
