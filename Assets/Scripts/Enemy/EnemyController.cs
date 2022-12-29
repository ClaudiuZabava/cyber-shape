using Evolution;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float firingSpeed;
        [SerializeField] private GameObject projectile;
        [SerializeField] private Transform enemyFirePoint;
        [SerializeField] private int minDistance = 20;
        [SerializeField] private GameObject enemyBody;
        
        public EnemyStageData StageData => _evolvable.Stage.EnemyData;

        private GameObject _player;
        private Rigidbody _rigidbody;
        private NavMeshAgent _nav;
        private Canvas _healthBar;
        private Slider _healthBarSlider;
        private float _maxHealth;
        public float health;
        private Evolvable _evolvable;
        private int _distance;
        private float _lastTimeShot;
        private Camera _camera;
        private float _rollSpeed;

        private void Awake()
        {
            _nav = GetComponent<NavMeshAgent>();
            _player = GameObject.FindWithTag("Player");
            _healthBar = GetComponentInChildren<Canvas>();
            _healthBarSlider = _healthBar.GetComponentInChildren<Slider>();
            _evolvable = GetComponentInChildren<Evolvable>();
            _rigidbody = enemyBody.GetComponent<Rigidbody>();
            _camera = Camera.main;
        }

        private void Start()
        {
            health = StageData.Health;
            SetMaxHealth(health);
            SetHealth(health);

            _rollSpeed = StageData.RollSpeed;
            _nav.speed = StageData.NavSpeed;
        }

        private void Update()
        {
            _healthBar.transform.rotation = _camera.transform.rotation;
            var distance = Vector3.Distance(_player.transform.position, transform.position);
            transform.LookAt(_player.transform);
            
            if (_player.transform.hasChanged)
            {
                var destination = _player.transform.position;
                var rotX = destination[0] - enemyBody.transform.position.x;
                var rotZ = destination[2] - enemyBody.transform.position.z;
                if (destination == enemyBody.transform.position)
                {
                    _rollSpeed = 0;
                }
                else
                {
                    _rollSpeed = StageData.RollSpeed;
                    _nav.speed = StageData.NavSpeed;
                }
                _rigidbody.AddTorque(new Vector3(rotX / 2, 0, rotZ / 2) * _rollSpeed);
                _nav.SetDestination(destination);
            }

            if (distance < minDistance)
            {
                Shoot();
            }

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void SetMaxHealth(float maxHealth)
        {
            _maxHealth = maxHealth;
        }

        private void SetHealth(float health)
        {
            _healthBarSlider.value = health / _maxHealth;
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            SetHealth(health);
            if (health <= 0)
            {
                _player.GetComponent<Player>().AddScore(1);
            }
        }

        private void Shoot()
        {
            if (_lastTimeShot + firingSpeed < Time.time)
            {
                _lastTimeShot = Time.time;
                Instantiate(projectile, enemyFirePoint.position, enemyFirePoint.rotation);
            }
        }
    }
}