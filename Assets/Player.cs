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
    private int damage;
    #endregion

    #region DamageCheck
    private bool _hit;
    #endregion

    #region Referances
    private Rigidbody _rb;
    #endregion

    private void Start()
    {
        currentHealth = maxHealth;
        GameManager.instance.Health = maxHealth;
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

    public int GetDamageValue()
    {
        return damage;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            ApplyDamage(collision.gameObject.GetComponent<Enemy>().AttackPower);
        }
    }

    public void ApplyDamage(int damage)
    {
        Damage(damage);
    }

    private void Damage(int damage)
    {
        if (!_hit && currentHealth > 0)
        {
            Debug.Log("I got hit with " + damage);
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
