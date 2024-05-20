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
    [SerializeField]
    private float _triggerRange;
    [SerializeField]
    private float _idleTime;

    [Header("CanFollow?")]
    [SerializeField]
    private bool _canFollow;

    [Header("HitSetup")]
    [SerializeField]
    public Material _far;
    [SerializeField]
    public Material _close;
    public GameObject hitRing;
    [SerializeField]
    private float distance;
    [SerializeField]
    private bool _hasBeenTriggered;
    [SerializeField]
    private bool _closeRing;
    private bool _hitable;


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
        checkPlayerDistance();
        SetHitRing();

        _distance = Vector2.Distance(_player.gameObject.transform.position, transform.position);

        if (_canFollow)
        {
            if (inTriggerRange())
            {
                FollowPlayer();
            }
        }
        else
        {
            TravelToPoint();
        }
    }

    private void SetHitRing()
    {
        if(distance < _triggerRange && !_hasBeenTriggered)
        {
            _hasBeenTriggered = true;
            hitRing.SetActive(true);
        }

        if(hitRing.transform.localScale == new Vector3(0,0,1) && _hasBeenTriggered)
        {
            hitRing.SetActive(false);
        }

        if(hitRing.transform.localScale == new Vector3(0.8f, 0.8f, 1))
        {
            _hitable = true;
            hitRing.GetComponent<SpriteRenderer>().material = _close;
        }

        if(hitRing.transform.localScale == new Vector3(0.5f, 0.5f, 1))
        {
            _hitable = false;
            hitRing.GetComponent<SpriteRenderer>().material = _far;
        }

        if (_hasBeenTriggered && !_closeRing)
        {
            //hitRing.transform.localScale = new Vector3(distance / 10, distance / 10, 1);
            StartCoroutine(CloseRing());
        }
    }

    private void TryToHit()
    {
        //implement kill function here
    }

    private IEnumerator CloseRing()
    {
        _closeRing = true;
        while(hitRing.transform.localScale != new Vector3(0, 0, 1))
        {
            hitRing.transform.localScale += new Vector3(-0.01f,-0.01f, 0);
            yield return new WaitForSeconds(0.003f);
        }
    }

    private float checkPlayerDistance()
    {
        distance = Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(_player.transform.position.x, 0));
        return distance;
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
        yield return new WaitForSeconds(_idleTime);
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
        if (_distance <= _triggerRange)
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
