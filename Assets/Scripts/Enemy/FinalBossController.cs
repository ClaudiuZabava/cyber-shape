using System.Collections;
using Evolution;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Enemy
{
    public class FinalBossController : MonoBehaviour
    {
        [SerializeField] private int minDistance = 500;
        [SerializeField] private GameObject enemyBody;
        [SerializeField] private GameObject projectile;
        [SerializeField] private int projectileCountPerAttack = 6;
        // Time between attacks in milliseconds
        [SerializeField] private int timeBetweenAttacks = 5 * 1000;

        private static readonly int MAX_ROLL_SPEED = 8;
        private static readonly int MAX_NAV_SPEED = 2;        private static readonly int MAX_HP = 100;
        private static readonly int SCORE = 1000;

        private NavMeshAgent _nav;
        private Canvas _healthBar;
        private Slider _healthBarSlider;
        private GameObject _player;
        private float _rollSpeed;
        private Rigidbody _rigidbody;
        private Camera _camera;
        private int _lastAttackTime = 0;
        private bool _isTelegraphing = false;
        private Animator _animator;

        public int health = MAX_HP;
        private static readonly int TelegraphAttackAnimTrig = Animator.StringToHash("TelegraphAttack");

        private void Start()
        {
            _rollSpeed = MAX_ROLL_SPEED;
            _nav.speed = MAX_NAV_SPEED;
        }
        
        private void Awake()
        {
            _nav = GetComponent<NavMeshAgent>();
            _player = GameObject.FindWithTag("Player");
            _healthBar = GetComponentInChildren<Canvas>();
            _healthBarSlider = _healthBar.GetComponentInChildren<Slider>();
            _rigidbody = enemyBody.GetComponent<Rigidbody>();
            _camera = Camera.main;
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _healthBar.transform.rotation = _camera.transform.rotation;
            var distance = Vector3.Distance(_player.transform.position, transform.position);
            transform.LookAt(_player.transform);
            
            if (_player.transform.hasChanged)
            {
                var destination = _player.transform.position;
                // var direction = destination - enemyBody.transform.position;
                var rot = destination - enemyBody.transform.position;
                rot.y = 0;
                
                if (destination == enemyBody.transform.position)
                {
                    _rollSpeed = 0;
                }
                else
                {
                    _rollSpeed = MAX_ROLL_SPEED;
                    _nav.speed = MAX_NAV_SPEED;
                }
                // _rigidbody.AddForce(direction.normalized * _rollSpeed);
                 _rigidbody.AddTorque(new Vector3(rot.x / 2, 0, rot.z / 2) * _rollSpeed);
                _nav.SetDestination(destination);
                HandleShooting();
            }

            if (health <= 0)
            {
                // TODO: Add death animation
                Destroy(gameObject);
            }
        }

        private void SetHealth(int health)
        {
            _healthBarSlider.value = (float)health / MAX_HP;
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            SetHealth(health);
            if (health <= 0)
            {
                _player.GetComponent<Player>().AddScore(SCORE);
            }
        }

        private void HandleShooting()
        {
            if (Time.time * 1000 - _lastAttackTime < timeBetweenAttacks || _isTelegraphing) return;
            TelegraphAttack();
        }

        private void TelegraphAttack()
        {
            _animator.SetTrigger(TelegraphAttackAnimTrig);
            _isTelegraphing = true;
        }
        
        public void Shoot()
        {
            // check if it's time to shoot
            // Instantiate projectileCountPerAttack projectiles in a circle around the enemy
            var angle = 360 / projectileCountPerAttack;
            for (var i = 0; i < projectileCountPerAttack; i++)
            {
                var projectileRotation = Quaternion.Euler(0, angle * i, 0);
                var projectilePosition = transform.position + projectileRotation * Vector3.forward * 2;
                Instantiate(projectile, projectilePosition, projectileRotation);
            }
            _lastAttackTime = (int) (Time.time * 1000);
            _isTelegraphing = false;
        }
    }
}