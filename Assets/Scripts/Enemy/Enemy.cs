using Evolution;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        private EnemyHealthBar _healthBar;
        private Evolvable _evolvable;
        private int _health;

        private EnemyStageData StageData => _evolvable.Stage.EnemyData;

        private void Awake()
        {
            _healthBar = GetComponentInChildren<EnemyHealthBar>();
            _evolvable = GetComponentInChildren<Evolvable>();
        }

        private void Start()
        {
            _health = StageData.Health;
            _healthBar.SetMaxHealth(_health);
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
            _healthBar.SetHealth(_health);
        }
    }   
}