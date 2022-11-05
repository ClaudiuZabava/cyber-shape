using UnityEngine;
using Utility;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int rank = 1;
        
        private EnemyHealthBar _healthBar;
        private int _health;

        private void Awake()
        {
            _healthBar = GetComponentInChildren<EnemyHealthBar>();
        }

        private void Start()
        {
            _health = ScalingUtils.GetFibonacci(rank + 1);
            _healthBar.SetMaxHealth(_health);
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
            _healthBar.SetHealth(_health);
        }
    }   
}