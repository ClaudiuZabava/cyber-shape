using UnityEngine;

namespace Enemy
{
    public abstract class AbstractEnemyController : MonoBehaviour
    {
        public abstract float CollisionDamage { get; }

        public abstract void TakeDamage(float damage);
    }
}