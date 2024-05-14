using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("EnemyHealth")]
    [SerializeField]
    private string _name;

    [Header("Health")]
    [SerializeField]
    private int _maxHealth;
    private int _health;
    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }

    [Header("Speed")]
    [SerializeField]
    private int _speed;
    public int Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    [Header("AttackPower")]
    [SerializeField]
    private int _attackPower;

    public int AttackPower
    {
        get { return _attackPower; }
        set { _attackPower = value; }
    }

    [Header("Melee/Ranged")]
    [SerializeField]
    private bool _isMelee;

    public bool isMelee
    {
        get { return _isMelee; }
        set { _isMelee = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool inTriggerRange()
    {
        //implement in range check
        return true;
    }

    private bool inAttackRange()
    {
        //implement in range check
        return true;
    }

    private void FollowPlayer()
    {
        //implement followPlayer Mechanic
    }

    private void AttackPlayer()
    {
        //implement check if in range
        //implement attack player -> CD
    }

    private void TakeDamage(int dmgValue)
    {
        if(_health - dmgValue <= 0)
        {
            //implement deathRoutine
            return;
        }

        _health = _health -= dmgValue;
    }

    private void OnDeath()
    {

    }
}
