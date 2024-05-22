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
    public bool _hasBeenTriggered;
    [SerializeField]
    private bool _closeRing;
    private bool _hitable;
    public bool _cantBeTarget;
    private bool _dead;
    public GameObject hitEffect;

    public GameObject _projectilePrefab;
    [SerializeField]
    private float _numberOfProjectiles;

    [SerializeField]
    private float _shootCD;
    [SerializeField]
    private float _radius;
    [SerializeField]
    private float _projectileSpeed;
    [SerializeField]
    private bool _isShooting;
    [SerializeField]
    private float _shootingRange;



    private ComboHolder _combo;
    private PlayerController _player;
    private Player _player_;
    private EnemyTrigger _enemyTrigger;
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
        _combo = FindObjectOfType<ComboHolder>();
        _player_ = FindObjectOfType<Player>();
        _enemyTrigger = FindObjectOfType<EnemyTrigger>();
        if (_canFollow)
        {
            _pois = FindObjectOfType<NavPointManager>().points;
            _currentDestination = _pois[randomPoint()];
        }
        _anim = GetComponent<Animator>();
        _player = FindObjectOfType<PlayerController>(includeInactive: true);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_dead)
        {
            _distance = Vector2.Distance(_player.gameObject.transform.position, transform.position);
            checkPlayerDistance();
            SetHitRing();

            if (!_canFollow)
            {
                if (inShootingRange() && !_isShooting)
                {
                    StartCoroutine(ShootCD());
                }
            }
            else
            {
                SetNewDestination();
                TravelToPoint();
            }
        }
    }

    private bool inShootingRange()
    {
        if (_distance <= _shootingRange)
        {
            return true;
        }

        return false;
    }

    private void Shoot()
    {
        Vector2 startPoint = gameObject.transform.position;

        float angleStep = 360f / _numberOfProjectiles;
        float angle = 0f;

        for (int i = 0; i <= _numberOfProjectiles - 1; i++)
        {
            float projectileDirXposition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * _radius;
            float projectileDirYposition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * _radius;

            Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
            Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * _projectileSpeed;

            var proj = Instantiate(_projectilePrefab, startPoint, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity =
                new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

            angle += angleStep;
        }
    }

    private IEnumerator ShootCD()
    {
        _isShooting = true;
        Shoot();
        yield return new WaitForSeconds(_shootCD);
        _isShooting = false;
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
            _cantBeTarget = true; ;
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
            _enemyTrigger.DeleteFromList(this.gameObject);
            hitRing.GetComponent<SpriteRenderer>().material = _far;
        }

        if (_hasBeenTriggered && !_closeRing)
        {
            //hitRing.transform.localScale = new Vector3(distance / 10, distance / 10, 1);
            StartCoroutine(CloseRing());
        }
    }

    public void TryToKill()
    {
        if (GameManager.instance.Bullets > 0)
        {
            GameManager.instance.DecreaseBullets(1);
            _player_.BulletDown();
            if (_hitable)
            {
                _combo.RandomizeString(false);
                GameManager.instance.IncreaseEnemies();
                GameObject explosion = Instantiate(hitEffect, transform);
                explosion.transform.position = transform.position;
                GameManager.instance.IncreaseCollectedCoins(1000);
                _dead = true;
                _hitable = false;
                GetComponent<Rigidbody2D>().gravityScale = 10;
                GetComponent<Collider2D>().enabled = false;
                return;
            } 
            
            _combo.RandomizeString(true);
        }
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && collision.name == "Player")
        {
            collision.GetComponent<Player>().ApplyDamage(AttackPower);
        }
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

    private void OnDestroy()
    {
        _enemyTrigger.DeleteFromList(this.gameObject);
    }
}
