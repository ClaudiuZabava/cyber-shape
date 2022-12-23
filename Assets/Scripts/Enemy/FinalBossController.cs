using Evolution;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Enemy
{
    public class FinalBossController : MonoBehaviour
    {
        [SerializeField] private int minDistance = 10;
        [SerializeField] private GameObject enemyBody;

        private static readonly int MAX_ROLL_SPEED = 8;
        private static readonly int MAX_HP = 100;
        private static readonly int SCORE = 1000;

        private NavMeshAgent _nav;
        private Canvas _healthBar;
        private Slider _healthBarSlider;
        private GameObject _player;
        private float _rollSpeed;
        private Rigidbody _rigidbody;
        private Camera _camera;

        public int health = MAX_HP;

        private void Awake()
        {
            _nav = GetComponent<NavMeshAgent>();
            _player = GameObject.FindWithTag("Player");
            _healthBar = GetComponentInChildren<Canvas>();
            _healthBarSlider = _healthBar.GetComponentInChildren<Slider>();
            _rigidbody = enemyBody.GetComponent<Rigidbody>();
            _camera = Camera.main;

        }

        private void Update()
        {
            _healthBar.transform.rotation = _camera.transform.rotation;
            var distance = Vector3.Distance(_player.transform.position, transform.position);
            transform.LookAt(_player.transform);

            if (distance < minDistance && _player.transform.hasChanged)
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
                    _rollSpeed = MAX_ROLL_SPEED;
                    _nav.speed = MAX_ROLL_SPEED;
                }

                _rigidbody.AddTorque(new Vector3(rotX / 2, 0, rotZ / 2) * _rollSpeed);
                _nav.SetDestination(destination);
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

        private void Shoot()
        {
            // TODO: Implement shooting
        }
    }
}