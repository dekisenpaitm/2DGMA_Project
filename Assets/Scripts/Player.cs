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
    #endregion

    #region Referances
    private Rigidbody _rb;
    #endregion

    private void Start()
    {
        currentBullets = maxBullets;
        currentHealth = maxHealth;
        GameManager.instance.Health = maxHealth;
        GameManager.instance.Bullets = maxBullets;
        _rb = GetComponent<Rigidbody>();
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
            GameManager.instance.DecreaseHealth(damage);
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
        _hit = true;
        yield return new WaitForSeconds(0.3f);
        _hit = false;
    }
}
