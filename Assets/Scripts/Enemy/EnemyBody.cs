using UnityEngine;

namespace Enemy
{
    public class EnemyBody : MonoBehaviour
    {
        private Enemy _enemy;

        private void Awake()
        {
            _enemy = GetComponentInParent<Enemy>();
        }

        public void TakeDamage(int damage)
        {
            _enemy.TakeDamage(damage);
        }
    }
}