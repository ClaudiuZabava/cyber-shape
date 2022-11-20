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
        [SerializeField] private int minDistance = 10;
        
        private GameObject _player;
        private NavMeshAgent _nav;
        private Canvas _healthBar;
        private Slider _healthBarSlider;
        private int _maxHealth;
        public int health;
        private Evolvable _evolvable;
        private EnemyStageData StageData => _evolvable.Stage.EnemyData;
        private int _distance;
        private float _lastTimeShot = 0;
        
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
            health = StageData.Health;
            SetMaxHealth(health);
            SetHealth(health);
        }

        private void Update()
        {
            _healthBar.transform.rotation = Camera.main.transform.rotation;
            var distance = Vector3.Distance(_player.transform.position, this.transform.position);
            transform.LookAt(_player.transform);

            if (distance < minDistance && _player.transform.hasChanged)
            {
                _nav.SetDestination(_player.transform.position);
                Shoot();
            }

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void SetMaxHealth(int maxHealth)
        {
            _maxHealth = maxHealth;
        }

        private void SetHealth(int health)
        {
            _healthBarSlider.value = (float)health / _maxHealth;
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            SetHealth(health);
            if(health <=0)
                _player.GetComponent<Player>().AddScore(1);
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