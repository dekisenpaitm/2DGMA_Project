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

    private PlayerController _player;
    private Transform[] _pois;
    private Transform _currentDestination;
    private Animator _anim;
    private float _distance;
    private bool _isIdling;

    public bool isMelee
    {
        get { return _isMelee; }
        set { _isMelee = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _pois = FindObjectOfType<NavPointManager>().points;
        _anim = GetComponent<Animator>();
        _player = FindObjectOfType<PlayerController>(includeInactive: true);
        _currentDestination = _pois[randomPoint()];
    }

    // Update is called once per frame
    void Update()
    {

        SetNewDestination();

        _distance = Vector2.Distance(_player.gameObject.transform.position, transform.position);

        if (inTriggerRange())
        {
            FollowPlayer();
        } 
        else if (inAttackRange())
        {
            AttackPlayer();
        }
        else if (!inTriggerRange())
        {
            TravelToPoint();
        }
    }

    private void SetNewDestination()
    {
        float distanceToDestination = Vector2.Distance(_currentDestination.position, transform.position);
        if (distanceToDestination < 1f && !_isIdling)
        {
            StartCoroutine(reachedLocation());
        }
    }

    private IEnumerator reachedLocation()
    {
        _isIdling = true;
        //_anim.Play("Idle");
        yield return new WaitForSeconds(2f);
        _currentDestination = _pois[randomPoint()];
        _isIdling = false;
    }

    private int randomPoint()
    {
        return Random.Range(0, _pois.Length);
    }

    private void FollowTarget(Transform target)
    {
        var step = _speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.position, step);
    }

    private void TravelToPoint()
    {
        FollowTarget(_currentDestination);
    }

    private bool inTriggerRange()
    {
        if (_distance <= 10f && _distance >= 1f)
        {
            return true;
        }

        return false;
            
    }

    private void FollowPlayer()
    {
        _isIdling = false;
        //_anim.Play("Move");
        FollowTarget(_player.transform);
    }

    private bool inAttackRange()
    {
        if (_distance <= 1f)
        {
            return true;
        }

        return false;
    }

    private void AttackPlayer()
    {
        _isIdling = false;
        //_anim.Play("Attack");
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
