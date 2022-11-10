using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Evolution;

public class EnemyController : MonoBehaviour
{

    private GameObject _player;
    private NavMeshAgent _nav;
    private Canvas _healthBar;
    private Slider _healthBarSlider;
    private int _maxHealth;
    public int _health;
    private Evolvable _evolvable;
    private EnemyStageData StageData => _evolvable.Stage.EnemyData;
    private int _distance;
    private float _lastTimeShot = 0;


    [SerializeField] private float _firingSpeed;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _enemyFirePoint;
    [SerializeField] private int _minDistance = 10;


    private void Awake()
    {
        _nav = GetComponent<NavMeshAgent>();
        _player = GameObject.FindWithTag("Player");
        _healthBar = GetComponentInChildren<Canvas>();
        _healthBarSlider = _healthBar.GetComponentInChildren<Slider>();
        _evolvable = GetComponentInChildren<Evolvable>();
    }

    private void Start()
    {
        _health = StageData.Health;
        SetMaxHealth(_health);
        SetHealth(_health);

    }

    private void Update()
    {

        _healthBar.transform.rotation = Camera.main.transform.rotation;
        float _distance = Vector3.Distance(_player.transform.position, this.transform.position);
        this.transform.LookAt(_player.transform);

        if (_distance < _minDistance && _player.transform.hasChanged)
        {
            this._nav.SetDestination(_player.transform.position);
            Shoot();
        }

        if(_health <= 0)
        {
            Destroy(this.gameObject);
        }

       
    }


    public void SetMaxHealth(int maxHealth)
    {
        _maxHealth = maxHealth;
    }

    public void SetHealth(int health)
    {
        _healthBarSlider.value = (float)health / _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        SetHealth(_health);
    }

    public void Shoot()
    {
        if (_lastTimeShot + _firingSpeed < Time.time)
        {
            _lastTimeShot = Time.time;
            Instantiate(_projectile, _enemyFirePoint.position, _enemyFirePoint.rotation);
        }
    }


}
